using System;
using DevExpress.XtraReports.UI;
using AmloNewbis.DataContract;
namespace AmloNewbisAPI.Report
{
    public partial class EDDReport
    {
        public EDDReport(EddReport[] data)
        {
            InitializeComponent();
            this.DataSource = data;
        }
    }
}
