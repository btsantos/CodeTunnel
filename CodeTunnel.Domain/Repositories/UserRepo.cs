using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTunnel.Domain.Interfaces;
using CodeTunnel.Domain.Entities;

namespace CodeTunnel.Domain.Repositories
{
    /// <summary>
    /// Repository for creating, reading, updating, and deleting users.
    /// </summary>
    public class UserRepo : IUserRepository
    {
        /// <summary>
        /// The Entity Framework data context.
        /// </summary>
        CTContext _dataContext;

        /// <summary>
        /// Constructor that accepts a data context as a dependency.
        /// </summary>
        public UserRepo(CTContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Retrieves a user entity by username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A user entity.</returns>
        public User GetUser(string username)
        {
            var result = _dataContext.Users
                .SingleOrDefault(x => x.Username.ToLower() == username.ToLower());
            return result;
        }

        /// <summary>
        /// Adds a user to the repository.
        /// </summary>
        /// <param name="user">The user to be added.</param>
        public void AddUser(User user)
        {
            _dataContext.Users.AddObject(user);
        }

        /// <summary>
        /// Deletes a user from the repository.
        /// </summary>
        /// <param name="user">The user to be deleted.</param>
        public void DeleteUser(User user)
        {
            _dataContext.Users.DeleteObject(user);
        }

        /// <summary>
        /// Persists changes in the repository to the data store.
        /// </summary>
        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }
    }
}
