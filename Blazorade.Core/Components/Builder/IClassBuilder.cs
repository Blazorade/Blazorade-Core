using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorade.Core.Components.Builder
{
    /// <summary>
    /// The interface that defines a class builder.
    /// </summary>
    /// <remarks>
    /// A class builder is an implementation that builds class names for an element to add when it is rendered.
    /// </remarks>
    public interface IClassBuilder : IEnumerable<string>
    {

        /// <summary>
        /// Builds the classes and returns them as a collection of strings where each string represents one class name to be added to an element.
        /// </summary>
        IEnumerable<string> Build();

    }
}
