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
  public class MiniPie
  {
        public string InputData { get; set; }

        public int NumLines { get; set; }
        public string[] result;

        public MiniPie() { }
        public string[] Encode()
        {
            //Add verification code in the future for all parameters
            //Or maybe change width etc.. to a number
            DrawMiniPies();
            return result;
            //return sparklineStr;
        }

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

        //private string[] colors={"#ce4b99","#dddd99","#ffee99","#aaabf9","#bbbb99"};
        //private string[] colors = { "#ce4b99", "#27A844", "#377bbc","#fe2712", "#fc600a", "#fb9902","#fccc1a", "#fefe33", "#b2d732", "#66b032", "#347c98", "#0247fe", "#4424d6","#8601af","#c21460" };
        private string[] colors = { "#27A844", "#377bbc","#fe2712", "#fc600a", "#fb9902","#fccc1a", "#fefe33", "#b2d732", "#66b032", "#347c98", "#0247fe", "#4424d6","#8601af","#c21460" };

        private int colorCounter=0;

        private string DrawMiniPie(string inputLine)
        {
            string[] inputDataArr = inputLine.Split(',');
            string sparklineStr = "<span style=color:#ce4b99>"+"&#" + (65).ToString() + ";"+ "</span>";
            colorCounter=0;
            for (int x = 0; x < inputDataArr.Length; x++)
            {
                int val = int.Parse(inputDataArr[x]);

                if (val > 0 && val <100)
                {
                    string charStr = "<span style=color:"+colors[colorCounter++]+">&#" + (GetRangeBase(val)).ToString() +";"+ "</span>";
                    /*
                    string charStr = "<span style=color:#ce4b99>"+"&#" + (GetRangeBase(val)).ToString() +";"+ "</span>";
                    if (val>30)
                        charStr = "<span style=color:#dddd99>"+"&#" + (GetRangeBase(val)).ToString() +";"+ "</span>";
                    if (val>50)
                        charStr = "<span style=color:#ffee99>"+"&#" + (GetRangeBase(val)).ToString() +";"+ "</span>";
                    if (val>95)
                        charStr = "<span style=color:#aaabf9>"+"&#" + (GetRangeBase(val)).ToString() +";"+ "</span>";
                    */
                    sparklineStr = sparklineStr +  charStr;                       
                }
                else if (val == 100)
                {
                    string charStr = "<span style=color:"+colors[colorCounter++]+">&#" + (122).ToString() +";"+ "</span>";
                    //string charStr = "<span style=color:#bbbb99>"+"&#" + (122).ToString() +";"+ "</span>";
                    sparklineStr = sparklineStr +  charStr;                       
                }
                if (colorCounter>colors.Length-1)
                        colorCounter=0;

            }

            sparklineStr = sparklineStr +"&#" + (105).ToString() + ";";
            return sparklineStr;
/*
Public Function SparkCodePiesHelper(ByVal workRange As Range) As String

            Dim newStrX As String
            Dim c As Variant
            Dim lastValue As Integer
            
            newStrX = VBA.ChrW(65)
            For Each c In workingRow.Cells
                'Fix for string in cells
                If (IsNumeric(c.value)) Then
                If (c.value > 0) And (c.value < 100) Then
                    newStrX = newStrX + VBA.ChrW(GetRangeBase(c.value))
                    lastValue = c.value
                ElseIf (c.value = 100) Then
                    newStrX = newStrX + VBA.ChrW(122) 'Nothing Character for coloring
                End If
                End If
            Next c
            
            SparkCodePiesHelper = newStrX + VBA.ChrW(105)

End Function

Private Function GetRangeBase(PercentValue As Integer)
    'Assuming PercentValue is from 1..100
    Dim fortyvalues As Integer
    fortyvalues = Round(PercentValue / 2.5, 0)
    GetRangeBase = 65 + fortyvalues
    
End Function
*/
        }

        private int GetRangeBase(int percentValue)
        {
            int fortyvalues=(int)(percentValue/2.5);
            return 65+fortyvalues;
        }
    }
}
