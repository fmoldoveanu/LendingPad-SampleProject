using System;
using System.Collections.Generic;
using BusinessEntities;
using Common.Results;

namespace Core.Services.Users
{
    public interface ICreateUserService
    {
        Result<User> Create(string name, string email, int age, UserTypes type, decimal? monthlySalary, IEnumerable<string> tags);

        // Legacy route: POST /users/{userId}/create
        Result<User> CreateLegacy(Guid userId, string name, string email, int age, UserTypes type, decimal? monthlySalary, IEnumerable<string> tags);
    }
}