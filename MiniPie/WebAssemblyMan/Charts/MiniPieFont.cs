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

        public int NumLines { get; set; }
        public string[] result;

        public MiniPieFont() { }
        public string Encode()
        {
            //Add verification code in the future for all parameters
            //Or maybe change width etc.. to a number
            /*
            DrawMiniPies();
            return result;
            */
            return DrawMiniPie(InputData);
            //return miniPieStr;
        }
/*
        private void DrawMiniPies()
        {
            //not very optimal to be improved.
            string[] inputDataArr = InputData.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            result = new string[inputDataArr.Length];
            NumLines = inputDataArr.Length;
            int x = 0;
           
            foreach (string inputLine in inputDataArr)
            {
                if (inputLine.IndexOfAny(new char[] { '0', '1','2','3','4','5','6','7','8','9' }) >= 0)
                {
                    string sparkStr = DrawMiniPie(inputLine);
                    result[x++] = sparkStr;
                }
            }            
        }
*/
        //private string[] colors = { "#ce4b99", "#27A844", "#377bbc","#fe2712", "#fc600a", "#fb9902","#fccc1a", "#fefe33", "#b2d732", "#66b032", "#347c98", "#0247fe", "#4424d6","#8601af","#c21460" };
        //private string[] colors = { "#27A844", "#377bbc","#fe2712", "#fc600a", "#fb9902","#fccc1a", "#fefe33", "#b2d732", "#66b032", "#347c98", "#0247fe", "#4424d6","#8601af","#c21460" };

        private int segmentCounter=1;

        private string DrawMiniPie(string inputLine)
        {
            string[] inputDataArr = inputLine.Split(',');
            //string miniPieStr = "<span style=color:#ce4b99>"+"&#" + (65).ToString() + ";"+ "</span>";
            segmentCounter=1;
            string miniPieStr = GetPieSegment(65);
            int inputLen=inputDataArr.Length;
            if (inputLen>6)
                inputLen=6;

            for (int x = 0; x < inputDataArr.Length; x++)
            {
                Console.WriteLine("MiniPie:"+inputDataArr[x]);
                int val;
                bool isInt = int.TryParse(inputDataArr[x],out val);
                if (!isInt)
                    return "";

                if (val > 0 && val <100)
                {
                    /*
                    string charStr = "<span style=color:"+colors[segmentCounter++]+">&#" + (GetRangeBase(val)).ToString() +";"+ "</span>";
                    miniPieStr = miniPieStr +  charStr;                       
                    */
                    miniPieStr = miniPieStr + GetPieSegment(GetRangeBase(val));
                }
                else if (val == 100)
                {
                    /*
                    string charStr = "<span style=color:"+colors[segmentCounter++]+">&#" + (122).ToString() +";"+ "</span>";
                    miniPieStr = miniPieStr +  charStr;                       
                    */
                    miniPieStr = miniPieStr + GetPieSegment(122);
                }
                //if (segmentCounter>colors.Length-1)
                //        segmentCounter=0;

            }

            miniPieStr = miniPieStr +"&#" + (105).ToString() + ";";
            return miniPieStr;
        }

        private int GetRangeBase(int percentValue)
        {
            int fortyvalues=(int)(percentValue/2.5);
            return 65+fortyvalues;
        }

        private string GetPieSegment(int fontValue)
        {
            //return "<span style=color:"+colors[segmentCounter++]+">&#" + (fontValue).ToString() +";"+ "</span>";
            return "<span class=seg-"+(segmentCounter++).ToString()+">&#" + (fontValue).ToString() +";"+ "</span>";
        }
    }
}
