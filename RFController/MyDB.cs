using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace RFController {
    [Serializable]
    public class MyDB<TKey, TValue> where TKey : struct /*where TValue : class*/ {
        //public SortedDictionary<TKey, List<TValue>> Data;
        public SortedDictionary<TKey, TValue> Data;

        public MyDB() {
            //Data = new SortedDictionary<TKey, List<TValue>>();
            Data = new SortedDictionary<TKey, TValue>();
        }

        public int SaveToFile(string path) {
            StreamWriter s1 = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.ReadWrite));
            string serData = JsonConvert.SerializeObject(this,Formatting.Indented);
            s1.Write(serData);
            s1.Close();
            return 1;
        }
    }

    [Serializable]
    public class TempAtChannel {
        public DateTime CurrentTime { get;  set; }
        public float Value { get;  set; }
        public TempAtChannel(DateTime dt, float val) {
            CurrentTime = dt;
            Value = val;
        }
        public override string ToString() {
            return String.Format("{0:#.##} {1}C", Value, (char)176);
        }
    }

    [Serializable]
    public class RfDevice {
        List<int> redirect = new List<int>(16);
        public int Channel { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public int Addr { get; set; }
        public int Bright { get; set; }
        public bool IsDimmable { get; set; }
        public int FirmwareVer { get; set; }
        public int DevType { get; set; }
        public int Settings { get; set; }
        public int DimCorrLvlHi { get; set; }
        public int DimCorrLvlLow { get; set; }
        public int OnLvl { get; set; }
        public string Room { get; set; }
        public List<int> Redirect { get {
                return redirect;
            }
        }
        public int AddRedirect(int devid) {
            redirect.Add(devid);
            return 0;
        }
        public string GetDeviceType() {
            string res = "";
            switch (Type) {
                case NooDevType.RemController:
                    res = "Пульт";
                    break;
                case NooDevType.Sensor:
                    switch (DevType) {
                        case 1:
                            res = "PT112";
                            break;
                        case 2:
                            res = "PT111";
                            break;
                        case 3:
                            res = "PM111";
                            break;
                        case 5:
                            res = "PM112";
                            break;
                    }
                    break;
                case NooDevType.PowerUnit:
                    res = "Сил. блок";
                    break;
                case NooDevType.PowerUnitF:
                    switch (DevType) {
                        case 0:
                            res = "MTRF-64";
                            break;
                        case 1:
                            res = "SLF-1-300";
                            break;
                        case 2:
                            res = "SRF-10-1000";
                            break;
                        case 3:
                            res = "SRF-1-3000";
                            break;
                        case 4:
                            res = "SRF-1-3000M";
                            break;
                        case 5:
                            res = "SUF-1-300";
                            break;
                        case 6:
                            res = "SRF-1-3000T";
                            break;
                    }
                    break;
            }
            return res;
        }
    }
}
