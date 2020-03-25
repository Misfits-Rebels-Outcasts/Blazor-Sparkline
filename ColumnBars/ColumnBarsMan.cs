
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
    public class ColumnBarsMan :ComponentBase
    {
        [Parameter]
        public string InputData { get; set; }
        //[Parameter]
        //public string Target { get; set; }
        //[Parameter]
        //public string Actual { get; set; }

        private string[] sparklineOutput;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            ColumnBars columnbars = new ColumnBars();
            columnbars.InputData = InputData;
            //columnbars.Actual = Actual;
            //columnbars.Target = Target;
            sparklineOutput = columnbars.Encode();

            builder.OpenElement(seq, "figure");
            builder.OpenElement(++seq, "div");
            builder.AddAttribute(++seq, "class", "columnbars-main");

            //string startStr = "<span style=\"font-family:sans-serif;font-size:16\">"+inputDataArr[0]+ "</span>&nbsp;" + sparklineStr + " &nbsp;" + "&nbsp;<span style=\"font-family:sans-serif;font-size:16\">" + inputDataArr[inputDataArr.Length-1]+"</span>"+ "&nbsp;";
            //sparklineStr = sparklineStr + "<span style=\"font-family:sans-serif;font-size:14\">&nbsp;[" + min.ToString() + "," + max.ToString() + "] </span><br />";

            foreach (string sline in sparklineOutput)
            {

                builder.OpenElement(++seq, "span");
                builder.AddAttribute(++seq, "class", "ColumnBars");
                string slineu = sline + "<br />";
                builder.AddMarkupContent(++seq, slineu);
                builder.CloseElement();
            }
            builder.CloseElement();
            builder.CloseElement();

        }
    }
}
