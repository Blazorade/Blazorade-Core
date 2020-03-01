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

        IReadOnlyDictionary<string, object> BuildAttributes();

        IReadOnlyCollection<string> BuildClasses();

        IReadOnlyDictionary<string, string> BuildStyles();

    }
}
