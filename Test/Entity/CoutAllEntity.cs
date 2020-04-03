using System.Collections.Generic;

namespace Test.Entity
{
    public class CoutAllEntity
    {
        public string DataName { get; set; }
        public int Count { get; set; }
        //总页数
        public int TotalPages { get; set; }
        //分页url
        public string PaginationUrl { get; set; }
        //下一分页url
        public string NextPaginationUrl { get; set; }
        public List<UrlEntity> urlEntities { get; set; }
    }
}