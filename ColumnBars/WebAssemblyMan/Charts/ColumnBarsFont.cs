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
  public class ColumnBarsFont
  {
        public string InputData { get; set; }
        public string Actual { get; set; }
        public string Target { get; set; }
        public bool GenerateText {get; set;}
        public double Min { get; set; }
        public double Max { get; set; }
        public int NumLines { get; set; }
        public string[] result;

        public ColumnBarsFont() { }
        public string Encode()
        {
            //Add verification code in the future for all parameters
            //Or maybe change width etc.. to a number
            //DrawColumnBars();            

            //**********TODO***********
            //'scaling 0 => Normal , 1 => Maps to 0 to 100, 2=> Maps to 0 to -100
            int scaling=0;

            int scaleVal;
            scaleVal = -10;
            if (scaling == 0)
                scaleVal = -10;
            else if (scaling == 1)
                scaleVal = 0;
            else if (scaling == 2)
                scaleVal = 5;

            return DrawColumnBar(InputData,scaleVal);
                    
            //return result;
            //return columnBarStr;
        }
        int SparkBarsApplyFontSet(int mapchar, int fontset, int shiftZero)
        {
        //'original mapchar is from 0 to 200

        //'origin at 0% ... 0 (Set 0)
        if (fontset == 0)
        {
            //'using 8 as cutoff to remove 0 point
            if ((mapchar < 8) && (shiftZero == 1))
                mapchar = 8;
            
        //'E000 to E0C8
        mapchar = 0xE000 + mapchar;
        Console.WriteLine("mapchar:"+mapchar);

        }
        //'origin at 20% ... 160 (Set 1)
        else if (fontset == 1)
        {
            //'-160 to 640 range (20%)
            //'C000 to C0A0
            //'A000 to A028
            
            //'last 161 chars
            if (mapchar >= 40)
                mapchar = 0xC000 + mapchar - 40;
            
            else
                mapchar = 0xA028 - mapchar;
            //'1st 40 chars (max index at 39, which correspond to -4, or char A001 => A000 not used)
        }
        //'origin at 40% ... 320 (Set 2)
        else if (fontset == 2)
        {
            //'-320 to 480 range (40%)
            //'BC00 to BC78
            //'9C00 to 9C50
            
            //'last 121 chars
            if (mapchar >= 80) 
                mapchar = 0xBC00 + mapchar - 80;    
            else
                mapchar = 0x9C50 - mapchar;
            
            //'1st 80 chars (max index at 79, which correspond to -4, or char 9C01 => 9C00 not used)
            
        }
        //'origin at 60% ... 480 (Set 3)
        else if (fontset == 3)
        {

            //'-480 to 320 range (60%)
            //'4400 to 4478
            //'B800 to B850
            
            
            //'last 81 chars
            if (mapchar >= 120)
                mapchar = 0xB800 + mapchar - 120;    
            else
                mapchar = 0x4478 - mapchar;
            //'1st 120 chars (max index at 119, which correspond to -4)
                
        }
        //'origin at 80% ... 640 (Set 4)
        else if (fontset == 4)
        {

            //'-640 to 160 range (80%)
            //'8800 to 88A0
            //'B400 to B428
            
            //'last 41 chars
            if (mapchar >= 160) 
                mapchar = 0xB400 + mapchar - 160;    
            else
                mapchar = 0x88A0 - mapchar;
            //'1st 160 chars (max index at 159, which correspond to -4, or char 8801 => 8800 not used)

        }
        //'origin at 100% ... 800 (Set 5)
        else if (fontset == 5)
        {
            //'using 8 as cutoff to remove 0 point
            if ((mapchar > 192) && (shiftZero == 1))
                mapchar = 192;

            //'-800 to 0 range (1000%)
            //'F000 to F0C8
            mapchar = 0xF0C8 - mapchar;

        }

        return mapchar;
        }

        int SparkBarsValue(double value, double rangelow, double rangehigh, int fontset, double userMin, double userMax, int useUser, int shiftZero)
        {

        int mapchar=0;
        double fullrange;
        double mapinterval;
        double mappoint;


        if (useUser == 1)
        {   
            //'not yet
        }
        else
        {

                if (value > rangehigh) 
                {
                //'index to character 201
                mapchar = 200;
                }
                else if (value < rangelow)
                {
                //'index to character 0
                mapchar = 0;
                }
                else
                {
                fullrange = rangehigh - rangelow;
                Console.WriteLine("fr:"+fullrange);

                if (fullrange > 0.0001)
                {
                    mapinterval = 200 / fullrange;
                    mappoint = (value - rangelow) * mapinterval;
                    
                    Console.WriteLine("mappoint:"+mappoint);
                    //'mappoint = mappoint - yAdjust
                    //'MsgBox mappoint
                    
                    //Math.Round(mappoint,mapchar);
                    mapchar=(int)Math.Round(mappoint);
                }          
                else
                    mapchar = 0;

                }
                
        }


        if (mapchar < 0)
        mapchar = 0;

        if (mapchar > 200)
        mapchar = 200;
        Console.WriteLine("Before:"+mapchar);        
        return SparkBarsApplyFontSet(mapchar, fontset, shiftZero);        

        }

        //' fontset 0 to 5 .... use fontset 0(0% origin) to 5 (100% origin)
        //' fontset 0 , positive range 0 to max - default
        //' fontset -10, user defined range of 0 to max
        //' not yet....fontset -20, user defined range of min to max , may need value modification to draw exactly in the range
        //private string[] colors={"#000000","#7F7F7F","#A5A5A5","#BFBFBF","#D8D8D8","#F2F2F2"};
        //private string[] colors={"#7F7F7F","#A5A5A5","#BFBFBF","#D8D8D8","#F2F2F2"};
        //private int colorCounter=0;
        private string DrawColumnBar(string inputLine,int fontset)
        {
            string columnBarStr = "";
            string[] inputDataArr = inputLine.Split(',');
            double minValue=double.MaxValue;
            double maxValue=double.MinValue;
            int actualFontset =fontset;

            int shiftZero;   
            shiftZero = 0;
                
            if (fontset == 0)
                shiftZero = 1;
            else if (fontset == 5)
                shiftZero = 1;

            //determine min max
            for (int x = 0; x < inputDataArr.Length; x++)
            {
                //int val = int.Parse(inputDataArr[x]);
                //double val = double.Parse(inputDataArr[x]);
                double val=0;
                double.TryParse(inputDataArr[x],out val);
                
                if (minValue>val)
                    minValue=val;
                if (maxValue<val)
                    maxValue=val;
            }
            Min=minValue;
            Max=maxValue;

            if (fontset == -10) 
            {
    
                double fullRangex;
                //double zeroPercentile;
                double orgval;
                      
                //'All positive values
                if (minValue > 0) 
                {
                        actualFontset = 0;
                    //'All negative values
                }
                else if (maxValue < 0)
                {
                        actualFontset = 5;
                }
                else
                {
                    //'Select a fontset
                    fullRangex = (maxValue - minValue);
                    if (fullRangex > 0.00001)               
                            actualFontset=(int)Math.Round(((0 - minValue) / fullRangex) * 5);                
                    //else
                            //;//SparkBarsHelper = "";
                            //Exit Function
                    
                    if (actualFontset < 0)
                        actualFontset = 0;
                    else if (actualFontset > 5)
                        actualFontset = 5;
                }
                    
                //'MsgBox actualFontset
                // 'Map Range to the fontset
                // 'Zero need to map to 0
                if (actualFontset == 0)
                {
                        minValue = 0;
                        //'maxvalue = maxvalue
                } 
                else if (actualFontset == 1)
                {
                        //'20 percent
                        //'maxvalue = maxvalue
                        orgval = minValue;
                        minValue = -maxValue / 4;
                        if (minValue > orgval)
                        {
                            minValue = orgval;
                            maxValue = -minValue * 4;
                        }
                }                                             
                else if (actualFontset == 2)
                {                                
                        //'40 percent
                        //'maxvalue = maxvalue
                        orgval = minValue;
                        minValue = -maxValue * 2 / 3;
                        if (minValue > orgval)
                        {
                            minValue = orgval;
                            maxValue = -minValue * 3 / 2;
                        }                 
                }      
                else if (actualFontset == 3)
                {
                        //'60 percent
                        //'maxvalue = maxvalue
                        orgval = minValue;
                        minValue = -maxValue * 3 / 2;
                        if (minValue > orgval)
                        {
                            minValue = orgval;
                            maxValue = -minValue * 2 / 3;
                        }
                }                                             
                else if (actualFontset == 4)
                {
                        //'80 percent
                        //'maxvalue = maxvalue
                        orgval = minValue;
                        minValue = -maxValue * 4;
                        if (minValue > orgval)
                        {
                            minValue = orgval;
                            maxValue = -minValue / 4;
                        }                 
                    }
                    else if (actualFontset == 5)
                    {
                        //'minValue = minValue
                        maxValue = 0;
                    }
                                                          
                }

                if (actualFontset < 0)
                    actualFontset = 0;
                else if (actualFontset > 5)
                    actualFontset = 5;


                for (int x = 0; x < inputDataArr.Length; x++)
                {
                    //int val = int.Parse(inputDataArr[x]);
                    //double val = double.Parse(inputDataArr[x]);
                    double val=0;
                    double.TryParse(inputDataArr[x],out val);

                    int barVal = SparkBarsValue(val, minValue, maxValue, actualFontset, 0, 0, 0, shiftZero);
                    Console.WriteLine(barVal);
                    string barStr = "&#" + (barVal).ToString() +";";
                    //barStr = "<span style=color:#000000>"+"&#" + (barVal).ToString() +";"+ "</span>";
                    if (x==0)
                    {
                        if (GenerateText)
                        {
                            string startStr = "<span class=text-start>"+inputDataArr[x]+"&nbsp;</span>";
                            barStr = startStr+barStr;
                        }
                        barStr = barStr+"<span class=bar-first>"+"&#" + (barVal).ToString() +";"+ "</span>";
                    }
                    else if (x==inputDataArr.Length-1)
                    {
                        barStr = "<span class=bar-last>"+"&#" + (barVal).ToString() +";"+ "</span>";
                        if (GenerateText)
                        {
                            string stopStr = "<span class=text-stop>&nbsp;"+inputDataArr[x]+"</span>";
                            barStr = barStr+stopStr;
                        }
                    }
                    else
                        barStr = "<span class=bar>"+"&#" + (barVal).ToString() +";"+ "</span>";
                    
                    columnBarStr = columnBarStr+barStr;

                }
                if (GenerateText)
                {
                    string minMaxStr = "<span class=text-min-max>&nbsp;[" + Min.ToString() + "," + Max.ToString() + "] </span>";
                    columnBarStr = columnBarStr+minMaxStr;
                }

            return columnBarStr;
        }




    }
}
