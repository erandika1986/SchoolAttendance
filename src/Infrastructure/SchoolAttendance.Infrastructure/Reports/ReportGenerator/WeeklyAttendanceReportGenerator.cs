using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Infrastructure.Reports
{
	public class WeeklyAttendanceReportGenerator : BaseReportGenerator
  {
    int fromYear;
    int fromMonth;
    int fromDay;
    int toYear;
    int toMonth;
    int toDay;
    int selectedClassId;
    string clasName = string.Empty;
    
    public WeeklyAttendanceReportGenerator(Dictionary<string, string> reportParams, ISchoolAttendanceContext db, IConfiguration config)
      : base(reportParams, db, config)
    {
       fromYear = int.Parse(reportParams.FirstOrDefault(x => x.Key == "fromYear").Value);
       fromMonth = int.Parse(reportParams.FirstOrDefault(x => x.Key == "fromMonth").Value);
       fromDay = int.Parse(reportParams.FirstOrDefault(x => x.Key == "fromDay").Value);
       toYear = int.Parse(reportParams.FirstOrDefault(x => x.Key == "toYear").Value);
       toMonth = int.Parse(reportParams.FirstOrDefault(x => x.Key == "toMonth").Value);
       toDay = int.Parse(reportParams.FirstOrDefault(x => x.Key == "toDay").Value);
       selectedClassId = int.Parse(reportParams.FirstOrDefault(x => x.Key == "selectedClassId").Value);
       clasName = reportParams.FirstOrDefault(x => x.Key == "clasName").Value;
    }

    public override DownloadFileModel GenerateExcelReport()
    {
      var file = new DownloadFileModel();

      var reportData = GenerateWeeklyAttendanceReportData();
      var fileIdentifier = DateTime.Now.ToString("yyyyMMddHHmmssfff");
      var fromDate = new DateTime(fromYear, fromMonth, fromDay, 0, 0, 0);
      var toDate = new DateTime(toYear, toMonth, toDay, 0, 0, 0);

      var dateRange = string.Format("{0} to {1}", fromDate.ToString("dd-MM-yyyy"), toDate.ToString("dd-MM-yyyy"));
      var excelName = string.Format("Grade {0} {1}-{2}.xlsx", clasName, dateRange, fileIdentifier);
      //var pdfName = string.Format("{0}{1}.pdf", reportData.PONumber, fileIdentifier);

      var excelSection = config.GetSection("Report");

      var excelSavingPath = excelSection.GetSection("ExcelReportSavingPath").Value;

      var excelReportPath = string.Format("{0}{1}", excelSavingPath, excelName);
      //var pdfReportPath = string.Format("{0}{1}", excelSavingPath, pdfName);
      var templatePath = GenerateWeeklyAttendanceReportTemplatePath();

      File.Copy(templatePath, excelReportPath);

      FileInfo newFile = new FileInfo(excelReportPath);



      using (var package = new ExcelPackage(newFile))
      {
        var workSheet = package.Workbook.Worksheets["Attendance"];

        //For Company Details
        //workSheet.Cells[1, 3].Value = string.Format("Attendance Sheet ({0}) {1}", clasName, dateRange);
        var cell = workSheet.Cells[1, 3];

        var topic = string.Format("Attendance Sheet({0}) ", clasName);
        var r1 = cell.RichText.Add(topic + Environment.NewLine);
        r1.Bold = true;
        r1.Size = 12;

        var r2 = cell.RichText.Add(dateRange);
        r2.Bold = false;
        r2.Size = 11;



        workSheet.Cells[1, 3].Style.WrapText = true;
        workSheet.Cells[1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        workSheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        int startIndex = 6;
        var studentNo = 0;
        foreach (var student in reportData.Students)
        {
          studentNo++;
          workSheet.Cells[startIndex, 1].Value = studentNo;
          workSheet.Cells[startIndex, 2].Value = student.IndexNo;
          workSheet.Cells[startIndex, 3].Value = student.StudentName;

          var dayStartIndex = 3;
          var totalDeleteColumns = 0;

          foreach (var day in student.Days)
          {

            var totalNoOfSubject = 0;

            foreach (var subject in day.Subjects)
            {
              dayStartIndex++;

              if (startIndex == 6)
              {
                workSheet.Cells[3, dayStartIndex,5,dayStartIndex].Value = string.Format("{0} {1}", subject.Name, day.Date);

                workSheet.Column(dayStartIndex).Width = 5;

                if(subject.NotConducted)
                {
                  using (ExcelRange Rng = workSheet.Cells[startIndex, dayStartIndex, reportData.Students.Count+5, dayStartIndex])
                  {
                    Rng.Value = "Not Done";
                    Rng.Merge = true;
                    Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Top.Color.SetColor(Color.Black);
                    Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Left.Color.SetColor(Color.Black);
                    Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Right.Color.SetColor(Color.Black);
                    Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Bottom.Color.SetColor(Color.Black);
                  }
                  workSheet.Cells[startIndex, dayStartIndex, reportData.Students.Count + 5, dayStartIndex].Style.TextRotation = 180;
                  workSheet.Cells[startIndex, dayStartIndex, reportData.Students.Count + 5, dayStartIndex].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                  workSheet.Cells[reportData.Students.Count + 6, dayStartIndex].Value=0;
                }
                else
                {
                  var sumCell = workSheet.Cells[reportData.Students.Count + 6, dayStartIndex];
                  sumCell.Formula = "=SUM(" + workSheet.Cells[startIndex, dayStartIndex].Address + ":" + workSheet.Cells[reportData.Students.Count + 5, dayStartIndex].Address + ")";
                }


              }

              if(!subject.NotConducted)
              {
                workSheet.Cells[startIndex, dayStartIndex].Value = subject.IsPresent ? 1 : 0;
              }



              totalNoOfSubject++;


            }

            var totalSubject = day.Subjects.Count();
            var leftColumns = 10 - totalSubject;

            if (startIndex == 6)
            {
              var noOfDeletions = 1;
              while (leftColumns > noOfDeletions)
              {
                workSheet.DeleteColumn(dayStartIndex + noOfDeletions);
                noOfDeletions++;
                totalDeleteColumns++;
              }

              var fromRow1 = 1;
              var fromColumn1 = dayStartIndex -totalNoOfSubject+1;

              var toRow = 2;
              int ToCol1 = dayStartIndex;



              using (ExcelRange Rng = workSheet.Cells[fromRow1, fromColumn1, toRow, ToCol1])
              {
                Rng.Value = day.DayName;
                Rng.Merge = true;
                Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Top.Color.SetColor(Color.Black);
                Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Left.Color.SetColor(Color.Black);
                Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Right.Color.SetColor(Color.Black);
                Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Bottom.Color.SetColor(Color.Black);
              }

              workSheet.Cells[fromRow1, fromColumn1, toRow, ToCol1].Style.WrapText = true;
              workSheet.Cells[fromRow1, fromColumn1, toRow, ToCol1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
              workSheet.Cells[fromRow1, fromColumn1, toRow, ToCol1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

              //dayStartIndex = dayStartIndex + leftColumns;

          }

          if(startIndex==6)
          {
            if(dayStartIndex+totalDeleteColumns!=53)
            {
              int columnLeftToDelete = 53 - dayStartIndex - totalDeleteColumns;
              var deleteLimit = dayStartIndex + columnLeftToDelete + 2;
              for (int i = dayStartIndex+1; i < deleteLimit; i++)
              {
                workSheet.DeleteColumn(i);
              }

            }

            var totalRowCount = reportData.Students.Count + 6;
            var emptyRowCount = 57 - totalRowCount;

            workSheet.DeleteRow(reportData.Students.Count + 7, emptyRowCount);
          }

          startIndex++;
        }


        package.Save();

      }



      byte[] fileContents = null;
      MemoryStream ms = new MemoryStream();

      using (FileStream fs = File.OpenRead(excelReportPath))
      {
        fs.CopyTo(ms);
        fileContents = ms.ToArray();
        ms.Dispose();
        file.FileData = fileContents;
      }

      file.FileName = excelName;

      return file;
    }

    public override DownloadFileModel GeneratePDFReport()
    {
      return base.GeneratePDFReport();
    }


    private ClassAllSubjectAttendanceReportContainer GenerateWeeklyAttendanceReportData()
    {
      var reportData = new ClassAllSubjectAttendanceReportContainer();


      var reportContainer = new ClassAllSubjectAttendanceReportContainer();
      reportContainer.FromDate = new DateTime(fromYear, fromMonth, fromDay, 0, 0, 0);
      reportContainer.ToDate = new DateTime(toYear, toMonth, toDay, 0, 0, 0);
      reportContainer.TotalStudent = db.StudentClasses
        .Where(x => x.IsActive == true && x.ClassId == selectedClassId).Count();

      var fromDate = new DateTime(fromYear, fromMonth, fromDay, 0, 0, 0);
      var toDate = new DateTime(toYear, toMonth, toDay, 0, 0, 0);

      var students = db.Classes.FirstOrDefault(cl => cl.Id == selectedClassId)
        .StudentClasses
        .Select(x => new ClassAllSubjectAttendanceReport()
        {
          IndexNo = x.Student.Username,
          StudentName = x.Student.FullName,
          StudentId = x.StudentId
        })
        .OrderBy(s => s.StudentName
        ).ToList();

      while (fromDate <= toDate)
      {
        var dayAttendance = db.SubjectAttendances
          .Where(x => x.Date == fromDate)
          .OrderBy(d => d.StartTime).ToList();

        var classDayTimeTableIds = db.ClassSubjectTimeTables
          .Where(x => x.DayId == (int)fromDate.DayOfWeek)
          .Select(d => d.Id).ToList();

        var classTimeSlotIds = dayAttendance.Where(x => x.TimeSlotId.HasValue).Select(d => d.TimeSlotId.Value).ToList();

        var notAddedTimeSlots = db.ClassSubjectTimeTables
          .Where(x => x.DayId == (int)fromDate.DayOfWeek && !classTimeSlotIds.Any(y => y == x.Id)).ToList();

        foreach (var student in students)
        {
          var day = new TimeTableDay()
          {
            DayId = (int)fromDate.DayOfWeek,
            DayName = fromDate.DayOfWeek.ToString(),
            Date = fromDate.ToString("dd/MM/yyyy")
          };

          var currentDate = DateTime.Now;

          foreach (var item in dayAttendance)
          {
            //var matchingTimeTableRecord = classDayTimeTable.FirstOrDefault(x=>x.Id== item.TimeSlot)
            var daySubject = new DaySubject()
            {
              StartTime = new DateTime(currentDate.Year,currentDate.Month,currentDate.Day,item.StartTime.Hour,item.StartTime.Minute,0),
              NotConducted = false,
              Name = item.Subject.Name,
              SubjectId = item.SubjectId,
              IsPresent = item.StudentSubjectAttendances.FirstOrDefault(x => x.StudentId == student.StudentId).IsAttended
            };

            day.Subjects.Add(daySubject);
          }

          foreach (var item in notAddedTimeSlots)
          {
            var daySubject = new DaySubject()
            {
              StartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, item.StartTime.Hour, item.StartTime.Minute, 0),
              NotConducted = true,
              Name = item.Subject.Name,
              SubjectId = item.SubjectId,
            };

            day.Subjects.Add(daySubject);
          }

          day.Subjects = day.Subjects.OrderBy(x => x.StartTime).ToList();
          student.Days.Add(day);
        }

        fromDate = fromDate.AddDays(1);
      }

      reportData.Students.AddRange(students);

      return reportData;
    }

    private string GenerateWeeklyAttendanceReportTemplatePath()
    {
      var outPutDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
      var templatePath = Path.Combine(outPutDirectory, "ExcelTemplates\\WeeklyRegisterTemplate.xlsx");

      return templatePath;
    }
  }
}
