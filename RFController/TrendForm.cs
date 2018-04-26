using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace RFController {
    public partial class GraphForm : Form {
        Action<Series> ChartUpdater;
        MTRF dev1;
        List<TempAtChannel> ChannelTempLog;
        Series s1;

        public GraphForm(MTRF dev, List<TempAtChannel> chnData) {
            InitializeComponent();

            ChannelTempLog = chnData;
            dev1 = dev;
            dev1.NewDataReceived += Dev1_NewDataReceived;
            this.FormClosing += TrendForm_FormClosing;
            s1 = chart1.Series[0];
            s1.IsVisibleInLegend = false;
            s1.ChartType = SeriesChartType.Point;
            s1.Color = Color.Green;
            s1.XAxisType = AxisType.Primary;
            s1.XValueType = ChartValueType.Time;
            s1.ToolTip="Temperature at selected channel";
            s1.IsXValueIndexed = true;
            ChartUpdater = new Action<Series>(ChartUpdate);


            s1.Points.DataBind(ChannelTempLog, "CurrentTime", "Value","");
            //for (int i = 0; i < ChannelTempLog.Count; i++) {
            //    DataPoint p1 = new DataPoint();
            //    p1.SetValueXY(ChannelTempLog[i].CurrentTime.ToShortTimeString(),
            //        ChannelTempLog[i].Value);
            //    s1.Points.Add(p1);
            //        //= ChannelTempLog[i].CurrentTime.ToShortTimeString(), ChannelTempLog[i].Value);
            //}            
        }

        private void TrendForm_FormClosing(object sender, FormClosingEventArgs e) {
            dev1.NewDataReceived -= Dev1_NewDataReceived;
        }

        private void ChartUpdate(Series s) {
            if (s.Points.Count != ChannelTempLog.Count) {
                DataPoint p1 = new DataPoint();
                p1.SetValueXY(ChannelTempLog[ChannelTempLog.Count - 1].CurrentTime,
                    ChannelTempLog[ChannelTempLog.Count - 1].Value);
                s.Points.Add(p1);
            }
            //s.Points.AddXY(ChannelTempLog[ChannelTempLog.Count-1].CurrentTime.ToShortTimeString(), 
            //    ChannelTempLog[ChannelTempLog.Count - 1].Value);
        }

        private void Dev1_NewDataReceived(object sender, EventArgs e) {
            chart1.BeginInvoke(ChartUpdater, s1);
        }

        private void chart1_GetToolTipText(object sender, ToolTipEventArgs e) {
            e.Text = String.Format("{0:#.##} {1}C", e.Y, (char)176);
        }
    }
}
