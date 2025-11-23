using System;

namespace BusinessEntities
{
    public class IdNameObject : IdObject
    {
        private string _name;

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

        public void SetName(string name) => Name = name;
    }
}