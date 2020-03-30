
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
    public class Sparkline :ComponentBase
    {
        [Parameter]
        public string InputData { get; set; }
        [Parameter]
        public bool GenerateText { get; set; }

        //[Parameter]
        //public string StartColor { get; set; }
        //[Parameter]
        //public string StopColor { get; set; }
        [Parameter]
        public string SegmentWidth { get; set; }

        private string sparklineOutput;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            SparklineFont sparkline = new SparklineFont();
            sparkline.InputData = InputData;
            sparkline.GenerateText=GenerateText;
            sparkline.SegmentWidth = SegmentWidth;
            sparklineOutput = sparkline.Encode();

            builder.OpenElement(++seq, "span");
            builder.AddAttribute(++seq, "class", "Sparklines");
            builder.AddMarkupContent(++seq, sparklineOutput);
            builder.CloseElement();
        }
    }
}
