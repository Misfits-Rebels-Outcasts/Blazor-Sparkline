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
  public class SparklineFont
  {
        public string InputData { get; set; }
        //public string StartColor { get; set; }
        //public string StopColor { get; set; }
        public bool GenerateText { get; set; }
        public string SegmentWidth { get; set; }

        public double Min { get; set; }
        public double Max { get; set; }
        public int NumLines { get; set; }
        public string result;

        public SparklineFont() { }
        public string Encode()
        {
            return DrawSparkline(InputData);
        }

        private string[] inputDataArr;
        private string DrawSparkline(string inputLine)
        {
            double min = double.MaxValue, max = double.MinValue;
            inputDataArr = inputLine.Split(',');
            string sparklineStr = "";

            for (int x = 0; x < inputDataArr.Length; x++)
            {
                //double val = double.Parse(inputDataArr[x]);
                double val=0;
                double.TryParse(inputDataArr[x],out val);

                if (min > val)
                    min = val;
                if (max < val)
                    max = val;
            }
            Min = min;
            Max = max;

            for (int y = 0; y < inputDataArr.Length; y++)
            {
                if (y < inputDataArr.Length - 1)
                {
                    double pcy0 = 0, pcy1 = 0;
                    //pcy0 = double.Parse(inputDataArr[y]) / max * 100;
                    //pcy1 = double.Parse(inputDataArr[y + 1]) / max * 100;
                    double.TryParse(inputDataArr[y],out pcy0);
                    pcy0=pcy0 / max * 100;
                    double.TryParse(inputDataArr[y+1],out pcy1);
                    pcy1=pcy1 / max * 100;

                    if (y == 0)
                        sparklineStr=drawLine(pcy0, pcy1, 0, sparklineStr,y);
                    else if (y + 1 == inputDataArr.Length - 1)
                        sparklineStr=drawLine(pcy0, pcy1, 1, sparklineStr,y);
                    else
                        sparklineStr=drawLine(pcy0, pcy1, -1, sparklineStr,y);
                }
            }
            //Console.WriteLine("*---->:" + sparklineStr);

            return sparklineStr;
        }

        string drawLine(double y0, double y1, int startStop,string sparklineStr, int index)
        {
            int width = int.Parse(SegmentWidth);
            y0 = Math.Round(y0 / 2);
            y1 = Math.Round(y1 / 2);

            sparklineStr=line(0, (int)y0, width, (int)y1, startStop, sparklineStr,index);
            return sparklineStr;
        }

        //Bresenham Algorithm
        string line(int x0, int y0, int x1, int y1, int startStop, string sparklineStr, int index)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = (x0 < x1) ? 1 : -1;
            int sy = (y0 < y1) ? 1 : -1;
            int err = dx - dy;
            bool firstTime = true;
            var px = 0;

            while (true)
            {
                //start
                if (startStop == 0)
                {
                    if (firstTime)
                    {
                        firstTime = false;

                        string startStr = "<span class=text-start>" + inputDataArr[index].ToString() + "&nbsp;&nbsp;</span>";
                        string charStr = "&#" + (y0 + 64090).ToString() + ";";
                        if (GenerateText)
                            sparklineStr = sparklineStr+startStr+"<span class=sparkline-start>"+charStr+"</span>";
                        else
                            sparklineStr = sparklineStr+"<span class=sparkline-start>"+charStr+"</span>";
                        
                    }
                }

                if (x0 > px)
                {
                    sparklineStr = sparklineStr + "&#" + (y0 + 48).ToString() + ";"; //shift width     
                }
                else
                {
                    sparklineStr = sparklineStr + "&#" + (y0 + 176).ToString() + ";"; //noshift width     
                }

                px = x0;

                if ((x0 == x1) && (y0 == y1)) break;
                var e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x0 += sx; }
                if (e2 < dx) { err += dx; y0 += sy; }
            }
            //stop
            if (startStop == 1)
            {
                string stopStr = "<span class=text-stop>&nbsp;&nbsp;" + inputDataArr[index+1].ToString() + "&nbsp;</span>";                
                string charStr = "&#" + (y0 + 64090).ToString() + ";";
                string minMaxStr = "<span class=text-min-max> [" + Min.ToString() + "," + Max.ToString() + "] </span>";
                if (GenerateText)
                    sparklineStr = sparklineStr + "<span class=sparkline-stop>" + charStr + "</span>" + stopStr +minMaxStr;
                else
                    sparklineStr = sparklineStr + "<span class=sparkline-stop>" + charStr + "</span>";

            }
            return sparklineStr;
        }
    }
}
