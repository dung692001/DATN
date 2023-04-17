using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Common
{
    /// <summary>
    /// Thông tin bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MISARequired : Attribute
    {
        
    }

    /// <summary>
    /// Thông tin tên của thuộc tính 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PropNameDisplay : Attribute
    {
        public string PropName { get; set; }
        public PropNameDisplay(string name)
        {
            this.PropName = name;
        }
    }

    /// <summary>
    /// Kiểm tra độ dài
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MISALength : Attribute
    {
        public int LengthInput { get; set; }
        public MISALength(int length)
        {
            this.LengthInput = length;
        }
    }

    /// <summary>
    /// Nếu có attr này thì là khóa chính của 1 bảng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PrimaryKey : Attribute
    {

    }
}
