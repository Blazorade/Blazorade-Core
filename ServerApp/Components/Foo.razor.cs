using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Components
{
    partial class Foo
    {

        protected override void OnParametersSet()
        {
            this.AddClasses("foo");
            base.OnParametersSet();
        }
    }
}
