using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorade.Core.Interop
{
    /// <summary>
    /// An arguments class for use when calling a JavaScript function from .NET, where the JavaScript function
    /// needs to call back to your .NET code.
    /// </summary>
    /// <remarks>
    /// This is useful in scenarios where the JavaScript function that you call from .NET calls into some JavaScript
    /// API that returns a result using callback functions.
    /// </remarks>
    public class DotNetInstanceCallbackArgs : IDisposable
    {

        /// <summary>
        /// The callback method that will be called from JavaScript when the operation completes successfully.
        /// </summary>
        public DotNetInstanceMethod SuccessCallback { get; set; }

        /// <summary>
        /// The callback method that will be called from JavaScript when the operation fails.
        /// </summary>
        public DotNetInstanceMethod FailureCallback { get; set; }

        /// <summary>
        /// Arbitrary data to pass in to the JavaScript function.
        /// </summary>
        public Dictionary<string, object> Data { get; set; }



        /// <summary>
        /// Handles disposing of objects.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }



        private bool IsDisposed = false;
        /// <summary>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                this.SuccessCallback?.Dispose();
                this.FailureCallback?.Dispose();
            }

            this.IsDisposed = true;
        }
    }
}
