using System;
using AmloNewbis.DataContract;
using DevExpress.XtraReports.UI;

namespace AmloNewbisAPI.Report
{
    public partial class AmloFormReport
    {
        public AmloFormReport(AmloReport[] data)
        {
            InitializeComponent();
            this.DataSource = data;
        }
    }
}
