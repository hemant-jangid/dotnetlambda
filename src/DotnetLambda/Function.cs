using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using OfficeOpenXml;
using OfficeOpenXml.Style;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DotnetLambda
{
  public class Function
  {

    /// <summary>
    /// A simple function that takes a string and returns both the upper and lower case version of the string.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public string FunctionHandler(Input input, ILambdaContext context)
    {
      var startTime = DateTime.Now;
      GenerateExcel(input.Count);
      var endTime = DateTime.Now;

      return $"Done - Took {((endTime - startTime).TotalMilliseconds)}ms";
    }

    void GenerateExcel(int count)
    {
      var Articles = new[]
      {
                new {
                    Id = "101", Name = "C++"
                },
                new {
                    Id = "102", Name = "Python"
                },
                new {
                    Id = "103", Name = "Java Script"
                },
                new {
                    Id = "104", Name = "GO"
                },
                new {
                    Id = "105", Name = "Java"
                },
                new {
                    Id = "106", Name = "C#"
                }
            };


      // file name with .xlsx extension 

      var tempPath = Path.GetTempPath();


      string p_strPath = Path.Combine(tempPath, "geeksforgeeks.xlsx");

      if (File.Exists(p_strPath))
        File.Delete(p_strPath);

      // Create excel file on physical disk 
      FileStream objFileStrm = File.Create(p_strPath);


      // Creating an instance
      // of ExcelPackage
      ExcelPackage excel = new ExcelPackage(objFileStrm);

      // name of the sheet
      var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

      // setting the properties
      // of the work sheet 
      workSheet.TabColor = System.Drawing.Color.Black;
      workSheet.DefaultRowHeight = 12;

      // Setting the properties
      // of the first row
      workSheet.Row(1).Height = 20;
      workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
      workSheet.Row(1).Style.Font.Bold = true;

      // Header of the Excel sheet
      workSheet.Cells[1, 1].Value = "S.No";
      workSheet.Cells[1, 2].Value = "Id";
      workSheet.Cells[1, 3].Value = "Name";

      // Inserting the article data into excel
      // sheet by using the for each loop
      // As we have values to the first row 
      // we will start with second row
      int recordIndex = 2;

      for (var i = 0; i < count; i++)
      {
        foreach (var article in Articles)
        {
          workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
          workSheet.Cells[recordIndex, 2].Value = article.Id;
          workSheet.Cells[recordIndex, 3].Value = article.Name;
          recordIndex++;
        }
      }

      // By default, the column width is not 
      // set to auto fit for the content
      // of the range, so we are using
      // AutoFit() method here. 
      workSheet.Column(1).AutoFit();
      workSheet.Column(2).AutoFit();
      workSheet.Column(3).AutoFit();
      Console.WriteLine($"Excel Size {objFileStrm.Length}");

      excel.Save();
      //Close Excel package
      excel.Dispose();

      //   File.Delete(p_strPath);




    }
  }

  public record Input(int Count);
  public record Casing(string Lower, string Upper);
}
