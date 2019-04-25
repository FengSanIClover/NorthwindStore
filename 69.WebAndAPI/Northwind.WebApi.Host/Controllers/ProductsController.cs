using Adapters;
using Northwind.Entities.Models;
using Northwind.Modules.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
//透過NuGet加入System.Linq.Dynamic，Server端Linq排序用得到
using System.Linq.Dynamic;

namespace Northwind.WebApi.Host.Controllers
{
    /// <summary>
    /// 每一筆資料Entity
    /// </summary>
    public class MyRecord
    {
        public int sysid { get; set; }
        public string MyTitle { get; set; }
        public int MyMoney { get; set; }
    }

    public class ProductsController : Controller
    {
        private List<MyRecord> myRecords = new List<MyRecord>();

        public ProductsController()
        {
            for (int i = 0; i <= 100; i += 1)
            {
                this.myRecords.Add(new MyRecord()
                {
                    sysid = i + 1,
                    MyTitle = "MyTitle" + i,
                    MyMoney = i*100
                });
            }
        }
        ProductsAdapter productsAdapter = new ProductsAdapter();
        // GET: Products
        public ActionResult Index()
        {
            var result = this.productsAdapter.SendRequest<IEnumerable<Products>>("GetProducts");
            if (result.ReturnCode == "0000")
            {
                return View(result.Result);
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="draw">DataTables 自動傳遞參數</param>
        /// <param name="start">DataTables 自動傳遞參數</param>
        /// <param name="length">DataTables 自動傳遞參數</param>
        /// <param name="MyTitle">表單的查詢條件</param>
        /// <param name="MyMoney">表單的查詢條件</param>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult AjaxDataTable(int draw, int start, int length,string MyTitle,int? MyMoney)
        {
            int skip = start; // 起始資料列索引直(略過幾筆)
            #region jQuery DataTables的排序資料行
            //jQuery DataTable的Column index
            string col_index = Request.QueryString["order[0][column]"];
            //col_index 換算成 資料行名稱
            //排序資料行名稱
            string sortColName = string.IsNullOrEmpty(col_index) ? "sysid" : Request.QueryString[$@"columns[{col_index}][data]"];
            //升冪或降冪
            string asc_desc = string.IsNullOrEmpty(Request.QueryString["order[0][dir]"]) ? "desc" : Request.QueryString["order[0][dir]"];//防呆
            #endregion

            //查詢&排序後的總筆數
            int recordsTotal = 0;

            //要回傳的分頁資料
            List<MyRecord> pageData = new List<MyRecord>();

            //總資料
            var query = this.myRecords.AsEnumerable();

            //查詢
            if (!string.IsNullOrEmpty(MyTitle))
            {
                query = this.myRecords.Where(m => m.MyTitle.Contains(MyTitle));
            }

            if (MyMoney.HasValue)
            {
                query = this.myRecords.Where(m => m.MyMoney == MyMoney);
            }

            //排序
            query = query.OrderBy($@"{sortColName} {asc_desc}"); //排序使用到System.Linq.Dynamic

            //查詢後的總筆數
            recordsTotal = query.Count();

            if (length == -1)
            {//抓全部資料
                pageData = query.ToList();
            }
            else
            {//分頁
                pageData = query.Skip(skip).Take(length).ToList();
            }

            var returnObj = new
            {
                draw = draw,
                recordsTotal = recordsTotal, // recordsTotal:經過查詢後的資料總筆數 
                recordsFiltered = recordsTotal, // 經過filter後的資料總筆數，由於未使用filter功能，所以此數值和recordsTotal一樣
                data = pageData //分頁後的資料
            };

            return Json(returnObj,JsonRequestBehavior.AllowGet);
        }


        public ActionResult JqueryBlockUI()
        {
            return View();
        }
    }
}