using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(IReadOnlyDictionary<string, string[]> errors)
            : base("Validation Error", "One or more validation failures have occurred.")
        {
            Errors = errors;
        }

        public IReadOnlyDictionary<string, string[]> Errors { get; }
    }
}
