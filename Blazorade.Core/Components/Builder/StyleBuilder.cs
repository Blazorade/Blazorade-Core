using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blazorade.Core.Components.Builder
{
    /// <summary>
    /// A generic style builder implementation that can also be used as base for other builder implementations.
    /// </summary>
    public class StyleBuilder : IStyleBuilder
    {

        /// <summary>
        /// Creates an instance of the builder.
        /// </summary>
        public StyleBuilder() { }

        /// <summary>
        /// Creates an instance of the class and loads it with styles.
        /// </summary>
        /// <param name="styles">The styles to load into the instance.</param>
        public StyleBuilder(params KeyValuePair<string, string>[] styles)
        {
            foreach(var s in styles ?? new KeyValuePair<string, string>[0])
            {
                this.Add(s.Key, s.Value);
            }
        }



        private Dictionary<string, string> Styles = new Dictionary<string, string>();

        /// <summary>
        /// Builds the styles and returns them as a collection where each item represents one style including its name and value.
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Build()
        {
            return this.Styles.AsEnumerable();
        }

        /// <summary>
        /// Adds a style to the builder.
        /// </summary>
        /// <param name="name">The name of the style to add.</param>
        /// <param name="value">The value of the style.</param>
        /// <remarks>
        /// If a style with the given name already exists, it will be overwritten.
        /// </remarks>
        public void Add(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            this.Styles[name] = value;
        }

        /// <summary>
        /// Removes the style with the given name.
        /// </summary>
        /// <param name="name">The name of the style to remove.</param>
        /// <remarks>
        /// The style name is ignored if it does not exist.
        /// </remarks>
        public void Remove(string name)
        {
            if (this.Styles.ContainsKey(name)) this.Styles.Remove(name);
        }




        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
