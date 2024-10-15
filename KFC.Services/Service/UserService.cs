using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Spreadsheet;
using KFC.Contract.Repositories.Interface;
using KFC.Contract.Services.Interface;
using KFC.Core.Base;
using KFC.Core.Constants;
using KFC.Core.ExceptionCustom;
using KFC.Core.Utils;
using KFC.Entity;
using KFC.ModelViews.ModelViews.UserModels;
using KFC.ModelViews.UserModels;
using KFC.Services.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KFC.Services.Service
{
    public class UserService : IUser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;

        public UserService(
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            JwtSettings jwtSettings
            )
        {
            _jwtSettings = jwtSettings;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }
        public void Create(ResponseUserModels model)
        {
            User? existedUser =  _unitOfWork.GetRepository<User>().Entities
            .FirstOrDefault(u => u.UserName == model.UserName && !u.DeletedTime.HasValue);
            if (existedUser != null)
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.EXISTED, "Tài khoản đã tồn tại");
            }
            
            Role existedRole = _unitOfWork.GetRepository<Role>().Entities.FirstOrDefault(x => x.Name == model.RoleId)
             ?? throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.EXISTED, "Vai trò không tồn tại");
            User user = new()
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedBy = null,
                CreatedTime = CoreHelper.SystemTimeNow,
                RoleId = model.RoleId
            };

            FixedSaltPasswordHasher<User> passwordHasher = new FixedSaltPasswordHasher<User>(Options.Create(new PasswordHasherOptions()));
            user.Password = passwordHasher.HashPassword(user, model.Password);
            User userRole = new User
            {
                Id = user.Id,
                RoleId = existedRole.Id.ToString(),
            };
             _unitOfWork.GetRepository<User>().InsertAsync(userRole);
             _unitOfWork.GetRepository<User>().InsertAsync(user);
             _unitOfWork.Save();
        }

        public void DeleteById(string id)
        {
            var x = _unitOfWork.GetRepository<User>().Entities.FirstOrDefault(x => x.Id.Equals(id));
            x.DeletedBy = null;
            x.DeletedTime = CoreHelper.SystemTimeNow;
        }

        public List<ResponseUserModels> GetAll()
        {
            var data = _unitOfWork.GetRepository<User>().Entities.Where(x => !x.DeletedTime.HasValue).Select(x => new ResponseUserModels
            {
                UserName = x.UserName,
                Password = x.Password,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                RoleId = x.RoleId,
                CreatedTime = x.CreatedTime,
                CreatedBy = x.CreatedBy!
            }).ToList();

            return data;
        }

        public ResponseUserModels GetById(string id)
        {
            var x = _unitOfWork.GetRepository<User>().Entities.FirstOrDefault(x => x.Id.Equals(id));
            var result = new ResponseUserModels
            {
                UserName = x.UserName,
                Password = x.Password,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                RoleId = x.RoleId,
                CreatedTime = x.CreatedTime,
                CreatedBy = x.CreatedBy!
            };


            return result;
        }

        public void Update(ResponseUserModels model)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> LoginAsync(LoginRequestModel request)
        {
            // Tìm người dùng dựa trên tên đăng nhập và đảm bảo tài khoản không bị xóa
            User user = await _unitOfWork.GetRepository<User>().Entities
                .FirstOrDefaultAsync(u => !u.DeletedTime.HasValue && u.UserName == request.Username)
                ?? throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.BADREQUEST, "Không tìm thấy tài khoản");

            // Tạo đối tượng hash mật khẩu
            FixedSaltPasswordHasher<User> passwordHasher = new FixedSaltPasswordHasher<User>(Options.Create(new PasswordHasherOptions()));

            // Hash mật khẩu đầu vào (use null! to suppress nullable warning)
            string hashedInputPassword = passwordHasher.HashPassword(null!, request.Password);

            // So sánh mật khẩu đã hash với mật khẩu trong cơ sở dữ liệu
            if (hashedInputPassword != user.Password)
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.BADREQUEST, "Mật khẩu sai");
            }

            // Tìm vai trò của người dùng
            Role roleUser = await _unitOfWork.GetRepository<Role>().Entities
                .FirstOrDefaultAsync(x => x.Id.ToString() == user.RoleId)
                ?? throw new ErrorException(StatusCodes.Status401Unauthorized, ResponseCodeConstants.BADREQUEST, "Không tìm thấy vai trò của tài khoản");

            // Lấy vai trò từ vai trò của người dùng (nullable role)
            Role? role = await _unitOfWork.GetRepository<Role>().Entities
                .FirstOrDefaultAsync(x => x.Id == roleUser.Id);
            string roleName = role?.Name ?? "Unknown";
            // Tạo phản hồi token và token JWT
            return new LoginResponse
            {
                TokenResponse = _tokenService.GenerateTokens(user, roleName),
                Role = roleName,
                token = Authentication.CreateToken(user.Id.ToString(), _jwtSettings)
            };
        }
    }
}
