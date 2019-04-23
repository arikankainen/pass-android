using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace Pass
{
    public static class ProcessFile
    {
        private static string firstline = "73hfa8ka-4jw3-5jsc-jldf-lfgjsadgmdlk3hlgfjsf-3562-sdg5-423d-3468jkjsäsköldsakölf";
        private static string separator = "c3b19327-3a2e-4bac-b8a9-2b20b167c1e94083d8ce-4ebf-4112-b33d-eca6ac3f79b7ca238cd2";
        private static string newline = "9ed3a5d1-901b-49fa-b9c6-700bbd534fa798bb6729-b934-47fa-af34-3e8038c01c6571544560";

        public static List<Dictionary<string, string>> GetProcessed(string str)
        {
            try
            {
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                string[] lines = str.Split(new[] { newline }, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    if (line.Length > 10 && line != firstline)
                    {
                        string[] columns = line.Split(new[] { separator }, StringSplitOptions.None);

                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        dict.Add("Name", columns[0]);
                        dict.Add("Address", columns[1]);
                        dict.Add("User", columns[2]);
                        dict.Add("Pass", columns[3]);
                        dict.Add("Comments", columns[4]);

                        list.Add(dict);
                    }
                }

                return list;
            }
            catch
            {
                return new List<Dictionary<string, string>>();
            }
        }
    }
}