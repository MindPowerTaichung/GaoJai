using MPERP2015.Models;
using MPERP2015.Reports;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var reportData =generateReportData();
            exportReport.DataSource = reportData;

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
                            string.Format("attachment;FileName=\"標籤{0:yyyyMMddHHmmss}_{1}.pdf\"",
                                            DateTime.Now,
                                            reportData[0].Name
                                            ));


            HttpContext.Current.Response.BinaryWrite(result.DocumentBytes);
            HttpContext.Current.Response.End();
        }

        Report getReport()
        {
            return new CustomerLabelReport();
        }


        List<CustomerViewModel> generateReportData()
        {
            string jsonString = HttpContext.Current.Request.Form["record"];
            JObject jsonObj = JsonConvert.DeserializeObject<JObject>(jsonString);
            int num = Convert.ToInt32(jsonObj.Property("num").Value);
            string name = jsonObj.Property("name").Value.ToString();
            string addr = jsonObj.Property("address").Value.ToString();
            string tel = jsonObj.Property("telephone").Value.ToString();

            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            for (int i = 0; i < num; i++)
            {
                customers.Add(new CustomerViewModel { Name = name, Shipaddr = addr, Telephone1 = tel });
            }
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