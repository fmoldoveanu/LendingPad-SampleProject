using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Common.Results;
using Data.Repositories;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class UpdateUserService : IUpdateUserService
    {

        private readonly IUserRepository _userRepository;

        public UpdateUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result<User> Update(User user, string name, string email, int age, UserTypes type, decimal? monthlySalary, IEnumerable<string> tags)
         {
            var errors = new List<string>();

            try { 
                user.SetEmail(email); 
            } 
            catch (Exception ex) { 
                errors.Add(ex.Message); 
            }

            try { 
                user.SetName(name); 
            } 
            catch (Exception ex) { 
                errors.Add(ex.Message); 
            }

            try { 
                user.SetAge(age); 
            } 
            catch (Exception ex) { 
                errors.Add(ex.Message); 
            }

            try { 
                user.SetType(type); 
            } 
            catch (Exception ex) { 
                errors.Add(ex.Message); 
            }

            try { 
                user.SetTags(tags); 
            } 
            catch (Exception ex) { 
                errors.Add(ex.Message); 
            }

            try { 
                user.SetMonthlySalary(monthlySalary); 
            } 
            catch (Exception ex) { 
                errors.Add(ex.Message); 
            }

            if (errors.Any())
            {
                return Result<User>.Fail(errors.ToArray());
            }

            // Persist changes
            _userRepository.Save(user);

            return Result<User>.Ok(user);
        }
    }
}