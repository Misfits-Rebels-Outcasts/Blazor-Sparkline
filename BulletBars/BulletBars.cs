
 //1.Need to support not just percent but just a series of numbers
 //2.Need to pad start/stop to equal length?   

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAssemblyMan.Charts;

namespace WebAssemblyMan
{
    public class BulletBars :ComponentBase
    {
        [Parameter]
        public string InputData { get; set; }
        [Parameter]
        public string Target { get; set; }
        [Parameter]
        public string Actual { get; set; }

        [Parameter]
        public bool GenerateText { get; set; }

        private string bulletBarOutput;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            BulletBarsFont bulletbars = new BulletBarsFont();
            bulletbars.InputData = InputData;
            bulletbars.Actual = Actual;
            bulletbars.Target = Target;
            bulletbars.GenerateText=GenerateText;
            bulletBarOutput = bulletbars.Encode();

            builder.OpenElement(++seq, "span");
            builder.AddAttribute(++seq, "class", "BulletBars");
            builder.AddMarkupContent(++seq, bulletBarOutput);
            builder.CloseElement();

        }
    }
}
