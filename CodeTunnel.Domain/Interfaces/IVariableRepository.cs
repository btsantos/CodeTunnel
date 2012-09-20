using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTunnel.Domain.Entities;

namespace CodeTunnel.Domain.Interfaces
{
    /// <summary>
    /// Repository for creating, reading, updating, and deleting variables.
    /// </summary>
    public interface IVariableRepository
    {
        /// <summary>
        /// Retrieves a variable by name.
        /// </summary>
        /// <param name="name">The unique name of the variable.</param>
        /// <returns>A variable entity.</returns>
        Variable GetVariable(string name);

        /// <summary>
        /// Adds a variable to the repository.
        /// </summary>
        /// <param name="variable">The variable to be added.</param>
        void AddVariable(Variable variable);

        /// <summary>
        /// Deletes a variable from the repository.
        /// </summary>
        /// <param name="variable">The variable to be deleted.</param>
        void DeleteVariable(Variable variable);

        /// <summary>
        /// Persists changes in the repository to the data store.
        /// </summary>
        void SaveChanges();
    }
}
