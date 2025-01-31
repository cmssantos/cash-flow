
using CashFlow.Application.Services;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCase(ILocalizer localizer) : IGenerateExpensesReportExcelUseCase
{
    private readonly ILocalizer _localizer = localizer;

    public async Task<byte[]> ExecuteAsync(DateOnly month)
    {
        var workbook = new XLWorkbook
        {
            Author = "CashFlow"
        };

        workbook.Style.Font.FontName = "Times New Roman";
        workbook.Style.Font.FontSize = 12;

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = _localizer.GetString("Report.Title");
        worksheet.Cell("B1").Value = _localizer.GetString("Report.Date");
        worksheet.Cell("C1").Value = _localizer.GetString("Report.PaymentType");
        worksheet.Cell("D1").Value = _localizer.GetString("Report.Amount");
        worksheet.Cell("E1").Value = _localizer.GetString("Report.Description");

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
