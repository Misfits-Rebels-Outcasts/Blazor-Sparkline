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
  public class WinLossFont
  {
        public string InputData { get; set; }
        public bool GenerateText { get; set; }

        public int NumLines { get; set; }
        public string result;

        private int numWin;
        private int numDraw;
        private int numLoss;
        
        public WinLossFont() { 
            numWin=0;
            numDraw=0;
            numLoss=0;
            
        }
        public string Encode()
        {
            return DrawWinLoss(InputData);
        }
        private string DrawWinLoss(string inputLine)
        {
            string[] inputDataArr = inputLine.Split(',');
            string winLossStr = "";

            for (int x = 0; x < inputDataArr.Length; x++)
            {
                //double val = double.Parse(inputDataArr[x]);
                double val = 0;
                double.TryParse(inputDataArr[x],out val);

                if (val > 0)
                {
                    numWin++;
                    string barStr = "<span class=bar-win>"+"&#" + (108).ToString() +";"+ "</span>";
                    winLossStr = winLossStr +  barStr;                       
                }
                else if (val < 0)
                {
                    numLoss++;
                    string barStr = "<span class=bar-loss>"+"&#" + (106).ToString() +";"+ "</span>";
                    winLossStr = winLossStr +  barStr;                       
                }
                else
                {
                    numDraw++;
                    string barStr = "<span class=bar-draw>"+"&#" + (107).ToString() +";"+ "</span>";
                    winLossStr = winLossStr +  barStr;                       
                }

            }

            if (GenerateText)
            {
                string numStr = "<span class=text-win-loss> [" + numWin.ToString() + "," + numDraw.ToString() + "," + numLoss.ToString() + "] </span>";
                winLossStr = winLossStr + numStr;
            }

            return winLossStr;

        }

    }
}
