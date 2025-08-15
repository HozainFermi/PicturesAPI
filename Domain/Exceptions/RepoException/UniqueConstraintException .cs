using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.RepoException
{
    public class UniqueConstraintException : RepositoryException
    {
        public string ConstraintName { get; }

        public UniqueConstraintException(string constraintName, Exception inner)
            : base($"Unique constraint '{constraintName}' violation", inner)
        {
            ConstraintName = constraintName;
        }
    }
}
