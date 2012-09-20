using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTunnel.Domain.Entities;

namespace CodeTunnel.Domain.Interfaces
{
    /// <summary>
    /// Repository for creating, reading, updating, and deleting users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user entity by username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A user entity.</returns>
        User GetUser(string username);

        /// <summary>
        /// Adds a user to the repository.
        /// </summary>
        /// <param name="user">The user to be added.</param>
        void AddUser(User user);

        /// <summary>
        /// Deletes a user from the repository.
        /// </summary>
        /// <param name="user">The user to be deleted.</param>
        void DeleteUser(User user);

        /// <summary>
        /// Persists changes in the repository to the data store.
        /// </summary>
        void SaveChanges();
    }
}
