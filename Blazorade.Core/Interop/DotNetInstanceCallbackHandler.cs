﻿using Microsoft.JSInterop;
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
    public class DotNetInstanceCallbackHandler<TSuccess, TFailure> : IDisposable
    {
        private DotNetInstanceCallbackHandler(string functionIdentifier, Dictionary<string, object> data = null)
        {
            this.FunctionIdentifier = functionIdentifier ?? throw new ArgumentNullException(nameof(functionIdentifier));
            this.Data = data ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="module">The module declaring the JavaScript function to call.</param>
        /// <param name="functionIdentifier">The identifier (name) of the function to call.</param>
        /// <param name="data">
        /// Optional data to pass to the JavaScript function. This will be available on the
        /// <see cref="DotNetInstanceCallbackArgs.Data"/> property in the called JavaScript function.
        /// </param>
        /// <remarks>
        /// This class assumes that the JavaScript function defined by <paramref name="functionIdentifier"/> takes one parameter
        /// that is represented by an instance of the <see cref="DotNetInstanceCallbackArgs"/> class. That instance defines a
        /// success callback and a failure callback. The called JavaScript function is assumed to call either of those to return data.
        /// </remarks>
        public DotNetInstanceCallbackHandler(IJSObjectReference module, string functionIdentifier, Dictionary<string, object> data = null) : this(functionIdentifier, data)
        {
            this.Module = module ?? throw new ArgumentNullException(nameof(module));
            this.Invoker = (id, args) =>
            {
                return this.Module.InvokeVoidAsync(id, args).AsTask();
            };
        }

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="jsRuntime">The JavaScript runtime to use when calling a JavaScript function.</param>
        /// <param name="functionIdentifier">The identifier (name) of the function to call.</param>
        /// <param name="data">
        /// Optional data to pass to the JavaScript function. This will be available on the
        /// <see cref="DotNetInstanceCallbackArgs.Data"/> property in the called JavaScript function.
        /// </param>
        /// <remarks>
        /// This class assumes that the JavaScript function defined by <paramref name="functionIdentifier"/> takes one parameter
        /// that is represented by an instance of the <see cref="DotNetInstanceCallbackArgs"/> class. That instance defines a
        /// success callback and a failure callback. The called JavaScript function is assumed to call either of those to return data.
        /// </remarks>
        public DotNetInstanceCallbackHandler(IJSRuntime jsRuntime, string functionIdentifier, Dictionary<string, object> data = null) : this(functionIdentifier, data)
        {
            this.JSRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
            this.Invoker = (id, args) =>
            {
                return this.JSRuntime.InvokeVoidAsync(id, args).AsTask();
            };
        }

        private Func<string, DotNetInstanceCallbackArgs, Task> Invoker;
        private IJSObjectReference Module;
        private IJSRuntime JSRuntime;
        private TaskCompletionSource<TSuccess> Promise;
        private string FunctionIdentifier;
        private Dictionary<string, object> Data;
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
        /// Calls the JavaScript function specified when the class instance was created. The <see cref="Task"/> returned
        /// by this method will complete when the called JavaScript function calls one of the callbacks sent as argument.
        /// </summary>
        public async Task<TSuccess> GetResultAsync()
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
                Data = this.Data
            };

            await this.Invoker(this.FunctionIdentifier, this.Args);

            return await this.Promise.Task;
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
        /// <param name="functionIdentifier">The identifier (name) of the function to call.</param>
        /// <param name="data">
        /// Optional data to pass to the JavaScript function. This will be available on the
        /// <see cref="DotNetInstanceCallbackArgs.Data"/> property in the called JavaScript function.
        /// </param>
        public DotNetInstanceCallbackHandler(IJSObjectReference module, string functionIdentifier, Dictionary<string, object> data = null) : base(module, functionIdentifier, data) { }

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="jsRuntime">The JavaScript runtime to use when calling a JavaScript function.</param>
        /// <param name="functionIdentifier">The identifier (name) of the function to call.</param>
        /// <param name="data">
        /// Optional data to pass to the JavaScript function. This will be available on the
        /// <see cref="DotNetInstanceCallbackArgs.Data"/> property in the called JavaScript function.
        /// </param>
        public DotNetInstanceCallbackHandler(IJSRuntime jsRuntime, string functionIdentifier, Dictionary<string, object> data = null) : base(jsRuntime, functionIdentifier, data) { }
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
        /// <param name="functionIdentifier">The identifier (name) of the function to call.</param>
        /// <param name="data">
        /// Optional data to pass to the JavaScript function. This will be available on the
        /// <see cref="DotNetInstanceCallbackArgs.Data"/> property in the called JavaScript function.
        /// </param>
        public DotNetInstanceCallbackHandler(IJSObjectReference module, string functionIdentifier, Dictionary<string, object> data = null) : base(module, functionIdentifier, data) { }

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="jsRuntime">The JavaScript runtime to use when calling a JavaScript function.</param>
        /// <param name="functionIdentifier">The identifier (name) of the function to call.</param>
        /// <param name="data">
        /// Optional data to pass to the JavaScript function. This will be available on the
        /// <see cref="DotNetInstanceCallbackArgs.Data"/> property in the called JavaScript function.
        /// </param>
        public DotNetInstanceCallbackHandler(IJSRuntime jsRuntime, string functionIdentifier, Dictionary<string, object> data = null) : base(jsRuntime, functionIdentifier, data) { }
    }
}
