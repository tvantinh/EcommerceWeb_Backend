using Ecommerce_BE.Contract.Repositories.Entities;
using Ecommerce_BE.Contract.Repositories.IUnitOfWork;
using Ecommerce_BE.Contract.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_BE.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> CheckUserExists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckUserExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetAll()
        {
            return _unitOfWork.GetGenericRepository<User>().GetAllAsync();
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
    
}
