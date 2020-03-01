using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorade.Core.Components.Builder
{
    /// <summary>
    /// The base interface for all builders.
    /// </summary>
    /// <typeparam name="TItem">The type of item the builder can handle.</typeparam>
    public interface IBuilder<TItem> : IEnumerable<TItem>
    {

        /// <summary>
        /// Builds the items and returns them as a collection.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TItem> Build();

    }
}
