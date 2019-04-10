using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thinkpower.ModuleResolver.URF
{
    /// <summary>
    /// DataContext 的擴充介面
    /// </summary>
    /// <remarks>
    /// 因 ModelResolver 為 Entities 及 Services 的基底類別庫，
    /// 為了不要有太多基底類別庫，所以先將該介面放置在此類別庫中。
    /// </remarks>
    public interface IDataContextHelper
    {
        DbConnection CreateConnection();

        /// <summary>
        /// 透過 EF 執行 ExecuteSqlCommand 語法，使用與目前的 Context 同一條連線。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);

        System.Data.Entity.DbContextTransaction BeginTransaction();

        DbRawSqlQuery<T> SqlQuery<T>(string queryString, params object[] parameters);
    }
}
