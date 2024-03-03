using TicketingSystem.Application.Abstractions.IRepositories;
using TicketingSystem.Application.Abstractions.IServices;
using TicketingSystem.Domain.Entities.DTOs;
using TicketingSystem.Domain.Entities.Models;

namespace TicketingSystem.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Create(UserDTO userDTO)
        {
            var email = await _userRepository.GetByAny(x => x.Email == userDTO.Email);
            var username = await _userRepository.GetByAny(x => x.Username == userDTO.Username);
            if (username == null)
            {
                if (email == null)
                {
                    var user = new User()
                    {
                        Username = userDTO.Username,
                        Email = userDTO.Email,
                        Password = userDTO.Password,
                        role = userDTO.role,
                    };
                    var result = await _userRepository.Create(user);

                    return "You succesfully registered!";
                }
                return "This email already exists";
            }
            return "This username already exists";
        }
        public async Task<string> Delete(int id)
        {
            var result = await _userRepository.Delete(x => x.Id == id);
            if (result)
            {
                return "Deleted";
            }
            else
            {
                return "Failed";
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _userRepository.GetAll();

            var result = users.Select(model => new User
            {
                Username = model.Username,
                Email = model.Email,
                role = model.role,
            });

            return result;
        }

        public async Task<User> GetByEmail(string email)
        {
            var result = await _userRepository.GetByAny(x => x.Email == email);
            return result;
        }

        public async Task<User> GetById(int Id)
        {
            var result = await _userRepository.GetByAny(x => x.Id == Id);
            return result;
        }

        public async Task<User> GetByName(string name)
        {
            var result = await _userRepository.GetByAny(d => d.Username == name);
            return result;
        }

        public async Task<string> Update(int Id, UserDTO userDTO)
        {
            var res = await _userRepository.GetAll();
            var email = res.Any(x => x.Email == userDTO.Email);
            var name = res.Any(x => x.Username == userDTO.Username);
            if (!email)
            {
                if (!name)
                {
                    var old = await _userRepository.GetByAny(x => x.Id == Id);

                    if (old == null) return "Failed";
                    old.Username = userDTO.Username;
                    old.Password = userDTO.Password;
                    old.Email = userDTO.Email;
                    old.role = userDTO.role;


                    await _userRepository.Update(old);
                    return "Updated";

                }
                return "Such login already exists";
            }
            return "Such email already exists";
        }
    }
}
