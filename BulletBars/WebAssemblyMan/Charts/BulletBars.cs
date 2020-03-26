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
  public class BulletBars
  {
        public string InputData { get; set; }
        public string Actual { get; set; }
        public string Target { get; set; }

        public double Min { get; set; }
        public double Max { get; set; }
        public int NumLines { get; set; }
        public string[] result;

        public BulletBars() { }
        public string[] Encode()
        {
            //Add verification code in the future for all parameters
            //Or maybe change width etc.. to a number
            DrawBulletBars();            
            return result;
            //return sparklineStr;
        }

        private void DrawBulletBars()
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
                    //string sparkStr = DrawBulletBar(inputLine);
                    string sparkStr = DrawBulletBarActualTarget(inputLine);
                    
                    result[x++] = sparkStr;
                }
            }
            
        }

        //private string[] colors={"#000000","#7F7F7F","#A5A5A5","#BFBFBF","#D8D8D8","#F2F2F2"};
        private string[] colors={"#7F7F7F","#A5A5A5","#BFBFBF","#D8D8D8","#F2F2F2"};
        private int colorCounter=0;
        private string DrawBulletBar(string inputLine)
        {
            string sparklineStr = "";
            string[] inputDataArr = inputLine.Split(',');
            int lastValue=62052;
            string charStr="";
            colorCounter=0;
            for (int x = 0; x < inputDataArr.Length; x++)
            {
                int val = int.Parse(inputDataArr[x]);
                Console.WriteLine("val:"+val);
                if (val > 0 && val <=100)
                {
                    //sparklineStr = sparklineStr+"&#" + (GetRangeBase(val)).ToString() +";";
                    charStr = "<span style=color:"+colors[colorCounter++]+">"+"&#" + (GetRangeBase(val)).ToString() +";"+ "</span>";
                    sparklineStr = charStr+sparklineStr;
                    if (colorCounter>colors.Length-1)
                        colorCounter=0;
                    lastValue = val;
                }
            }

            charStr = "<span style=color:"+colors[colorCounter++]+">"+"&#" + (GetSpaceBase(lastValue)).ToString() +";"+ "</span>";
            sparklineStr = sparklineStr+charStr;

            return sparklineStr;
        }

        private string DrawBulletBarActualTarget(string inputLine)
        {
            string sparklineStr = "";
            string[] inputDataArr = inputLine.Split(',');
            int lastValue=62052;
            string charStr="";
            colorCounter=0;
            for (int x = 0; x < inputDataArr.Length; x++)
            {
                int val = int.Parse(inputDataArr[x]);
                Console.WriteLine("val:"+val);
                if (val > 0 && val <=100)
                {
                    //sparklineStr = sparklineStr+"&#" + (GetRangeBase(val)).ToString() +";";
                    charStr = "<span style=color:"+colors[colorCounter++]+">"+"&#" + (GetRangeBase(val)).ToString() +";"+ "</span>";
                    sparklineStr = charStr+sparklineStr;
                    if (colorCounter>colors.Length-1)
                        colorCounter=0;
                    lastValue = val;
                }
            }

            int actual=int.Parse(Actual);
            if ((actual > 0) && (actual <= 100)) 
            {
                //Find the largest space
                if (lastValue < actual) 
                    lastValue = actual;
                charStr = "<span style=color:#000000>"+"&#" + (GetActualBase(actual)).ToString() +";"+ "</span>";
                sparklineStr = sparklineStr + charStr;
            }
            else if (actual <= 0) 
            {
                //'Use the minimun
                charStr = "<span style=color:#000000>"+"&#" + (GetActualBase(1)).ToString() +";"+ "</span>";
                sparklineStr = sparklineStr + charStr;
            }
            else if (actual > 100) 
            {
                //'Use the maximun
                charStr = "<span style=color:#000000>"+"&#" + (GetActualBase(100)).ToString() +";"+ "</span>";
                sparklineStr = sparklineStr + charStr;
                lastValue = 100;
            }
        
            //Target
            //Find the largest space
            int target=int.Parse(Target);
            if ((target > 0) && (target <= 100))
            {
                if (lastValue < target) 
                    lastValue = target;
                charStr = "<span style=color:#000000>"+"&#" + (GetTargetBase(target)).ToString() +";"+ "</span>";
                sparklineStr = sparklineStr + charStr;
                //newStrX = newStrX + VBA.ChrW(GetTargetBase(targetRange.Cells(1, 1)))
            }
            else if (target <= 0) 
            {
                //'Use the minimun
                charStr = "<span style=color:#000000>"+"&#" + GetTargetBase(1).ToString() +";"+ "</span>";
                sparklineStr = sparklineStr + charStr;
                //newStrX = newStrX + VBA.ChrW(GetTargetBase(1))
            }
            else if (target > 100) 
            {
                //'Use the maximun
                charStr = "<span style=color:#000000>"+"&#" + GetTargetBase(100).ToString() +";"+ "</span>";
                sparklineStr = sparklineStr + charStr;
                //newStrX = newStrX + VBA.ChrW(GetTargetBase(100))
                lastValue = 100;
            }
            
            charStr = "<span style=color:"+colors[colorCounter++]+">"+"&#" + (GetSpaceBase(lastValue)).ToString() +";"+ "</span>";
            sparklineStr = sparklineStr+charStr;

            return sparklineStr;
        }


/*
Public Function SparkCodeBulletActualTargetHelper(ByVal workRange As Range, ByVal actualRange As Range, ByVal targetRange As Range) As String

            Dim newStrX As String
            Dim c As Variant
            Dim lastValue As Integer
            
            newStrX = ""
            For Each c In workingRow.Cells
                'Fix for string in cells
                If (IsNumeric(c.value)) Then
                If (c.value > 0) And (c.value <= 100) Then
                    newStrX = VBA.ChrW(GetRangeBase(c.value)) + newStrX
                    lastValue = c.value
                End If
                End If
            Next c
            
            'Actual
            If (actualRange.Cells(1, 1) > 0) And (actualRange.Cells(1, 1) <= 100) Then
                'Find the largest space
                If (lastValue < actualRange.Cells(1, 1)) Then
                    lastValue = actualRange.Cells(1, 1)
                End If
                newStrX = newStrX + VBA.ChrW(GetActualBase(actualRange.Cells(1, 1)))
            ElseIf (actualRange.Cells(1, 1) <= 0) Then
                'Use the minimun
                newStrX = newStrX + VBA.ChrW(GetActualBase(1))
            ElseIf (actualRange.Cells(1, 1) > 100) Then
                'Use the maximun
                newStrX = newStrX + VBA.ChrW(GetActualBase(100))
                lastValue = 100
            End If
            
            'Target
            'Find the largest space
            If (targetRange.Cells(1, 1) > 0) And (targetRange.Cells(1, 1) <= 100) Then
                If (lastValue < targetRange.Cells(1, 1)) Then
                    lastValue = targetRange.Cells(1, 1)
                End If
                newStrX = newStrX + VBA.ChrW(GetTargetBase(targetRange.Cells(1, 1)))
            ElseIf (targetRange.Cells(1, 1) <= 0) Then
                'Use the minimun
                newStrX = newStrX + VBA.ChrW(GetTargetBase(1))
            ElseIf (targetRange.Cells(1, 1) > 100) Then
                'Use the maximun
                newStrX = newStrX + VBA.ChrW(GetTargetBase(100))
                lastValue = 100
            End If
            
            SparkCodeBulletActualTargetHelper = newStrX + VBA.ChrW(GetSpaceBase(lastValue))

End Function
*/

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
