using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blazorade.Core.Components.Builder
{
    /// <summary>
    /// A generic attribute builder implementation that implements the <see cref="IAttributeBuilder"/> interface.
    /// </summary>
    public class AttributeBuilder : BuilderBase<KeyValuePair<string, object>>, IAttributeBuilder
    {

        /// <summary>
        /// Creates an instance of the builder.
        /// </summary>
        public AttributeBuilder() { }

        /// <summary>
        /// Creates an instance of the builder and specifies the attributes to load into the instance.
        /// </summary>
        /// <param name="attributes">The attributes to load into the builder.</param>
        public AttributeBuilder(params KeyValuePair<string, object>[] attributes)
        {
            foreach (var a in attributes ?? new KeyValuePair<string, object>[0])
            {
                this.Add(a.Key, a.Value);
            }
        }


        /// <summary>
        /// Adds an attribute to the builder.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <remarks>If an attribute with the given name exists, it will be overwritten.</remarks>
        public virtual void Add(string name, object value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            this.Add(new KeyValuePair<string, object>(name, value));
        }

    }
}
