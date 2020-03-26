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
    public class WinLossMan :ComponentBase
    {
        [Parameter]
        public string InputData { get; set; }

        private string[] sparklineOutput;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            WinLoss winLoss = new WinLoss();
            winLoss.InputData = InputData;
            sparklineOutput = winLoss.Encode();

            builder.OpenElement(seq, "figure");
            builder.OpenElement(++seq, "div");
            builder.AddAttribute(++seq, "class", "winloss-main");

            foreach (string sline in sparklineOutput)
            {

                builder.OpenElement(++seq, "span");
                builder.AddAttribute(++seq, "class", "WinLoss");
                string slineu = sline + "<br />";
                builder.AddMarkupContent(++seq, slineu);
                builder.CloseElement();
            }
            builder.CloseElement();
            builder.CloseElement();

        }
    }
}
