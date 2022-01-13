using NetCoreAppLayout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.Services
{
    public class UserService
    {
        public List<UserModel> Users
        {
            get
            {
                return new List<UserModel> { new UserModel
                {
                    Email = "test@test.com",
                    UserName = "test"
                },
                new UserModel
                {
                    Email = "ali@test.com",
                    UserName = "ali"
                }

                };
            }
        }
    }
}
