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
    public class MiniPie :ComponentBase
    {
        [Parameter]
        public string InputData { get; set; }

        [Parameter]
        public bool GenerateText { get; set; }

        private string miniPieOutput;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            MiniPieFont miniPieFont = new MiniPieFont();
            miniPieFont.InputData = InputData;
            miniPieFont.GenerateText = GenerateText;
            miniPieOutput = miniPieFont.Encode();            

/*
            builder.OpenElement(seq, "figure");
            builder.OpenElement(++seq, "div");
            builder.AddAttribute(++seq, "class", "minipie-main");
            foreach (string miniPieLine in miniPieOutput)
            {
                builder.OpenElement(++seq, "span");
                builder.AddAttribute(++seq, "class", "MiniPie");
                //string miniPieLineBreak = miniPieLine + "<br />";
                builder.AddMarkupContent(++seq, miniPieLine);
                builder.CloseElement();
            }

*/
            builder.OpenElement(++seq, "span");
            builder.AddAttribute(++seq, "class", "MiniPie");
            builder.AddMarkupContent(++seq, miniPieOutput);
            builder.CloseElement();
/*            
            builder.CloseElement();
            builder.CloseElement();
*/
        }
    }
}
