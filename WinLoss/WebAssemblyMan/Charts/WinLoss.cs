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
  public class WinLoss
  {
        public string InputData { get; set; }

        public int NumLines { get; set; }
        public string[] result;

        public WinLoss() { }
        public string[] Encode()
        {
            //Add verification code in the future for all parameters
            //Or maybe change width etc.. to a number
            DrawWinsLosses();
            return result;
            //return sparklineStr;
        }

        private void DrawWinsLosses()
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
                    string sparkStr = DrawWinLoss(inputLine);
                    result[x++] = sparkStr;
                }
            }
            
        }

        private string DrawWinLoss(string inputLine)
        {
            string[] inputDataArr = inputLine.Split(',');
            string sparklineStr = "";

            for (int x = 0; x < inputDataArr.Length; x++)
            {
                double val = double.Parse(inputDataArr[x]);
                if (val > 0)
                {
                    string charStr = "&#" + (108).ToString() + ";";
                    sparklineStr = sparklineStr +  charStr;                       
                }
                else if (val < 0)
                {
                    string charStr = "&#" + (106).ToString() + ";";
                    sparklineStr = sparklineStr +  charStr;                       
                }
                else
                {
                    string charStr = "&#" + (107).ToString() + ";";
                    sparklineStr = sparklineStr +  charStr;                       
                }

            }
            return sparklineStr;

/*
Public Function SparkCodeWinLoseHelper(ByVal workRange As Range, ByVal hasDraw As Long, winCounter As Integer, loseCounter As Integer, drawCounter As Integer) As String

            Dim newStrX As String
            Dim c As Variant                    
            winCounter = 0
            loseCounter = 0
            drawCounter = 0            
            
            newStrX = ""
            For Each c In workingRow.Cells
                'Fix for string in cells
                If (IsNumeric(c.value)) Then
                If (c.value > 0) Or (c.value = "+") Then
                    newStrX = newStrX + VBA.ChrW(108)
                    winCounter = winCounter + 1
              
                Else
                    'if '-' or c.value=0
                    newStrX = newStrX + VBA.ChrW(106)
                    loseCounter = loseCounter + 1
                End If
                End If
            Next c
            
            SparkCodeWinLoseHelper = newStrX

End Function
*/
        }

    }
}
