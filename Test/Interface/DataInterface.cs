using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Test.Entity;


namespace Test.Interface
{
    public class DataInterface : IDataInterface
    {
        CoutAllEntity ca = new CoutAllEntity();
        public List<UrlEntity> GetUrls { get; set; }
        public CoutAllEntity GetAll(string host, DataSet dataSet)
        {
            Run(dataSet);
            ca.Count = dataSet.Tables[0].Rows.Count;
            ca.DataName = "测试库Url";
            ca.TotalPages = ca.TotalPages <= 1 ? 1 : ca.TotalPages;
            ca.urlEntities = GetUrls;
            ca.PaginationUrl = host + "/1";
            return ca;
        }

        public CoutAllEntity GetPageSize(string host, DataSet dataSet, int pageid, int pageSize = 5)
        {
            Run(dataSet);
            var totalCount = dataSet.Tables[0].Rows.Count;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            pageSize = pageid < 1 ? totalCount : pageSize;
            var pageOfitems = GetUrls.Skip((pageid - 1) * pageSize).Take(pageSize).ToList();
            ca.DataName = "测试库分页测试Url";
            ca.TotalPages = pageid < 1 ? 1 : totalPages;
            ca.Count = pageOfitems.Count;
            ca.urlEntities = pageOfitems;
            if (pageid < totalPages)
            {
                ca.NextPaginationUrl = host.Replace(Regex.Match(host, "Url/(\\d+)").Groups[1].Value, (++pageid).ToString());
            }
            return ca;
        }

        public void Run(DataSet dataSet)
        {
            GetUrls = new List<UrlEntity>();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                GetUrls.Add(new UrlEntity
                {
                    ID = int.Parse(dataSet.Tables[0].Rows[i][0].ToString()),
                    Name = dataSet.Tables[0].Rows[i][1].ToString(),
                    Url = dataSet.Tables[0].Rows[i][2].ToString(),
                    Explain = dataSet.Tables[0].Rows[i][3].ToString(),
                });
            }
        }
    }

    public interface IDataInterface
    {
        CoutAllEntity GetAll(string host, DataSet dataSet);
        CoutAllEntity GetPageSize(string host, DataSet dataSet, int pageid, int pageSize = 5);
    }
}
