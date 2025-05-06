using Ecommerce_BE.Contract.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_BE.Contract.Services.IService
{
    public interface IUserService
    {
        Task<IList<User>> GetAll();
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
        Task<bool> CheckUserExists(string email);
        Task<bool> CheckUserExists(int id);

    }
}
