using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace DatabaseComparer.Common
{
    public class XmlHelper
    {
        public static string SettingXml { get; set; }

        #region SaveSetting
        public void SaveSetting(GroupBox gBox, Panel panel, ComboBox comBox, int type)
        {
            XDocument xDoc = XDocument.Load(SettingXml);

            #region 设置选中状态
            var ele = xDoc.Descendants().Elements().Where(p => p.Name == Data.XmlRoot && p.Attribute(Data.Type).Value == type.ToString());
            foreach (var e in ele)
            {
                if (e.Attribute(Data.Selected) != null)
                {
                    e.Attribute(Data.Selected).Value = "0";
                }
            }
            #endregion

            var check = xDoc.Descendants().Elements().Where(p => p.Name == comBox.Name && p.Value == comBox.Text).
                Select(p => p.Parent).Select(p => p.Attribute(Data.Type)).
                Where(p => int.Parse(p.Value) == type);

            if (check.Count() > 0)
            {
                var xe = (from x in xDoc.Descendants().Elements().Where(p => p.Name == comBox.Name && p.Value == comBox.Text &&
                    p.Parent.Attribute(Data.Type).Value == type.ToString()).Select(p => p.Parent)
                          select x);

                foreach (Control c in gBox.Controls)
                {
                    if (c is TextBox || c is ComboBox)
                    {
                        xe.FirstOrDefault().SetElementValue(c.Name, c.Text);
                    }
                }
                var radio = panel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                xe.FirstOrDefault().SetElementValue("DBType", radio.Text);
                xe.FirstOrDefault().SetAttributeValue(Data.Selected, 1);
                xDoc.Save(SettingXml);
            }
            else
            {
                XElement xe = xDoc.Root;
                List<XElement> list = new List<XElement>();
                foreach (Control c in gBox.Controls)
                {
                    if (c is TextBox || c is ComboBox)
                    {
                        list.Add(new XElement(c.Name, c.Text));
                    }
                }
                var radio = panel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                list.Add(new XElement("DBType", radio.Text));

                var xelement = new XElement(Data.XmlRoot, new XAttribute(Data.Type, type.ToString()), new XAttribute(Data.Selected, "1"), list);
                xelement.Attribute(Data.Type).Value = type.ToString();
                xe.Add(xelement);
                xDoc.Save(SettingXml);
            }
        }
        #endregion

    }
}
