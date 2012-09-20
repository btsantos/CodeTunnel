using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTunnel.Domain.Interfaces;
using CodeTunnel.Domain.Entities;

namespace CodeTunnel.Domain.Repositories
{
    /// <summary>
    /// Repository for creating, reading, updating, and deleting variables.
    /// </summary>
    public class VariableRepository : IVariableRepository
    {
        /// <summary>
        /// The Entity Framework data context.
        /// </summary>
        CTContext _dataContext;

        /// <summary>
        /// Constructor that accepts a data context as a dependency.
        /// </summary>
        public VariableRepository(CTContext dataContext)
        {
            this._dataContext = dataContext;
        }

        /// <summary>
        /// Retrieves a variable by name.
        /// </summary>
        /// <param name="name">The unique name of the variable.</param>
        /// <returns>A variable entity.</returns>
        public Variable GetVariable(string name)
        {
            return _dataContext.Variables.SingleOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Adds a variable to the repository.
        /// </summary>
        /// <param name="variable">The variable to be added.</param>
        public void AddVariable(Variable variable)
        {
            _dataContext.Variables.AddObject(variable);
        }

        /// <summary>
        /// Deletes a variable from the repository.
        /// </summary>
        /// <param name="variable">The variable to be deleted.</param>
        public void DeleteVariable(Variable variable)
        {
            _dataContext.Variables.DeleteObject(variable);
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
