using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorade.Core.Components
{
    /// <summary>
    /// A base class for Blazor components.
    /// </summary>
    public abstract class BlazoradeComponentBase : ComponentBase
    {
        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        protected BlazoradeComponentBase()
        {
            this.Attributes = new Dictionary<string, object>();
            this.Classes = new List<string>();
            this.Styles = new Dictionary<string, string>();
        }



        /// <summary>
        /// A collection of attributes that will be merged onto the component when rendered.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> Attributes { get; set; }

        /// <summary>
        /// Returns a read-only copy of the classes defined on the component. 
        /// </summary>
        public IReadOnlyCollection<string> Classes { get; private set; }

        /// <summary>
        /// Returns a read-only copy of the inline styles defined on the component.
        /// </summary>
        public IReadOnlyDictionary<string, string> Styles { get; set; }



        public override Task SetParametersAsync(ParameterView parameters)
        {
            this.Attributes.Clear();
            this.Classes = new List<string>();
            this.Styles = new Dictionary<string, string>();

            return base.SetParametersAsync(parameters);
        }



        /// <summary>
        /// Adds an attribute to the <see cref="this.Attributes"/> dictionary.
        /// </summary>
        /// <param name="name">The name of the attribute to add.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <returns>Returns the component instance.</returns>
        protected BlazoradeComponentBase AddAttribute(string name, object value)
        {
            this.Attributes[name] = value;
            return this;
        }

        /// <summary>
        /// Adds the given classes to the <see cref="Classes"/> collection.
        /// </summary>
        /// <param name="classNames">An array of class names to add.</param>
        /// <returns>Returns the component instance.</returns>
        protected BlazoradeComponentBase AddClasses(params string[] classNames)
        {
            var list = (ICollection<string>)this.Classes;

            if(null != classNames)
            {
                foreach(var name in classNames)
                {
                    if(!list.Contains(name))
                    {
                        list.Add(name);
                    }
                }

                this.BuildClassAttribute();
            }

            return this;
        }

        /// <summary>
        /// Adds a style to the style dictionary. The style dictionary will be rendered as an inline <c>style</c> attribute on the resulting element.
        /// </summary>
        /// <param name="styleName">The name of the style.</param>
        /// <param name="value">The value of the style.</param>
        /// <returns>Returns the component instance.</returns>
        protected BlazoradeComponentBase AddStyle(string styleName, string value)
        {
            ((IDictionary<string, string>)this.Styles)[styleName] = value;
            return this;
        }

        /// <summary>
        /// Removes the attribute with the given name.
        /// </summary>
        /// <param name="name">The name of the attribute to remove.</param>
        /// <returns>Returns the component instance.</returns>
        protected BlazoradeComponentBase RemoveAttribute(string name)
        {
            if (this.Attributes.ContainsKey(name))
            {
                this.Attributes.Remove(name);
            }

            return this;
        }

        /// <summary>
        /// Removes the given class from the <see cref="Classes"/> collection.
        /// </summary>
        /// <param name="className">The class name to remove.</param>
        /// <returns>Returns the component instance.</returns>
        protected BlazoradeComponentBase RemoveClass(string className)
        {
            var list = (ICollection<string>)this.Classes;
            if(list.Contains(className))
            {
                list.Remove(className);
                this.BuildClassAttribute();
            }

            return this;
        }

        /// <summary>
        /// Removes the given style from the styles dictionary.
        /// </summary>
        /// <param name="styleName">The style to remove.</param>
        /// <returns>Returns the component instance.</returns>
        protected BlazoradeComponentBase RemoveStyle(string styleName)
        {
            if(this.Styles.ContainsKey(styleName))
            {
                ((IDictionary<string, string>)this.Styles).Remove(styleName);
                this.BuildStyleAttribute();
            }
            return this;
        }



        private void BuildClassAttribute()
        {
            if(this.Classes.Count > 0)
            {
                this.AddAttribute("class", string.Join(" ", this.Classes));
            }
            else
            {
                this.RemoveAttribute("class");
            }
        }

        private void BuildStyleAttribute()
        {
            if(this.Styles.Count > 0)
            {
                this.AddAttribute("style", string.Join("; ", from x in this.Styles select $"{x.Key}: {x.Value}"));
            }
            else
            {
                this.RemoveAttribute("style");
            }
        }

    }
}
