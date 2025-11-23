using System.Collections.Generic;
using BusinessEntities;
using Common.Results;

namespace Core.Services.Users
{
    public interface IUpdateUserService
    {
        Result<User> Update(User user, string name, string email, int age, UserTypes type, decimal? monthlySalary, IEnumerable<string> tags);
    }
}