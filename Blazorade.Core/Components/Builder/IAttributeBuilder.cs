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
    public interface IAttributeBuilder : IBuilder<KeyValuePair<string, object>>
    {

    }
}
