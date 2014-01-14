using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseComparer.Common
{
    public class UIControlHelper
    {
        #region 程序退出
        public static void ApplicationExit()
        {
            Application.ExitThread();
        }
        #endregion
    }
}
