using WebAssemblyMan.Charts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAssemblyMan.Charts
{
  public class MiniPieFont
  {
        public string InputData { get; set; }
        public bool GenerateText { get; set; }
        public int NumLines { get; set; }
        public string[] result;
        public double Min { get; set; }
        public double Max { get; set; }

        public MiniPieFont() { }
        public string Encode()
        {
            return DrawMiniPie(InputData);
        }

        private int segmentCounter=1;

        private string DrawMiniPie(string inputLine)
        {
            string[] inputDataArr = inputLine.Split(',');
            segmentCounter=1;
            string miniPieStr = GetPieSegment(65);
            int inputLen=inputDataArr.Length;
            if (inputLen>6)
                inputLen=6;

            Min = double.MaxValue;
            Max = double.MinValue;

            for (int x = 0; x < inputDataArr.Length; x++)
            {
                Console.WriteLine("MiniPie:"+inputDataArr[x]);

                double valInput;
                bool isDouble = double.TryParse(inputDataArr[x],out valInput);
                int val=0;
                if (isDouble)
                    val=(int)Math.Round(valInput,MidpointRounding.AwayFromZero);

                if (x>0)
                {
                    //double valP = double.Parse(inputDataArr[x-1]);
                    double valP = 0;
                    bool isDouble1=double.TryParse(inputDataArr[x-1],out valP);
                    //double valC = double.Parse(inputDataArr[x]);
                    double valC = 0;
                    bool isDouble2=double.TryParse(inputDataArr[x],out valC);

                    double valD=valC-valP;
                    if (isDouble1==false && isDouble2==false)
                        valD=0;
                        
                    if (Min > valD)
                        Min = valD;
                    if (Max < valD)
                        Max = valD;

                }

                //if (!isInt)
                //    return "";

                if (val > 0 && val <100)
                {
                    miniPieStr = miniPieStr + GetPieSegment(GetRangeBase(val));
                }
                else if (val == 100)
                {
                    miniPieStr = miniPieStr + GetPieSegment(122);
                }

            }

            miniPieStr = miniPieStr +"&#" + (105).ToString() + ";";

            if (GenerateText)
            {
                string minMaxStr = "<span class=text-min-max> [" + Min.ToString() + "," + Max.ToString() + "] </span>";
                miniPieStr = miniPieStr + minMaxStr;
            }
 
            return miniPieStr;
        }

        private int GetRangeBase(int percentValue)
        {
            int fortyvalues=(int)(percentValue/2.5);
            return 65+fortyvalues;
        }

        private string GetPieSegment(int fontValue)
        {
            return "<span class=seg-"+(segmentCounter++).ToString()+">&#" + (fontValue).ToString() +";"+ "</span>";
        }
    }
}
