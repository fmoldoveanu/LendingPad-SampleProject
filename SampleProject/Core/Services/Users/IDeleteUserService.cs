using BusinessEntities;
using Common.Results;
using System;

namespace Core.Services.Users
{
    public interface IDeleteUserService
    {
        Result<User> DeleteById(Guid id);
        Result<User> Delete(User user);
        void DeleteAll();
    }
}