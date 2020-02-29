using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorade.Core.Components.Builder
{

    /// <summary>
    /// The interface implemented by style builders.
    /// </summary>
    /// <remarks>
    /// A style builder is an implementation that builds styles for an element to add to it when rendering.
    /// </remarks>
    public interface IStyleBuilder : IEnumerable<KeyValuePair<string, string>>
    {

        /// <summary>
        /// Builds the styles and returns them as a collection where each item represents one style including its name and value.
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> Build();

    }
}
