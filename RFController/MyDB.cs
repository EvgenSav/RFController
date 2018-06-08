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
}
