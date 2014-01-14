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
        public void SaveSetting(GroupBox gBox, Panel panel, ComboBox comBox, string parent)
        {
            XDocument xDoc = XDocument.Load(SettingXml);

            var check = xDoc.Descendants(parent).Elements().Where(p => p.Name == comBox.Name && p.Value == comBox.Text);
            if (check.Count() > 0)
            {
                var xe = (from x in xDoc.Descendants(parent)
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

                xe.Add(new XElement(parent, list));
                xDoc.Save(SettingXml);
            }
        }
        #endregion

    }
}
