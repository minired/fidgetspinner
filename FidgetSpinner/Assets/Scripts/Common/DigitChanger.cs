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
                to_str = digit / 1000 + "";
                if (sosu_point != 0) to_str += "." + sosu_point;
                to_str += "K";
                if (digit >= 1000000) //100만 이상일 때
                {

                    sosu_point = (digit % 1000000) / 100000;
                    to_str = digit / 1000000 + "";
                    if (sosu_point != 0) to_str += "." + sosu_point;
                    to_str += "M";


                    if (digit >= 1000000000) //10억 이상일 때
                    {
                        sosu_point = (digit % 1000000000) / 100000000;
                        to_str = digit / 1000000000 + "";
                        if (sosu_point != 0) to_str += "." + sosu_point;
                        to_str += "B";
                        if (digit >= 1000000000000) //1조 이상일 때
                        {
                            sosu_point = (digit % 1000000000000) / 100000000000;
                            to_str = digit / 1000000000000 + "";
                            if (sosu_point != 0) to_str += "." + sosu_point;
                            to_str += "T";
                        }
                    }
                }
            }
            
            return to_str;
        }


        public string digitToString(int digit)
        {
            string to_str = digit.ToString();
            if (digit >= 1000)
            {
                int sosu_point = (digit % 1000) / 100;
                to_str = digit / 1000 + "";
                if (sosu_point != 0) to_str += "." + sosu_point;
                to_str += "K";
                if (digit >= 1000000)
                {

                    sosu_point = (digit % 1000000) / 100000;
                    to_str = digit / 1000000 + "";
                    if (sosu_point != 0) to_str += "." + sosu_point;
                    to_str += "M";


                    if (digit >= 1000000000)
                    {
                        sosu_point = (digit % 1000000000) / 100000000;
                        to_str = digit / 1000000000 + "";
                        if (sosu_point != 0) to_str += "." + sosu_point;
                        to_str += "B";
                    }
                }
            }
            
            return to_str;
        }

    }
}