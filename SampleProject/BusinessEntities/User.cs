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
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email must be provided.", nameof(Email));
                _email = value.Trim();
            }
        }

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name must be provided.", nameof(Name));
                _name = value.Trim();
            }
        }

        public UserTypes Type
        {
            get => _type;
            private set
            {
                if (!Enum.IsDefined(typeof(UserTypes), value))
                    throw new ArgumentException("Invalid user type.", nameof(Type));
                _type = value;
            }
        }

        public int Age
        {
            get => _age;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Age), "Age cannot be negative.");
                _age = value;
            }
        }

        public decimal? MonthlySalary
        {
            get => _monthlySalary;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(MonthlySalary), "Salary cannot be negative.");
                _monthlySalary = value;
            }
        }

        public decimal? AnnualSalary => _monthlySalary.HasValue ? _monthlySalary * 12 : null;

        public List<string> Tags => _tags;

        public void SetName(string name) => Name = name;
        public void SetEmail(string email) => Email = email;
        public void SetType(UserTypes type) => Type = type;
        public void SetAge(int age) => Age = age;
        public void SetMonthlySalary(decimal? monthlySalary)
        {
            _monthlySalary = monthlySalary; // explicitly overwrite, even if null
        }


        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }
}