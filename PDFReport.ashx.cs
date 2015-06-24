using MPERP2015.Models;
using MPERP2015.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Reporting;

namespace MPERP2015
{
    /// <summary>
    /// Summary description for PDFReport
    /// </summary>
    public class PDFReport : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var exportReport = getReport();
            exportReport.DataSource = getJsonData();

            Telerik.Reporting.InstanceReportSource exportReportSource = new Telerik.Reporting.InstanceReportSource();
            // Assigning the Report object to the InstanceReportSource
            exportReportSource.ReportDocument = exportReport;

            // set any deviceInfo settings if necessary
            //System.Collections.Hashtable deviceInfo = new System.Collections.Hashtable();
            var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
            var result = reportProcessor.RenderReport("PDF", exportReportSource, null);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = result.MimeType;
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Private);
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.Buffer = true;

            //Uncomment to handle the file as attachment
            //HttpContext.Current.Response.AddHeader("Content-Disposition",
            //                string.Format("{0};FileName=\"{1:yyyyMMddHHmmss}.pdf\"",
            //                                "attachment",
            //                                DateTime.Now));


            HttpContext.Current.Response.BinaryWrite(result.DocumentBytes);
            HttpContext.Current.Response.End();
        }

        Report getReport()
        {
            return new CustomerLabelReport();
        }

        List<CustomerViewModel> getJsonData()
        {
            List<CustomerViewModel> items = new List<CustomerViewModel>();

            items.Add(new CustomerViewModel { Name = "森美交通器材行", Telephone1 = "0223937609", Shipaddr = "100 台北市中正區林森南路45號" });
            items.Add(new CustomerViewModel { Name = "興泰貿易有限公司", Telephone1 = "0225858068", Shipaddr = "248 新北市五股區新五路二段119-9號(工廠)" });
            items.Add(new CustomerViewModel { Name = "欣禾有限公司", Telephone1 = "0225627422", Shipaddr = "104 台北市中山區中山北路二段36巷35-1號" });

            return items;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}