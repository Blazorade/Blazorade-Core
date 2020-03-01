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
    public class StyleBuilder : BuilderBase<KeyValuePair<string, string>>, IStyleBuilder
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



        /// <summary>
        /// Adds a style to the builder.
        /// </summary>
        /// <param name="name">The name of the style to add.</param>
        /// <param name="value">The value of the style.</param>
        /// <remarks>
        /// If a style with the given name already exists, it will be overwritten.
        /// </remarks>
        public virtual void Add(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            this.Add(new KeyValuePair<string, string>(name, value));
        }

    }
}
