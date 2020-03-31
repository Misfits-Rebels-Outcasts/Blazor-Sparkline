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
    public class WinLoss :ComponentBase
    {
        [Parameter]
        public string InputData { get; set; }

        [Parameter]
        public bool GenerateText { get; set; }

        private string winLossOutput;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            WinLossFont winLossFont = new WinLossFont();
            winLossFont.InputData = InputData;
            winLossFont.GenerateText=GenerateText;
            winLossOutput = winLossFont.Encode();

            builder.OpenElement(++seq, "span");
            builder.AddAttribute(++seq, "class", "WinLoss");
            builder.AddMarkupContent(++seq, winLossOutput);
            builder.CloseElement();

        }
    }
}
