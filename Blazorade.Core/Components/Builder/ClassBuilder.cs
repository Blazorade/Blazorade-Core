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
    public class ClassBuilder : IClassBuilder
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


        private List<string> Classes = new List<string>();

        /// <summary>
        /// Returns the classes from the builder.
        /// </summary>
        public IEnumerable<string> Build()
        {
            return this.Classes.AsEnumerable();
        }


        /// <summary>
        /// Adds the given classes to the builder.
        /// </summary>
        /// <param name="classNames">The classes to add to the builder.</param>
        /// <remarks>Only the given classes that do not already exist in the builder are added.</remarks>
        public void Add(params string[] classNames)
        {
            if(null != classNames)
            {
                // Add unique classes that do not exist in the current class collection.
                this.Classes.AddRange(from x in classNames where !this.Classes.Contains(x) group x by x into y select y.Key);
            }
        }

        /// <summary>
        /// Removes the given classes from the builder if they exist.
        /// </summary>
        /// <param name="classNames">The class names to remove.</param>
        public void Remove(params string[] classNames)
        {
            if(null != classNames)
            {
                foreach(var c in classNames)
                {
                    if (this.Classes.Contains(c)) this.Classes.Remove(c);
                }
            }
        }



        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return this.Classes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Classes.GetEnumerator();
        }

    }
}
