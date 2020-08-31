using System;
using System.Collections.Generic;
using System.IO;
using CombinationsGenerator.Models;
using OfficeOpenXml;

namespace CombinationsGenerator.IO
{
    public class ExcelReader
    {
        public static (List<Variable>, I28Variable) GetVariableConditions(string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(fileName));
            var variables = new List<Variable>();

            var firstSheet = package.Workbook.Worksheets[0];
            var start = firstSheet.Dimension.Start;
            var end = firstSheet.Dimension.End;

            for (var column = start.Column; column <= end.Column; column++)
            {
                var cellContent = firstSheet.Cells[start.Row, column].Text;
                if (string.IsNullOrEmpty(cellContent))
                    break;

                variables.Add(new Variable(cellContent));
            }

            foreach (var variable in variables)
            {
                var indexInExcel = variables.FindIndex(v => v.Equals(variable)) + 1;
                for (var row = start.Row + 1; row <= end.Row; row++)
                {
                    var cellContent = firstSheet.Cells[row, indexInExcel].Text;
                    if (string.IsNullOrEmpty(cellContent))
                        break;

                    variable.Conditions.Add(new Condition() { Name = cellContent });
                }
            }

            const int firstIndexInExcel = 1;
            var secondSheet = package.Workbook.Worksheets[1];
            if (!secondSheet.Cells[firstIndexInExcel, firstIndexInExcel].Text.Contains("I28"))
                Console.WriteLine("There is a problem in the Variables.xlsx file structure. The seconds sheet should hold I28 conditions in the right format.");

            start = secondSheet.Dimension.Start;
            end = secondSheet.Dimension.End;

            var i28Variable = new I28Variable();

            for (var row = start.Row + 1; row <= end.Row; row++)
            {
                var cellContent = secondSheet.Cells[row, start.Column].Text;
                if (string.IsNullOrEmpty(cellContent))
                    break;

                i28Variable.Conditions.Add(new Condition() { Name = cellContent });
            }

            return (variables, i28Variable);
        }
    }
}
