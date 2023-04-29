using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DATN.Common.HandleMethod
{
    public static class HandleFormat
    {
        /// <summary>
        /// Phương thức tách ra dãy các số từ dãy string
        /// </summary>
        /// <param name="listText">Dãy cần tách</param>
        /// <returns>
        ///     Dãy sau khi đã tách hết kí tự không phải là số
        /// </returns>
        public static List<string> SplitNumberFromString(List<string> listText)
        {
            List<string> newList = new List<string>();
            foreach(string str in listText)
            {
                var tempText = Regex.Replace(str, "[^0-9]", "");
                if(string.IsNullOrEmpty(tempText) == false)
                {
                    newList.Add(tempText);
                }
            }
            return newList;
        }

        /// <summary>
        ///     Phương thức tìm số lớn nhất từ 1 dãy số
        /// </summary>
        /// <param name="listText">Dãy cần tìm</param>
        /// <returns>
        ///     Kết quả sau khi tìm số lớn nhất cộng 1
        /// </returns>
        public static string FindMaxNumber(List<string> listText)
        {
            int maxNumber = 0;
            List<int>listTemp = listText.Select(int.Parse).ToList();
            foreach(int tmp in listTemp)
            {
                if(maxNumber < tmp)
                {
                    maxNumber = tmp;
                }
            }
            maxNumber = maxNumber + 1;
            return maxNumber.ToString();
        }

    }
}
