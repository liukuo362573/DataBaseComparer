using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseComparer
{
    public class TreeViewEx : TreeView
    {
        public delegate void VScrollDelegate(object sender);
        public event VScrollDelegate OnVScroll;
        protected override void WndProc(ref   Message m)
        {
            if (m.Msg == 0x000F)//当消息为为WM_Paint时
            {
                if (OnVScroll != null)
                {
                    this.OnVScroll(this);
                }
            }
            base.WndProc(ref m);
        }
    }
}
