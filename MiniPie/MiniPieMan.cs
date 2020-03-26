/*
 * ConnectCode
 *
 * Copyright (c) 2006-2019 ConnectCode Pte Ltd (https://www.webassemblyman.com)
 * All Rights Reserved.
 *
 * This source code is protected by International Copyright Laws. You are only allowed to modify
 * and include the source in your application if you have purchased a Distribution License.
 *
 * https://www.webassemblyman.com
 *
 */

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
    public class MiniPieMan :ComponentBase
    {
        [Parameter]
        public string InputData { get; set; }

        private string[] sparklineOutput;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            MiniPie miniPie = new MiniPie();
            miniPie.InputData = InputData;
            sparklineOutput = miniPie.Encode();

            builder.OpenElement(seq, "figure");
            builder.OpenElement(++seq, "div");
            builder.AddAttribute(++seq, "class", "minipie-main");

            foreach (string sline in sparklineOutput)
            {

                builder.OpenElement(++seq, "span");
                builder.AddAttribute(++seq, "class", "MiniPie");
                string slineu = sline + "<br />";
                builder.AddMarkupContent(++seq, slineu);
                builder.CloseElement();
            }
            builder.CloseElement();
            builder.CloseElement();

        }
    }
}
