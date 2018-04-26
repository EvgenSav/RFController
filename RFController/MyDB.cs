﻿using System;
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
        public SortedDictionary<TKey, List<TValue>> Data;

        public MyDB() {
            Data = new SortedDictionary<TKey, List<TValue>>();
        }
        //public TValue Latest { get; set; }
        public void Add(TKey channel, TValue data) {
            if (Data.ContainsKey(channel)) {
                Data[channel].Add(data);
            } else {
                if (typeof(TValue) == typeof(TempAtChannel)) {
                    Data.Add(channel, new List<TValue>(44640));
                } else {
                    Data.Add(channel, new List<TValue>(64));
                }                
                Data[channel].Add(data);
            }
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
    }

    [Serializable]
    public class RfDevice {
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
    }
}
