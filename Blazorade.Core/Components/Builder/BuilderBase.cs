using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blazorade.Core.Components.Builder
{
    /// <summary>
    /// A base implementation for builders.
    /// </summary>
    /// <typeparam name="TItem">The type of items handled by the builder.</typeparam>
    public abstract class BuilderBase<TItem> : IBuilder<TItem>
    {

        /// <summary>
        /// </summary>
        protected BuilderBase()
        {
            this.Items = new List<TItem>();
        }

        /// <summary>
        /// Returns the items contained in the builder.
        /// </summary>
        protected IList<TItem> Items { get; private set; }



        /// <summary>
        /// Builds the items and returns them as a collection.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TItem> Build()
        {
            return this.Items.AsEnumerable();
        }



        /// <summary>
        /// Adds an item to the builder.
        /// </summary>
        /// <param name="item">The item to add.</param>
        protected virtual void Add(TItem item)
        {
            this.Items.Add(item);
        }

        /// <summary>
        /// Adds a range of items to the builder.
        /// </summary>
        /// <param name="items">The items to add.</param>
        protected virtual void AddRange(IEnumerable<TItem> items)
        {
            if(null != items)
            {
                foreach (var i in items)
                {
                    this.Add(i);
                }
            }
        }



        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            this.Build();
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            this.Build();
            return this.Items.GetEnumerator();
        }

    }
}
