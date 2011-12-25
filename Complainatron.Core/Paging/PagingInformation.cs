using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Core.Paging
{
    public class PagingInformation : IPagingInformation
    {
        public int ResultsPerPage { get; set; }
        public int Page { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }

        public PagingInformation()
        {
            ResultsPerPage = 5;
            Page = 1;
            SortBy = "DateCreated";
            SortDirection = SortDirection.Desc;
        }
    }
}
