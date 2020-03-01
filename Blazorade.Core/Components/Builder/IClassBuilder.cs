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
    public interface IClassBuilder : IBuilder<string>
    {

    }
}
