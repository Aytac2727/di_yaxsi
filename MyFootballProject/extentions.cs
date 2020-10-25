using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballProject
{
    class extentions
    {


        public static bool IsEmpty(string[] arr, string text)
        {
            foreach (var array in arr)
            {
                if (array == text)
                {
                    return false;
                }
            }
            return true;
        }

        public static string HashMe(string pas)
        {
            byte[] myByte = new ASCIIEncoding().GetBytes(pas);
            byte[] hashByte = new SHA256Managed().ComputeHash(myByte);
            string hashString = new ASCIIEncoding().GetString(hashByte);
            return hashString;
        }
    }
}
