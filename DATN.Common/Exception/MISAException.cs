using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common
{
    public class MISAException: Exception
    {
        /// <summary>
        /// Tin nhắn 
        /// </summary>
        string MISAMessenger;
        IDictionary erros;
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="msg">Tin nhắn</param>
        /// <param name="listMsgs">Danh sách tin nhắn</param>
        public MISAException(string msg = null, Dictionary<string, string> listMsgs= null)
        {
            this.MISAMessenger = msg;
            erros = new Dictionary<string, Dictionary<string, string>>();
            erros.Add("validateError", listMsgs); 
        }

        public override string Message => this.MISAMessenger;

        public override IDictionary Data => this.erros; 
    }
}
