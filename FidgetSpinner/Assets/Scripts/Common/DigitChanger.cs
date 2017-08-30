using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fidget.Common
{
    public class DigitChanger
    {
        public DigitChanger()
        {

        }
        public string UdigitToString(ulong digit)
        {
            string to_str = digit.ToString();
            if (digit >= 1000) //천 이상일때
            {
                ulong sosu_point = (digit % 1000) / 100;
                to_str = string.Format("{0:n0}", (digit / 1000));
                if (sosu_point != 0 && to_str.Length < 7)
                {
                    to_str += "." + sosu_point;
                }
                to_str += "K";
            }
            
            return to_str;
        }


        public string digitToString(int digit)
        {
            string to_str = digit.ToString();
            if (digit >= 1000)
            {
                int sosu_point = (digit % 1000) / 100;
                to_str = string.Format("{0:n0}", (digit / 1000));
                if (sosu_point != 0 && to_str.Length < 7)
                {
                    to_str += "." + sosu_point;
                }
                to_str += "K";
            }
            
            return to_str;
        }

    }
}