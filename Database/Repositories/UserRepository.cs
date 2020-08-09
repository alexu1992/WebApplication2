using Database.Context;
using Database.DB;
using Infrastructure.Interfaces.Repositories;
using Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly TestContext _dbContext;
        public UserRepository(TestContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserResponse Create(string username)
        {
            var user = new User()
            {
                Username = username,
                Password = Guid.NewGuid().ToString(),
                PasswordExpireTime = DateTime.Now.AddSeconds(30)
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return new UserResponse()
            {
                EndDate = user.PasswordExpireTime,
                Password = user.Password,
                Username = user.Username
            };
        }

        public UserResponse Get (string username)
        {
            return _dbContext.Users.Where(x => x.Username == username)
                .Select(x=>new UserResponse()
                {
                    EndDate = x.PasswordExpireTime,
                    Password = x.Password,
                    Username = x.Username
                }).FirstOrDefault();
        }

        public UserResponse UpdatePassword(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Username == username);
            user.Password = Guid.NewGuid().ToString();
            user.PasswordExpireTime = DateTime.Now.AddSeconds(30);
            _dbContext.SaveChanges();
            return new UserResponse()
            {
                EndDate = user.PasswordExpireTime,
                Password = user.Password,
                Username = user.Username
            };
        }
    }
}
