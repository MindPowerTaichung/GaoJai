using MPERP2015.Models;
using MPERP2015.Reports;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Reporting;

namespace MPERP2015.Reports
{
    /// <summary>
    /// Summary description for PDFReport
    /// </summary>
    public class CustomerLabelReportHandler : IHttpHandler
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
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                            string.Format("{0};FileName=\"標籤{1:yyyyMMddHHmmss}.pdf\"",
                                            "attachment",
                                            DateTime.Now));


            HttpContext.Current.Response.BinaryWrite(result.DocumentBytes);
            HttpContext.Current.Response.End();
        }

        Report getReport()
        {
            return new CustomerLabelReport();
        }

        List<CustomerViewModel> getJsonData()
        {
            string jsonString = HttpContext.Current.Request.Form["records"];
            List<CustomerViewModel> customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(jsonString);
            return customers;
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