using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{ 
    public static partial class Utils : System.Object
    {
        public const string DateTimeParseFormat = "yyyy-MM-dd HH:mm:ss.f";
        public const string DateTimePrintFormat = "yyyy-MM-dd";



        public static string GetFileVolumeText(long fileSize)
        {
            string res;

            double fileSizeDivided = 0.0f;
            if (fileSize >= 1024 * 1024 * 1024)
            {
                fileSizeDivided = (double)fileSize / (double)(1024 * 1024 * 1024);
                res = " GB";
            }
            else if (fileSize >= 1024 * 1024)
            {
                fileSizeDivided = (double)fileSize / (double)(1024 * 1024);
                res = " MB";
            }
            else if (fileSize >= 1024)
            {
                fileSizeDivided = (double)fileSize / (double)(1024);
                res = " KB";
            }
            else
                res = " B";

            if (fileSize > 1024)
            {
                if (fileSizeDivided < 10.0d)
                    res = fileSizeDivided.ToString("n2") + res;
                else if (fileSizeDivided < 100.0d)
                    res = fileSizeDivided.ToString("n1") + res;
                else
                    res = fileSizeDivided.ToString("n0") + res;
            }
            else
                res = fileSize.ToString("n0") + res;
            return res;
        }
    }
}