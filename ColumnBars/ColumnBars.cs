
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
    public class ColumnBars :ComponentBase
    {
        [Parameter]
        public string InputData { get; set; }
        [Parameter]
        public bool GenerateText { get; set; }

        private string columnBarsOutput;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            ColumnBarsFont columnBarsFont = new ColumnBarsFont();
            columnBarsFont.GenerateText=GenerateText;
            columnBarsFont.InputData = InputData;
            columnBarsOutput = columnBarsFont.Encode();

            builder.OpenElement(++seq, "span");
            builder.AddAttribute(++seq, "class", "ColumnBars");
            builder.AddMarkupContent(++seq, columnBarsOutput);
            builder.CloseElement();

        }
    }
}
