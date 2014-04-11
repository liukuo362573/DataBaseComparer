using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseComparer.Common
{
    #region ComboBox控件数据类
    /// <summary>
    /// ComboBox控件数据类
    /// </summary>
    public class ComboboxItem
    {
        public ComboboxItem() { }

        public ComboboxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public object Value { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
    #endregion

    #region 表里面的字段
    public class DBTable
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 是否可以为空
        /// </summary>
        public bool CanNull { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Comment { get; set; }

        public override string ToString()
        {
            return Field + " " + Type + "(" + Length + ")";
        }
    }
    #endregion
}
