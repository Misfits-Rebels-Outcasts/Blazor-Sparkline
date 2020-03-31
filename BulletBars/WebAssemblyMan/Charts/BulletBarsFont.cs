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

        private int segmentCounter=1;
        private string DrawHorizontalBar(string inputLine)
        {
            string bulletStr = "";
            string[] inputDataArr = inputLine.Split(',');
            int lastValue=62052;
            segmentCounter=0;
            for (int x = 0; x < inputDataArr.Length; x++)
            {
                int val = int.Parse(inputDataArr[x]);
                Console.WriteLine("val:"+val);
                if (val > 0 && val <=100)
                {
                    bulletStr = GetBarString(val,segmentCounter++)+bulletStr;
                    lastValue = val;
                }
            }

            bulletStr = bulletStr+GetSpaceString(lastValue);

            return bulletStr;
        }

        private string DrawBulletBarActualTarget(string inputLine)
        {
            string bulletStr = "";
            string[] inputDataArr = inputLine.Split(',');
            int lastValue=62052;
            segmentCounter=1;
            bool isDouble=false;
            //Background Bars
            for (int x = 0; x < inputDataArr.Length; x++)
            {
                //int val = int.Parse(inputDataArr[x]);
                double valInput;
                isDouble = double.TryParse(inputDataArr[x],out valInput);
                int val=0;
                if (isDouble)
                    val=(int)Math.Round(valInput,MidpointRounding.AwayFromZero);

                Console.WriteLine("val:"+val);
                if (val > 0 && val <=100)
                {
                    bulletStr = GetBarString(val,segmentCounter++)+bulletStr;
                    lastValue = val;
                }
            }

            //Actual
            //int actual=int.Parse(Actual);
            double actualD=0;
            isDouble = double.TryParse(Actual,out actualD);
            int actual=0;
            if (isDouble)
                actual=(int)Math.Round(actualD,MidpointRounding.AwayFromZero);

            if ((actual > 0) && (actual <= 100)) 
            {
                //Find the largest space
                if (lastValue < actual) 
                    lastValue = actual;

                bulletStr = bulletStr + GetActualString(actual);
            }
            else if (actual <= 0) 
            {
                //'Use the minimun
                bulletStr = bulletStr + GetActualString(1);
            }
            else if (actual > 100) 
            {
                //'Use the maximun
                bulletStr = bulletStr + GetActualString(100);
                lastValue = 100;
            }
        
            //Target
            //Find the largest space
            //int target=int.Parse(Target);
            double targetD=0;
            isDouble = double.TryParse(Target,out targetD);
            int target=0;
            if (isDouble)
                target=(int)Math.Round(targetD,MidpointRounding.AwayFromZero);

            if ((target > 0) && (target <= 100))
            {
                if (lastValue < target) 
                    lastValue = target;
                bulletStr = bulletStr + GetTargetString(target);
            }
            else if (target <= 0) 
            {
                //'Use the minimun
                bulletStr = bulletStr + GetTargetString(1);
            }
            else if (target > 100) 
            {
                //'Use the maximun
                bulletStr = bulletStr + GetTargetString(100);
                lastValue = 100;
            }
            
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
