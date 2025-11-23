using BusinessEntities;
using Common;
using Common.Results;
using Data.Repositories;
using System;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result<User> DeleteById(Guid id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
                return Result<User>.Fail("User not found.");

            _userRepository.Delete(user);
            return Result<User>.Ok(user);
        }

        public Result<User> Delete(User user)
        {
            if (user == null)
                return Result<User>.Fail("User cannot be null.");

            _userRepository.Delete(user);
            return Result<User>.Ok(user);
        }

        public void DeleteAll()
        {
            _userRepository.DeleteAll();
        }
    }
}