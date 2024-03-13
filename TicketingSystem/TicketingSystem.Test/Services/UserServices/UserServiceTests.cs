using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Abstractions.IServices;
using TicketingSystem.Application.Mappers;
using TicketingSystem.Domain.Entities.DTOs;
using TicketingSystem.Domain.Entities.Models;

namespace TicketingSystem.Test.Services.UserServices
{
    public class UserServiceTests
    {
        private readonly Mock<IUserService> _userservice = new Mock<IUserService>();
        MapperConfiguration? mockMapper = new MapperConfiguration(conf =>
        {
            conf.AddProfile(new AutoMapperProfile());
        });

        public static IEnumerable<object[]> GetUserFromDataGenerator()
        {
            yield return new object[]
            {
                new UserDTO()
                {
                    Username = "test1",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin"
                },
                new User()
                {
                    Username = "test1",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin",
                }
            };
            yield return new object[]
            {
                new UserDTO()
                {
                    Username = "test2",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin"
                },
                new User()
                {
                    Username = "test2",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin",
                }
            };
            yield return new object[]
            {
                new UserDTO()
                {
                    Username = "test3",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin"
                },
                new User()
                {
                    Username = "test3",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin",
                }
            };
            yield return new object[]
            {
                new UserDTO()
                {
                    Username = "test4",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin"
                },
                new User()
                {
                    Username = "test4",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin",
                }
            };
            yield return new object[]
            {
                new UserDTO()
                {
                    Username = "test5",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin"
                },
                new User()
                {
                    Username = "test5",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin",
                }
            };
            yield return new object[]
            {
                new UserDTO()
                {
                    Username = "test6",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin"
                },
                new User()
                {
                    Username = "test6",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin",
                }
            };
            yield return new object[]
            {
                new UserDTO()
                {
                    Username = "test7",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin"
                },
                new User()
                {
                    Username = "test7",
                    Email = "aaa11@gmail.com",
                    Password = "123456",
                    Login = "aaa11@gmail.com",
                    role = "Admin",
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetUserFromDataGenerator), MemberType = typeof(UserServiceTests)]
        public async void Create_User_Test(UserDTO inputUser, User expextedUser)
        {
            var myMapper = mockMapper.CreateMapper();

            var result = myMapper.Map<User>(inputUser);

            _userservice.Setup(x => x.Create(It.IsAny<UserDTO>()))
            .ReturnsAsync(result);
        }
    }
}
