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
  public class BulletBarsFont
  {
        public string InputData { get; set; }
        public string Actual { get; set; }
        public string Target { get; set; }
        public bool GenerateText { get; set; }

        public double Min { get; set; }
        public double Max { get; set; }
        public int NumLines { get; set; }
        public string result;

        public BulletBarsFont() { }
        public string Encode()
        {
            return DrawBulletBarActualTarget(InputData);
        }

        //private string[] colors={"#7F7F7F","#A5A5A5","#BFBFBF","#D8D8D8","#F2F2F2"};
        private int segmentCounter=1;
        private string DrawHorizontalBar(string inputLine)
        {
            string bulletStr = "";
            string[] inputDataArr = inputLine.Split(',');
            int lastValue=62052;
            //string charStr="";
            segmentCounter=0;
            for (int x = 0; x < inputDataArr.Length; x++)
            {
                int val = int.Parse(inputDataArr[x]);
                Console.WriteLine("val:"+val);
                if (val > 0 && val <=100)
                {
                    //bulletStr = bulletStr+"&#" + (GetRangeBase(val)).ToString() +";";
                    //charStr = "<span style=color:"+colors[segmentCounter++]+">"+"&#" + (GetRangeBase(val)).ToString() +";"+ "</span>";
                    bulletStr = GetBarString(val,segmentCounter++)+bulletStr;
                    //bulletStr = charStr+bulletStr;
                    //if (segmentCounter>colors.Length-1)
                    //    segmentCounter=0;
                    lastValue = val;
                }
            }

            //charStr = "<span style=color:"+colors[segmentCounter++]+">"+"&#" + (GetSpaceBase(lastValue)).ToString() +";"+ "</span>";
            bulletStr = bulletStr+GetSpaceString(lastValue);
            //bulletStr = bulletStr+charStr;

            return bulletStr;
        }

        private string DrawBulletBarActualTarget(string inputLine)
        {
            string bulletStr = "";
            string[] inputDataArr = inputLine.Split(',');
            int lastValue=62052;
            //string charStr="";
            segmentCounter=1;

            //Background Bars
            for (int x = 0; x < inputDataArr.Length; x++)
            {
                int val = int.Parse(inputDataArr[x]);
                Console.WriteLine("val:"+val);
                if (val > 0 && val <=100)
                {
                    //bulletStr = bulletStr+"&#" + (GetRangeBase(val)).ToString() +";";
                    //charStr = "<span style=color:"+colors[segmentCounter++]+">"+"&#" + (GetRangeBase(val)).ToString() +";"+ "</span>";
                    bulletStr = GetBarString(val,segmentCounter++)+bulletStr;
                    //if (segmentCounter>colors.Length-1)
                    //    segmentCounter=0;
                    lastValue = val;
                }
            }

            //Actual
            int actual=int.Parse(Actual);
            if ((actual > 0) && (actual <= 100)) 
            {
                //Find the largest space
                if (lastValue < actual) 
                    lastValue = actual;
                //charStr = "<span style=color:#000000>"+"&#" + (GetActualBase(actual)).ToString() +";"+ "</span>";
                bulletStr = bulletStr + GetActualString(actual);
            }
            else if (actual <= 0) 
            {
                //'Use the minimun
                //charStr = "<span style=color:#000000>"+"&#" + (GetActualBase(1)).ToString() +";"+ "</span>";
                bulletStr = bulletStr + GetActualString(1);
            }
            else if (actual > 100) 
            {
                //'Use the maximun
                //charStr = "<span style=color:#000000>"+"&#" + (GetActualBase(100)).ToString() +";"+ "</span>";
                bulletStr = bulletStr + GetActualString(100);
                lastValue = 100;
            }
        
            //Target
            //Find the largest space
            int target=int.Parse(Target);
            if ((target > 0) && (target <= 100))
            {
                if (lastValue < target) 
                    lastValue = target;
                //charStr = "<span style=color:#000000>"+"&#" + (GetTargetBase(target)).ToString() +";"+ "</span>";
                bulletStr = bulletStr + GetTargetString(target);
                //newStrX = newStrX + VBA.ChrW(GetTargetBase(targetRange.Cells(1, 1)))
            }
            else if (target <= 0) 
            {
                //'Use the minimun
                //charStr = "<span style=color:#000000>"+"&#" + GetTargetBase(1).ToString() +";"+ "</span>";
                bulletStr = bulletStr + GetTargetString(1);
                //newStrX = newStrX + VBA.ChrW(GetTargetBase(1))
            }
            else if (target > 100) 
            {
                //'Use the maximun
                //charStr = "<span style=color:#000000>"+"&#" + GetTargetBase(100).ToString() +";"+ "</span>";
                bulletStr = bulletStr + GetTargetString(100);
                //newStrX = newStrX + VBA.ChrW(GetTargetBase(100))
                lastValue = 100;
            }
            
            //charStr = "<span style=color:"+colors[segmentCounter++]+">"+"&#" + (GetSpaceBase(lastValue)).ToString() +";"+ "</span>";            
            bulletStr = bulletStr+GetSpaceString(lastValue);
            if (GenerateText)
                bulletStr = bulletStr+GetActualTargetText();            

            return bulletStr;
        }

        private string GetActualTargetText()
        {
            return "<span class=text-actual-target>&nbsp;["+ Actual+","+Target+ "]</span>";
        }

        private string GetSpaceString(int percentValue)
        {
            //return "<span class=bar-space>"+"&#" + GetSpaceBase(percentValue).ToString() +";"+ "</span>";
            return "&#" + GetSpaceBase(percentValue).ToString() +";";
        }

        private string GetTargetString(int percentValue)
        {
            return "<span class=bar-target>"+"&#" + GetTargetBase(percentValue).ToString() +";"+ "</span>";
        }

        private string GetActualString(int percentValue)
        {
            return "<span class=bar-actual>"+"&#" + GetActualBase(percentValue).ToString() +";"+ "</span>";
        }

        private string GetBarString(int percentValue,int segmentCounter)
        {
            return "<span class=bar-"+segmentCounter.ToString()+">"+"&#" + GetRangeBase(percentValue).ToString() +";"+ "</span>";
        }

        private int GetRangeBase(int percentValue)
        {
            return 61641 - percentValue;
        }

        private int GetActualBase(int percentValue)
        {
            return 62153 - percentValue;
        }

        private int GetSpaceBase(int percentValue)
        {
            return 62052 - percentValue;
        }

        private int GetTargetBase(int percentValue)
        {
            if (percentValue == 100)
                return 117;
            else if (percentValue == 99) 
                return 118;
            else if (percentValue == 98) 
                return 119;
            else if (percentValue == 97) 
                return 120;
            else if (percentValue == 96) 
                return 121;
            else if (percentValue == 95)
                return 122;
            else
                return 254 - percentValue;
        }

    }
}
