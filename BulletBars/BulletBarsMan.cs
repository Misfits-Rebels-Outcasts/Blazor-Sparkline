
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
    public class BulletBarsMan :ComponentBase
    {
        [Parameter]
        public string InputData { get; set; }
        [Parameter]
        public string Target { get; set; }
        [Parameter]
        public string Actual { get; set; }

        private string[] sparklineOutput;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            BulletBars bulletbars = new BulletBars();
            bulletbars.InputData = InputData;
            bulletbars.Actual = Actual;
            bulletbars.Target = Target;
            sparklineOutput = bulletbars.Encode();

            builder.OpenElement(seq, "figure");
            builder.OpenElement(++seq, "div");
            builder.AddAttribute(++seq, "class", "bulletbars-main");

            //string startStr = "<span style=\"font-family:sans-serif;font-size:16\">"+inputDataArr[0]+ "</span>&nbsp;" + sparklineStr + " &nbsp;" + "&nbsp;<span style=\"font-family:sans-serif;font-size:16\">" + inputDataArr[inputDataArr.Length-1]+"</span>"+ "&nbsp;";
            //sparklineStr = sparklineStr + "<span style=\"font-family:sans-serif;font-size:14\">&nbsp;[" + min.ToString() + "," + max.ToString() + "] </span><br />";

            foreach (string sline in sparklineOutput)
            {

                builder.OpenElement(++seq, "span");
                builder.AddAttribute(++seq, "class", "BulletBars");
                string slineu = sline + "<br />";
                builder.AddMarkupContent(++seq, slineu);
                builder.CloseElement();
            }
            builder.CloseElement();
            builder.CloseElement();

        }
    }
}
