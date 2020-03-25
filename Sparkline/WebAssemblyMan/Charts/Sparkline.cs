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
  public class Sparkline
  {
        public string InputData { get; set; }
        public string StartColor { get; set; }
        public string StopColor { get; set; }
        public string SegmentWidth { get; set; }

        public double Min { get; set; }
        public double Max { get; set; }
        public int NumLines { get; set; }
        public string[] result;

        public Sparkline() { }
        public string[] Encode()
        {
            //Add verification code in the future for all parameters
            //Or maybe change width etc.. to a number
            DrawSparklines();
            return result;
            //return sparklineStr;
        }

        private void DrawSparklines()
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
                    string sparkStr = DrawSparkline(inputLine);
                    result[x++] = sparkStr;
                }
            }
            
        }

        private string DrawSparkline(string inputLine)
        {
            double min = double.MaxValue, max = double.MinValue;
            string[] inputDataArr = inputLine.Split(',');
            string sparklineStr = "";

            for (int x = 0; x < inputDataArr.Length; x++)
            {
                double val = double.Parse(inputDataArr[x]);
                min = val;
                if (max < val)
                    max = val;
            }
            Min = min;
            Max = max;
            //Console.WriteLine("min:" + min);
            //Console.WriteLine("max:" + max);
            for (int y = 0; y < inputDataArr.Length; y++)
            {
                if (y < inputDataArr.Length - 1)
                {
                    double pcy0 = 0, pcy1 = 0;
                    pcy0 = double.Parse(inputDataArr[y]) / max * 100;
                    pcy1 = double.Parse(inputDataArr[y + 1]) / max * 100;
                    if (y == 0)
                        sparklineStr=drawLine(pcy0, pcy1, 0, sparklineStr);
                    else if (y + 1 == inputDataArr.Length - 1)
                        sparklineStr=drawLine(pcy0, pcy1, 1, sparklineStr);
                    else
                        sparklineStr=drawLine(pcy0, pcy1, -1, sparklineStr);
                }
            }
            //Console.WriteLine("*---->:" + sparklineStr);

            return sparklineStr;
        }

        string drawLine(double y0, double y1, int startStop,string sparklineStr)
        {
            int width = int.Parse(SegmentWidth);
            y0 = Math.Round(y0 / 2);
            y1 = Math.Round(y1 / 2);

            sparklineStr=line(0, (int)y0, width, (int)y1, startStop, sparklineStr);
            return sparklineStr;
        }

        //Bresenham Algorithm
        string line(int x0, int y0, int x1, int y1, int startStop, string sparklineStr)
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

                if (startStop == 0)
                {
                    if (firstTime)
                    {
                        firstTime = false;
                        string startStr = "<span style=\"font-family:sans-serif;font-size:14\">" + y0.ToString() + "</span><span style=\"font-family:sans-serif;font-size:14\">&nbsp;&nbsp;</span>";

                        string charStr = "&#" + (y0 + 64090).ToString() + ";";
                        sparklineStr = sparklineStr+startStr+"<span style=\"color:"+StopColor+"\">"+charStr+"</span>";

                        //sparklineStr = sparklineStr + "&#" + (y0 + 64090).ToString() + ";";
                        //Console.WriteLine("SSparkline:" + sparklineStr);
                    }
                }

                if (x0 > px)
                    sparklineStr = sparklineStr + "&#" + (y0 + 48).ToString() + ";"; //shift width     
                else
                    sparklineStr = sparklineStr + "&#" + (y0 + 176).ToString() + ";"; //noshift width     

                px = x0;

                if ((x0 == x1) && (y0 == y1)) break;
                var e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x0 += sx; }
                if (e2 < dx) { err += dx; y0 += sy; }
            }
            if (startStop == 1)
            {
                string stopStr = "<span style=\"font-family:sans-serif;font-size:14\">&nbsp;&nbsp;" + y0.ToString() + "&nbsp;</span>";
                string charStr = "&#" + (y0 + 64090).ToString() + ";";
                string minMaxStr = "<span style=\"font-family:sans-serif;font-size:14\">&nbsp;[" + Min.ToString() + "," + Max.ToString() + "] </span>";

                sparklineStr = sparklineStr + "<span style=\"color:" + StartColor + "\">" + charStr + "</span>" + stopStr +minMaxStr;

                //sparklineStr = sparklineStr + "&#" + (y0 + 64090).ToString(); //stop     
            }
            return sparklineStr;
        }
    }
}
