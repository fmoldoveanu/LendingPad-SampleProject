using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateUserService : IUpdateUserService
    {
        public string Update(User user, string name, string email, int age, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
        {
            string ret = "";
            ret += user.SetEmail(email);
            ret += user.SetName(name);
            ret += user.SetAge(age);
            ret += user.SetType(type);

            user.SetTags(tags);
            if (annualSalary != null)
            {
                user.SetMonthlySalary(annualSalary.Value / 12);
            }
            else
            {
                user.SetMonthlySalary(0);
            }
            return ret;
        }
    }
}