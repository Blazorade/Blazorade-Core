using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorade.Core.Components.Builder
{
    /// <summary>
    /// The interface implemented by attribute builders.
    /// </summary>
    /// <remarks>
    /// An attribute builder is an implementation that builds attributes for an element to add when it is rendered.
    /// </remarks>
    public interface IAttributeBuilder : IEnumerable<KeyValuePair<string, object>>
    {

        /// <summary>
        /// Builds the attributes and returns them as a collection where each item represents one attribute including its name and value.
        /// </summary>
        IEnumerable<KeyValuePair<string, object>> Build();

    }
}
