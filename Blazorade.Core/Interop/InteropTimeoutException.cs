using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorade.Core.Interop
{
    /// <summary>
    /// The exception that is used to signal timeouts with.
    /// </summary>
    public class InteropTimeoutException : Exception
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public InteropTimeoutException() { }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public InteropTimeoutException(string message) : base(message) { }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public InteropTimeoutException(string message, Exception innerException) : base(message, innerException) { }

    }
}
