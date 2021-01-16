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
    public sealed class DotNetInstanceCallbackHandler<TSuccess, TFailure>
    {
        //public DotNetInstanceCallbackHandler(IJSObjectReference module)
        //{

        //}

        /// <summary>
        /// The method that will be called by JavaScript code when the operation completes successfully.
        /// </summary>
        [JSInvokable]
        public Task SuccessCallbackAsync(TSuccess result = default)
        {

            return Task.CompletedTask;
        }

        /// <summary>
        /// The method that will be called by JavaSCript code when the operation fails.
        /// </summary>
        [JSInvokable]
        public Task FalureCallbackAsync(TFailure result = default)
        {

            return Task.CompletedTask;
        }
    }
}
