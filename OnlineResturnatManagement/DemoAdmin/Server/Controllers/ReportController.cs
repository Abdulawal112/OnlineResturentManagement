
using OnlineResturnatManagement.Server.Services.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
//using Microsoft.Reporting.NETCore;
using System.Data;
using System.Text;
//using Microsoft.Reporting.NETCore;
using ReportParameter = Microsoft.Reporting.NETCore.ReportParameter;

namespace OnlineResturnatManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmployeeService _employeeService;

        public ReportController(IWebHostEnvironment webHostEnvironment,IEmployeeService employeeService)
        {
            _webHostEnvironment = webHostEnvironment;
            _employeeService = employeeService;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        }
        [HttpGet]
        [Route("GetReport")]
        public IActionResult GetReport(int reportType)
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("Salary");
            DataRow row1;
            var employeeData = _employeeService.GetAllEmployeeAsync();
            foreach (var item in employeeData.Result)
            {
                row1 = dt.NewRow();
                row1["Name"] = item.Name;
                row1["Address"] = item.Address;
                row1["Mobile"] = item.MobileNo;
                row1["Salary"] = item.Salary;
                dt.Rows.Add(row1);
            }

            string reportName = "TestReport";
            string reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "Report1.rdlc"); //or webHostEnvironment.WebRootPath if your report is in wwwroot folder

            

            Stream reportDefinition; // your RDLC from file or resource
                                     //IEnumerable dataSource; // your datasource for the report
            using var fs = new FileStream(reportPath, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("dsEmployee", dt));
            report.SetParameters(new[] { new ReportParameter("param", "RDLC Sample Report ") });
            if (reportType == 1)
            {
                byte[] pdf = report.Render("PDF");
                fs.Dispose();

                return File(pdf, "application/pdf");
            }
            else
            {
                byte[] pdf = report.Render("Excel");
                fs.Dispose();

                return File(pdf, "application/msexcel", reportName + ".xls");
            }

        }
    }
}

