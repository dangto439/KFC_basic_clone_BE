using Microsoft.EntityFrameworkCore;
using KFC.Entity;
using KFC.Repositories.Base;

namespace KFC.Repositories.SeedData
{
    public class ApplicationDbContextInitialiser
    {
        private readonly KFCDBContext _context;
        public ApplicationDbContextInitialiser(KFCDBContext context)
        {
            _context = context;
        }

        // Phương thức khởi tạo cơ sở dữ liệu và seed data
        public void Initialise()
        {
            try
            {
                // Kiểm tra xem cơ sở dữ liệu có phải là SQL Server hay không
                if (_context.Database.IsSqlServer())
                {
                    // Kiểm tra xem có thể kết nối đến cơ sở dữ liệu không
                    bool dbExists = _context.Database.CanConnect();
                    if (!dbExists)
                    {
                        // Nếu chưa kết nối được, áp dụng migration
                        _context.Database.Migrate();
                    }

                    // Seed dữ liệu mặc định vào bảng
                    Seed();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và log lỗi
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                // Giải phóng đối tượng DbContext sau khi hoàn tất
                _context.Dispose();
            }
        }

        // Phương thức Seed dữ liệu mặc định
        public void Seed()
        {
            // Kiểm tra xem bảng Users đã có dữ liệu hay chưa
            if (!_context.Users.Any())
            {
                // Thêm các user mặc định
                //_context.Users.AddRange(new User
                //{
                //    UserName = "admin",
                //    Email = "admin@kfc.com",
                //    PasswordHash = "hashed_password", // Mã hóa mật khẩu thật khi lưu
                //    RoleId = 1, // Giả định RoleId 1 là admin
                //    CreatedBy = "system",
                //    CreatedTime = DateTimeOffset.UtcNow
                //},
                //new User
                //{
                //    UserName = "user",
                //    Email = "user@kfc.com",
                //    PasswordHash = "hashed_password",
                //    RoleId = 2, // Giả định RoleId 2 là user
                //    CreatedBy = "system",
                //    CreatedTime = DateTimeOffset.UtcNow
                //});

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
            }

            // Seed thêm dữ liệu khác như Product, Category, v.v. tương tự
        }

        // Class CountResult không cần thiết ở đây, có thể loại bỏ hoặc sử dụng cho mục đích khác
        public class CountResult
        {
            public int TableCount { get; set; }
        }
    }
}
