using System.Text.RegularExpressions;

namespace QuanLyPhongTro.Models.Pagination
{
    public class Pager
    {
        public int TotalItems { get;private set; }
        public int CurentPage { get;private set; }
        public int PageSize { get;private set; }
        public int TotalPage { get;private set; }
        public int StartPage { get;private set; }
        public int EndPage { get;private set; }

        public Pager()
        {

        }
        public Pager(int totalItems, int page, int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int curentPage = page;

            int start = curentPage - 5;
            int end = curentPage + 4;
            
            if(start < 0)
            {
                end = end - (start - 1);
                start = 1;
            }
            if(end > totalPages)
            {
                end = totalPages;
                if(end > 10)
                {
                    start = end - 9;
                }
            }

            TotalItems = totalItems;
            CurentPage = curentPage;
            PageSize = pageSize;
            TotalPage = totalPages;
            StartPage = start;
            EndPage = end;
        }
    }
}
