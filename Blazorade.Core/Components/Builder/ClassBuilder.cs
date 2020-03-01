using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blazorade.Core.Components.Builder
{
    /// <summary>
    /// A generic class builder that implements the <see cref="IClassBuilder"/> interface.
    /// </summary>
    public class ClassBuilder : BuilderBase<string>, IClassBuilder
    {
        /// <summary>
        /// Creates an instance of the builder.
        /// </summary>
        public ClassBuilder() { }

        /// <summary>
        /// Creates an instance of the builder and loads it with the given classes.
        /// </summary>
        /// <param name="classes"></param>
        public ClassBuilder(params string[] classes) : this()
        {
            this.Add(classes);
        }




        /// <summary>
        /// Adds the given classes to the builder.
        /// </summary>
        /// <param name="classNames">The classes to add to the builder.</param>
        /// <remarks>Only the given classes that do not already exist in the builder are added.</remarks>
        protected virtual void Add(params string[] classNames)
        {
            if(null != classNames)
            {
                // Add unique classes that do not exist in the current class collection.
                this.AddRange(from x in classNames where !this.Items.Contains(x) group x by x into y select y.Key);
            }
        }

    }
}
