using MyDataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters
{
   public class DataTableAdapter<T> where T : class
    {
        public  DataTableResponse<T> GetResponse(DataTablesRequest request, IEnumerable<T> enumerable)
        {
            var result = new DataTableResponse<T>();

            int start = request.start; // 起始資料列索引直(略過幾筆)

            //jQuery DataTables的排序資料行
            var tempQuery = this.ApplySorting(enumerable, request.columns, request.order);

            //查詢&排序後的總筆數
            var count = result.recordsTotal = result.recordsFiltered = tempQuery.Count();

            if (request.start > 0)
            {
                tempQuery = tempQuery.Skip(request.start);
            }
            if (request.length > 0)
            {
                tempQuery = tempQuery.Take(request.length);
            }

            result.data = tempQuery;
            return result ;
        }

        private IEnumerable<T> ApplySorting(IEnumerable<T> query, IEnumerable<column> columns, IEnumerable<order> order)
        {
            string sorting = this.GetSorting(columns, order) ?? "";
            return query.OrderBy(o => sorting);
        }

        private  string GetSorting(IEnumerable<column> columns, IEnumerable<order> orders)
        {
            if (orders == null)
                return null;

            foreach (var order in orders)
            {
                order.columnName = columns.ElementAt(order.column).data;
            }

            var expression = string.Join(",", orders.Select(s => s.columnName + " " + s.dir));
            return expression.Length > 0 ? expression : null;
        }
    }
}
