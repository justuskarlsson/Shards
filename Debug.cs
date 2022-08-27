using HarmonyLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shards
{
    internal static class Debug
    {
        public static void Log(params object[] list) {
            Harmony.DEBUG = true;
            List<string> buffer = FileLog.GetBuffer(true);


            Harmony.DEBUG = false;            
            for (int i = 0; i < list.Length; i++) {
                buffer.Add(list[i] + "");
            }
            buffer.Add("");
            buffer.Add("");
            FileLog.LogBuffered(buffer);
            FileLog.FlushBuffer();
            Harmony.DEBUG = false;
        }

        public static string DescribeObject(object obj) {
            string res = TypeDescriptor.GetClassName(obj) + ":\n";
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj)) {
                string name = descriptor.Name;
                object value = descriptor.GetValue(obj);
                res += "  " + name + ":" + value + "\n";
            }
            return res;
        }

    }
}
