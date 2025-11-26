using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _items = new List<User>();

        public void Save(User entity)
        {
            var existing = _items.FirstOrDefault(x => x.Id == entity.Id);
            if (existing != null)
            {
                _items.Remove(existing);
            }
            _items.Add(entity);
        }

        public void Delete(User entity)
        {
            _items.RemoveAll(x => x.Id == entity.Id);
        }

        public User Get(Guid id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> Get(UserTypes? userType = null, string name = null, string email = null)
        {
            var query = _items.AsQueryable();

            if (userType != null)
                query = query.Where(u => u.Type == userType);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(u => u.Name != null &&
                                         u.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);

            if (!string.IsNullOrEmpty(email))
                query = query.Where(u => u.Email != null &&
                                         u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            return query.ToList();
        }

        public void DeleteAll()
        {
            _items.Clear();
        }
    }
}
