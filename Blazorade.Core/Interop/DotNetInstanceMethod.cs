using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blazorade.Core.Interop
{
    /// <summary>
    /// Encapsulates a .NET instance method in your Blazor application that can be called from JavaScript.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An instance of this class can be used as argument when calling a JavaScript function from .NET. The argument allows
    /// the JS code to call the .NET method encapsulated in this instance.
    /// </para>
    /// <para>
    /// To call the method encapsulated in this instance from your JavaScript code, you pass this instance to your JavaScript
    /// code as an argument. In that JavaScript function you would then use it like this.
    /// </para>
    /// <code>
    /// function myJavaScriptFunction(dotNetInstanceMethod) {
    ///     
    ///     // To call the .NET method without any arguments, you do it like this.
    ///     dotNetInstanceMethod.target.invokeMethodAsync(dotNetInstanceCallback.methodName);
    ///     
    ///     // Or you can also pass in arguments like this.
    ///     dotNetInstanceMethod.target.invokeMethodAsync(dotNetInstanceMethod.methodName, arg1, arg2);
    /// }
    /// </code>
    /// </remarks>
    public sealed class DotNetInstanceMethod: IDisposable
    {

        /// <summary>
        /// Represents the target object defining the method to call back from JavaScript.
        /// </summary>
        public DotNetObjectReference<object> Target { get; set; }

        /// <summary>
        /// The name of the method on the object represented by <see cref="Target"/>.
        /// </summary>
        public string MethodName { get; set; }



        /// <summary>
        /// Creates a new instance from the delegate specified in <paramref name="method"/>.
        /// </summary>
        /// <param name="method">The method to encapsulate in the callback.</param>
        public static DotNetInstanceMethod Create(Delegate method)
        {
            if(null != method)
            {
                ValidateCallbackMethod(method.Method);
                return new DotNetInstanceMethod
                {
                    Target = DotNetObjectReference.Create(method.Target),
                    MethodName = method.Method.Name
                };
            }
            return null;
        }

        /// <summary>
        /// Creates a new instance from the async function specified in <paramref name="method"/>.
        /// </summary>
        /// <param name="method">The method to encapsulate in the callback.</param>
        /// <returns></returns>
        public static DotNetInstanceMethod Create(Func<Task> method)
        {
            return Create(method as Delegate);
        }

        /// <summary>
        /// Creates a new instance from the async function specified in <paramref name="method"/>.
        /// </summary>
        /// <typeparam name="T">The type for the input parameter on the callback method.</typeparam>
        /// <param name="method">The method to encapsulate in the callback.</param>
        public static DotNetInstanceMethod Create<T>(Func<T, Task> method)
        {
            return Create(method as Delegate);
        }

        /// <summary>
        /// Disposes of the object.
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.Target?.Dispose();
            }
            catch { }

            GC.SuppressFinalize(this);
        }


        private static void ValidateCallbackMethod(MethodInfo method)
        {
            var attribute = method.GetCustomAttribute<JSInvokableAttribute>();
            if (null == attribute)
            {
                throw new ArgumentException($"A callback method must be a defined method decorated with the '{typeof(JSInvokableAttribute).FullName}' attribute.", nameof(method));
            }
        }

    }
}
