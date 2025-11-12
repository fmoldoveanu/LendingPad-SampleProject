using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class User : IdObject
    {
        private readonly List<string> _tags = new List<string>();
        private int _age;
        private string _email;
        private decimal? _monthlySalary;
        private string _name;
        private UserTypes _type = UserTypes.Employee;

        public string Email
        {
            get => _email;
            private set => _email = value;
        }

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public UserTypes Type
        {
            get => _type;
            private set => _type = value;
        }

        public decimal? MonthlySalary
        {
            get => _monthlySalary;
            private set => _monthlySalary = value;
        }

        public int Age
        {
            get => _age;
            private set => _age = value;
        }

        public List<string> Tags
        {
            get => _tags;
            private set => _tags.Initialize(value);
        }

        public string SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Name was not provided.";
            }
            _name = name;
            return "";
        }

        public string SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return "Email was not provided.";
            }
            _email = email;
            return "";
        }

        public string SetType(UserTypes type)
        {
            if (!Enum.IsDefined(typeof(UserTypes), type))
            {
                return "Invalid user type.";
            }
            _type = type;
            return "";
        }

        public string SetAge(int age)
        {
            if (age < 0)
            {
                return "Age cannot be negative.";
            }
            _age = age;
            return "";
        }

        public void SetMonthlySalary(decimal? monthlySalary)
        {
            _monthlySalary = monthlySalary;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }
}