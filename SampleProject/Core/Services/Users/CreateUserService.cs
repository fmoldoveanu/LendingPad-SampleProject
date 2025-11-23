using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Common.Results;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class CreateUserService : ICreateUserService
    {
        private readonly IIdObjectFactory<User> _userFactory;
        private readonly IUserRepository _userRepository;

        public CreateUserService(IIdObjectFactory<User> userFactory, IUserRepository userRepository)
        {
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        // Modern route: generates a new Guid
        public Result<User> Create(string name, string email, int age, UserTypes type, decimal? monthlySalary, IEnumerable<string> tags)
        {
            var id = Guid.NewGuid();
            return CreateInternal(id, name, email, age, type, monthlySalary, tags);
        }

        // Legacy route: uses provided Guid
        public Result<User> CreateLegacy(Guid userId, string name, string email, int age, UserTypes type, decimal? monthlySalary, IEnumerable<string> tags)
        {
            return CreateInternal(userId, name, email, age, type, monthlySalary, tags);
        }

        // Shared private helper
        private Result<User> CreateInternal(Guid id, string name, string email, int age, UserTypes type, decimal? monthlySalary, IEnumerable<string> tags)
        {
            // Check for duplicate ID
            if (_userRepository.Get(id) != null)
                return Result<User>.Fail($"User with ID {id} already exists.");

            var user = _userFactory.Create(id);

            try
            {
                user.SetName(name);
                user.SetEmail(email);
                user.SetAge(age);
                user.SetType(type);
                user.SetMonthlySalary(monthlySalary);
                user.SetTags(tags);
            }
            catch (Exception ex)
            {
                return Result<User>.Fail($"Validation failed: {ex.Message}");
            }

            try
            {
                _userRepository.Save(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Fail($"Persistence failed: {ex.Message}");
            }

            return Result<User>.Ok(user);
        }
    }
}
