using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Blazorade.Core.Components.Builder
{
    /// <summary>
    /// A generic component builder.
    /// </summary>
    public class ComponentBuilder : IComponentBuilder
    {
        /// <summary>
        /// Creates an instance of the builder.
        /// </summary>
        public ComponentBuilder() { }

        /// <summary>
        /// Creates an instance of the builder from the given builder.
        /// </summary>
        /// <param name="builder">The builder to start the new instance from.</param>
        public ComponentBuilder(IComponentBuilder builder)
        {
            var sourceAttributes = builder?.BuildAttributes();
            var sourceStyles = builder?.BuildStyles();

            foreach(var key in sourceAttributes?.Keys ?? new string[0])
            {
                this.Attributes[key] = sourceAttributes[key];
            }

            foreach(var key in sourceStyles?.Keys ?? new string[0])
            {
                this.Styles[key] = sourceStyles[key];
            }

            this.AddClasses((from x in builder?.BuildClasses() ?? new string[0] select x).ToArray());
        }



        private Dictionary<string, object> Attributes = new Dictionary<string, object>();
        private List<string> Classes = new List<string>();
        private Dictionary<string, string> Styles = new Dictionary<string, string>();


        /// <summary>
        /// Returns the attributes from the builder.
        /// </summary>
        public virtual IReadOnlyDictionary<string, object> BuildAttributes()
        {
            return new ReadOnlyDictionary<string, object>(this.Attributes);
        }

        /// <summary>
        /// Returns the classes from the builder.
        /// </summary>
        public virtual IReadOnlyCollection<string> BuildClasses()
        {
            return new ReadOnlyCollection<string>(this.Classes);

        }

        /// <summary>
        /// Returns the styles from the builder.
        /// </summary>
        public virtual IReadOnlyDictionary<string, string> BuildStyles()
        {
            return new ReadOnlyDictionary<string, string>(this.Styles);
        }



        /// <summary>
        /// Adds an attribute to the builder.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <remarks>
        /// If the attribute already exists, it will be overwritten.
        /// </remarks>
        public virtual ComponentBuilder AddAttribute(string name, object value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            this.Attributes[name] = value;

            return this;
        }

        public virtual ComponentBuilder AddAttributes(IDictionary<string, object> attributes)
        {
            if(null != attributes)
            {
                foreach(var key in attributes.Keys)
                {
                    this.AddAttribute(key, attributes[key]);
                }
            }
            return this;
        }

        /// <summary>
        /// Adds the given classes to the builder.
        /// </summary>
        /// <param name="classes">The classes to add. Duplicates and classes that already exist are ignored.</param>
        public virtual ComponentBuilder AddClasses(params string[] classes)
        {
            this.Classes.AddRange(from x in classes ?? new string[0] where !this.Classes.Contains(x) group x by x into y select y.Key);

            return this;
        }

        /// <summary>
        /// Adds a CSS style to the builder.
        /// </summary>
        /// <param name="name">The name of the style.</param>
        /// <param name="value">The value of the style.</param>
        /// <remarks>
        /// If the style already exists, it will be overwritten.
        /// </remarks>
        public virtual ComponentBuilder AddStyle(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            this.Styles[name] = value;

            return this;
        }

        public virtual ComponentBuilder AddStyles(IDictionary<string, string> styles)
        {
            if(null != styles)
            {
                foreach(var key in styles.Keys)
                {
                    this.AddStyle(key, styles[key]);
                }
            }

            return this;
        }


        /// <summary>
        /// Removes the given attribute from the builder.
        /// </summary>
        /// <param name="name">The name of the attribute to remove.</param>
        /// <remarks>If the attribute does not exist, the call will be ignored.</remarks>
        protected virtual void RemoveAttribute(string name)
        {
            if (this.Attributes.ContainsKey(name)) this.Attributes.Remove(name);
        }

        /// <summary>
        /// Removes the given classes from the builder.
        /// </summary>
        /// <param name="classes">The classes to remove.</param>
        /// <remarks>If a class does not exist, it will be ignored.</remarks>
        protected virtual void RemoveClasses(params string[] classes)
        {
            foreach(var c in classes ?? new string[0])
            {
                if (this.Classes.Contains(c)) this.Classes.Remove(c);
            }
        }

        /// <summary>
        /// Removes the given style from the builder.
        /// </summary>
        /// <param name="name">The name of the style to remove.</param>
        /// <remarks>If the style does not exist, it is ignored.</remarks>
        protected virtual void RemoveStyle(string name)
        {
            if (this.Styles.ContainsKey(name)) this.Styles.Remove(name);
        }

    }
}
