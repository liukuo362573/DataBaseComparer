using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DatabaseComparer.Common
{
    public interface ISqlHelper
    {
        bool IsConnect(string connectionString);

        DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText);

    }
}
