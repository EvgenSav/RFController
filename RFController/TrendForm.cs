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
        List<ILogItem> ChannelTempLog;
        Series s1;

        public GraphForm(MTRF dev, List<ILogItem> chnData) {
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

            if(ChannelTempLog.Count > 0) {
                if ((ChannelTempLog[0] as LogItem) != null) {
                    s1.Points.DataBind(ChannelTempLog, "CurrentTime", "Cmd", "");
                }
                if ((ChannelTempLog[0] as SensLogItem) != null) {
                    s1.Points.DataBind(ChannelTempLog, "CurrentTime", "SensVal", "");
                }
                if ((ChannelTempLog[0] as PuLogItem) != null) {
                    s1.Points.DataBind(ChannelTempLog, "CurrentTime", "Bright", "");
                }
            }           
        }

        private void TrendForm_FormClosing(object sender, FormClosingEventArgs e) {
            dev1.NewDataReceived -= Dev1_NewDataReceived;
        }

        private void ChartUpdate(Series s) {
            if (s.Points.Count != ChannelTempLog.Count) {
                DataPoint p1 = new DataPoint();
                if ((ChannelTempLog[0] as LogItem) != null) {
                    p1.SetValueXY(ChannelTempLog[ChannelTempLog.Count - 1].CurrentTime,
                    ChannelTempLog[ChannelTempLog.Count - 1].Cmd);
                }
                if ((ChannelTempLog[0] as SensLogItem) != null) {
                    SensLogItem logItem = ChannelTempLog[ChannelTempLog.Count - 1] as SensLogItem;
                    p1.SetValueXY(ChannelTempLog[ChannelTempLog.Count - 1].CurrentTime,
                    logItem.SensVal);
                }
                if ((ChannelTempLog[0] as PuLogItem) != null) {
                    PuLogItem logItem = ChannelTempLog[ChannelTempLog.Count - 1] as PuLogItem;
                    p1.SetValueXY(ChannelTempLog[ChannelTempLog.Count - 1].CurrentTime,
                    logItem.Bright);
                }      
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
