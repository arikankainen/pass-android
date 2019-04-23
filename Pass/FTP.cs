using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Pass
{
    public static class FTP
    {
        public static string GetFileContents(string address, string username, string password)
        {
            try
            {
                FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(address);
                ftp.Method = WebRequestMethods.Ftp.DownloadFile;
                ftp.Credentials = new NetworkCredential(username, password);
                ftp.UseBinary = true;

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string str = reader.ReadToEnd();

                return str;
            }
            catch
            {
                return null;
            }

        }
    }
}