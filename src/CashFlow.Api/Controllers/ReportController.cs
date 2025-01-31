using System.Net.Mime;
using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExcel(
            [FromServices] IGenerateExpensesReportExcelUseCase useCase,
            [FromHeader] DateOnly month)
        {
            var contentFile = await useCase.ExecuteAsync(month);

            return contentFile.Length == 0
                ? NoContent()
                : File(
                fileContents: contentFile,
                contentType: MediaTypeNames.Application.Octet,
                fileDownloadName: "report.xlsx");
        }
    }
}
