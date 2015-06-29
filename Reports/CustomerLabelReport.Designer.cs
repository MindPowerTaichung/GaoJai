namespace MPERP2015.Reports
{
    partial class CustomerLabelReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.objectDataSource1 = new Telerik.Reporting.ObjectDataSource();
            this.detail = new Telerik.Reporting.DetailSection();
            this.addressDataTextBox = new Telerik.Reporting.TextBox();
            this.customerNameDataTextBox = new Telerik.Reporting.TextBox();
            this.telDataTextBox = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataMember = "GetSampleData";
            this.objectDataSource1.DataSource = typeof(MPERP2015.Reports.Models.CustomerLabel);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(1.4600000381469727D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.addressDataTextBox,
            this.customerNameDataTextBox,
            this.telDataTextBox});
            this.detail.Name = "detail";
            // 
            // addressDataTextBox
            // 
            this.addressDataTextBox.CanGrow = false;
            this.addressDataTextBox.CanShrink = false;
            this.addressDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388D), Telerik.Reporting.Drawing.Unit.Cm(0.30000004172325134D));
            this.addressDataTextBox.Name = "addressDataTextBox";
            this.addressDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.500106811523438D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.addressDataTextBox.Value = "= Fields.Address";
            // 
            // customerNameDataTextBox
            // 
            this.customerNameDataTextBox.CanGrow = false;
            this.customerNameDataTextBox.CanShrink = false;
            this.customerNameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388D), Telerik.Reporting.Drawing.Unit.Cm(1.2999998331069946D));
            this.customerNameDataTextBox.Name = "customerNameDataTextBox";
            this.customerNameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.500106811523438D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.customerNameDataTextBox.Value = "= Fields.CustomerName";
            // 
            // telDataTextBox
            // 
            this.telDataTextBox.CanGrow = false;
            this.telDataTextBox.CanShrink = false;
            this.telDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.19999989867210388D), Telerik.Reporting.Drawing.Unit.Cm(2.3000001907348633D));
            this.telDataTextBox.Name = "telDataTextBox";
            this.telDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.500106811523438D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.telDataTextBox.Value = "= Fields.Tel";
            // 
            // CustomerLabelReport
            // 
            this.DataSource = this.objectDataSource1;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "CustomerLabelReport";
            this.PageSettings.ColumnCount = 1;
            this.PageSettings.ColumnSpacing = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Mm(104.98894500732422D), Telerik.Reporting.Drawing.Unit.Inch(0.1574999988079071D), Telerik.Reporting.Drawing.Unit.Mm(3.9933760166168213D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(4.3299999237060547D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.ObjectDataSource objectDataSource1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox addressDataTextBox;
        private Telerik.Reporting.TextBox customerNameDataTextBox;
        private Telerik.Reporting.TextBox telDataTextBox;

    }
}