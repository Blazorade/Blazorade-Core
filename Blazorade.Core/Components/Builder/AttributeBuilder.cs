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
    public class AttributeBuilder : IAttributeBuilder
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

        private Dictionary<string, object> Attributes = new Dictionary<string, object>();


        /// <summary>
        /// Builds the attributes and returns them as a collection where each item represents one attribute including its name and value.
        /// </summary>
        public IEnumerable<KeyValuePair<string, object>> Build()
        {
            return this.Attributes.AsEnumerable();
        }

        /// <summary>
        /// Adds an attribute to the builder.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <remarks>If an attribute with the given name exists, it will be overwritten.</remarks>
        public void Add(string name, object value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            this.Attributes[name] = value;
        }

        /// <summary>
        /// Removes an attribute from the builder.
        /// </summary>
        /// <param name="name">The name of the attribute to remove.</param>
        /// <remarks>
        /// The given name is ignored if the builder does not contain an attribute with the given name.
        /// </remarks>
        public void Remove(string name)
        {
            if (this.Attributes.ContainsKey(name)) this.Attributes.Remove(name);
        }



        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return this.Attributes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Attributes.GetEnumerator();
        }

    }
}
