using Microsoft.EntityFrameworkCore;
using KFC.Core.Utils;
using KFC.Entity;
using KFC.Repositories.Base;

namespace KFC.Repositories.SeedData
{
    public class ApplicationDbContextInitialiser
    {
        private readonly KFCDBContext _context;
        public ApplicationDbContextInitialiser(
            KFCDBContext context)
        {
            _context = context;
        }

        //public void Initialise()
        //{
        //    try
        //    {
        //        if (_context.Database.IsSqlServer())
        //        {
        //            bool dbExists = _context.Database.CanConnect();
        //            if (!dbExists)
        //            {
        //                _context.Database.Migrate();
        //            }

        //            Seed();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    finally
        //    {
        //        _context.Dispose();
        //    }
        //}

        public class CountResult
        {
            public int TableCount { get; set; }
        }
        public void Seed()
        {
            
        }

        private static ApplicationUser[] CreateUser()
        {
            var passwordHasher = new FixedSaltPasswordHasher<ApplicationUser>();
            ApplicationUser[] users =
            [

                new ApplicationUser
                {
                    UserName = "admin",
                    FullName = "Admin",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PasswordHash = passwordHasher.HashPassword(null, "admin123@")
                },
                new ApplicationUser
                {
                    UserName = "string",
                    FullName = "DangTo",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PasswordHash = passwordHasher.HashPassword(null, "string")
                },

            ];
            return users;
        }


    }
}