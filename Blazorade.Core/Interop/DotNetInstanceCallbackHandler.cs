using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blazorade.Core.Interop
{
    /// <summary>
    /// Handles callbacks to and from JavaScript code.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class DotNetInstanceCallbackHandler<TSuccess, TFailure>: IDisposable
    {
        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="module">The module declaring the JavaScript function to call.</param>
        public DotNetInstanceCallbackHandler(IJSObjectReference module)
        {
            this.Module = module ?? throw new ArgumentNullException(nameof(module));
        }

        private IJSObjectReference Module;
        private TaskCompletionSource<TSuccess> Promise;
        private DotNetInstanceCallbackArgs Args;

        /// <summary>
        /// The method that will be called by JavaScript code when the operation completes successfully.
        /// </summary>
        /// <remarks>
        /// This method supports the functionality provided by the class and should not be used in your code.
        /// </remarks>
        [JSInvokable]
        public Task SuccessCallbackAsync(TSuccess result = default)
        {
            this.Promise.TrySetResult(result);
            return Task.CompletedTask;
        }

        /// <summary>
        /// The method that will be called by JavaSCript code when the operation fails.
        /// </summary>
        /// <remarks>
        /// This method supports the functionality provided by the class and should not be used in your code.
        /// </remarks>
        [JSInvokable]
        public Task FailureCallbackAsync(TFailure result = default)
        {
            this.Promise.TrySetException(new FailureCallbackException(result));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Calls the JavaScript function specified by <paramref name="functionIdentifier"/>. The <see cref="Task"/> returned
        /// by this method will complete when the called JavaScript function calls one of the callbacks sent as argument.
        /// </summary>
        /// <remarks>
        /// This method assumes that the JavaScript function defined by <paramref name="functionIdentifier"/> takes one parameter
        /// that is represented by an instance of the <see cref="DotNetInstanceCallbackArgs"/> class. That instance defines a
        /// success callback and a failure callback. The called JavaScript function will call either of those to return data.
        /// </remarks>
        /// <param name="functionIdentifier">The identifier (name) of the function to call.</param>
        /// <param name="data">
        /// Optional data to pass to the JavaScript function. This will be available on the
        /// <see cref="DotNetInstanceCallbackArgs.Data"/> property in the called JavaScript function.
        /// </param>
        /// <returns></returns>
        public Task<TSuccess> GetResultAsync(string functionIdentifier, Dictionary<string, object> data = null)
        {
            if(null != this.Promise)
            {
                throw new InvalidOperationException("This method has already been called on this instance. To call another JavaScript function you must create a new callback handler instance.");
            }

            this.Promise = new TaskCompletionSource<TSuccess>();

            this.Args = new DotNetInstanceCallbackArgs
            {
                SuccessCallback = DotNetInstanceMethod.Create<TSuccess>(this.SuccessCallbackAsync),
                FailureCallback = DotNetInstanceMethod.Create<TFailure>(this.FailureCallbackAsync),
                Data = data ?? new Dictionary<string, object>()
            };

            this.Module.InvokeVoidAsync(functionIdentifier, this.Args);

            return this.Promise.Task;
        }

        /// <summary>
        /// Disposes of the handler when it has been used.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        private bool disposed = false;
        /// <summary>
        /// Disposes of the handler when it has been used.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                this.Args?.Dispose();
            }
            this.disposed = true;
        }
    }

    /// <summary>
    /// A class derived from <see cref="DotNetInstanceCallbackHandler{TSuccess, TFailure}"/> that only defines
    /// <typeparamref name="TSuccess"/>.
    /// </summary>
    /// <typeparam name="TSuccess"></typeparam>
    public class DotNetInstanceCallbackHandler<TSuccess> : DotNetInstanceCallbackHandler<TSuccess, object>
    {
        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="module">The module declaring the JavaScript function to call.</param>
        public DotNetInstanceCallbackHandler(IJSObjectReference module) : base(module) { }
    }

    /// <summary>
    /// A class derived from <see cref="DotNetInstanceCallbackHandler{TSuccess, TFailure}"/> without type parameters.
    /// </summary>
    public class DotNetInstanceCallbackHandler : DotNetInstanceCallbackHandler<object, object>
    {
        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="module">The module declaring the JavaScript function to call.</param>
        public DotNetInstanceCallbackHandler(IJSObjectReference module) : base(module) { }
    }
}
