using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.RepoException
{
    public class RepositoryException : AppException
    {
        public RepositoryException(string message, Exception inner)
            : base($"Repository error: {message}", inner) { }
        public RepositoryException(string message) : base($"Repository error:{message}") { }
        
            
        
    }

}
