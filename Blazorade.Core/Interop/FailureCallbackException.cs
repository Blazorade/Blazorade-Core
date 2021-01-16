using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorade.Core.Interop
{
    /// <summary>
    /// The exception that is thrown when a failure callback is called.
    /// </summary>
    public class FailureCallbackException : Exception
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="result">The result data that was sent with the failure callback.</param>
        public FailureCallbackException(object result) : this(result, null) { }

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="result">The result data that was sent with the failure callback.</param>
        /// <param name="message">An additional error message.</param>
        public FailureCallbackException(object result, string message) : base(message)
        {
            this.Result = result;
        }

        /// <summary>
        /// The result that may contain additional information about the failure.
        /// </summary>
        public object Result { get; private set; }

    }
}
