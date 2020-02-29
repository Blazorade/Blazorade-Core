using Blazorade.Core.Components.Builder;
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
            this.InitComponent();
        }



        /// <summary>
        /// A collection of attributes that will be merged onto the component when rendered.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> Attributes { get; set; }

        /// <summary>
        /// An attribute builder that is used when building attributes for the element before rendering.
        /// </summary>
        [Parameter]
        public IAttributeBuilder AttributeBuilder { get; set; }

        /// <summary>
        /// Enables child content for the control.
        /// </summary>
        [Parameter]
        public virtual RenderFragment ChildContent { get; set; }

        /// <summary>
        /// A class builder that is used when building the class names for the element before rendering.
        /// </summary>
        [Parameter]
        public IClassBuilder ClassBuilder { get; set; }

        /// <summary>
        /// Returns a read-only copy of the classes defined on the component. 
        /// </summary>
        public IReadOnlyCollection<string> Classes { get; private set; }

        /// <summary>
        /// Returns a read-only copy of the inline styles defined on the component.
        /// </summary>
        public IReadOnlyDictionary<string, string> Styles { get; set; }

        /// <summary>
        /// A style builder that is used when building styles for the element before rendering.
        /// </summary>
        [Parameter]
        public IStyleBuilder StyleBuilder { get; set; }



        /// <summary>
        /// Prepares the component for parameters.
        /// </summary>
        public override Task SetParametersAsync(ParameterView parameters)
        {
            this.InitComponent();
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
            if(null != classNames && classNames.Length > 0)
            {
                var list = (List<string>)this.Classes;
                if (list.Count == 0 && classNames.Length > 0 && this.Attributes.TryGetValue("class", out object val))
                {
                    // If we don't have any classes defined yet, but there is a class attribute with classes, then we add those to the classes collection first.
                    list.AddRange(from x in $"{val}".Split(' ') select x);
                }

                foreach (var name in classNames)
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
            this.BuildStyleAttribute();
            return this;
        }

        /// <summary>
        /// Handles builders set on the component.
        /// </summary>
        /// <remarks>
        /// If you override this method, you must call the base implementation at some point in your implementation.
        /// </remarks>
        protected override void OnParametersSet()
        {
            
            foreach(var a in this.AttributeBuilder?.Build() ?? new KeyValuePair<string, object>[0])
            {
                this.AddAttribute(a.Key, a.Value);
            }

            if (null != this.ClassBuilder)
            {
                this.AddClasses(this.ClassBuilder.Build().ToArray());
            }

            foreach(var s in this.StyleBuilder?.Build() ?? new KeyValuePair<string, string>[0])
            {
                this.AddStyle(s.Key, s.Value);
            }

            base.OnParametersSet();
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

        private void InitComponent()
        {
            this.Attributes = new Dictionary<string, object>();
            this.Classes = new List<string>();
            this.Styles = new Dictionary<string, string>();
        }

    }
}
