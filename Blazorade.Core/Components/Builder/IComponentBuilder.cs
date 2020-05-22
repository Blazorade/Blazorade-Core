using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorade.Core.Components.Builder
{
    /// <summary>
    /// The interface that defines a component builder.
    /// </summary>
    public interface IComponentBuilder
    {

        /// <summary>
        /// Returns the attributes from the builder.
        /// </summary>
        IReadOnlyDictionary<string, object> BuildAttributes();

        /// <summary>
        /// Returns the classes from the builder.
        /// </summary>
        IReadOnlyCollection<string> BuildClasses();

        /// <summary>
        /// Returns the styles from the builder.
        /// </summary>
        IReadOnlyDictionary<string, string> BuildStyles();

    }
}
