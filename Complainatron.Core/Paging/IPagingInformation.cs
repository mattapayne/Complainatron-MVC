using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Core.Paging
{
    public interface IPagingInformation
    {
        int ResultsPerPage { get; set; }
        int Page { get; set; }
        string SortBy { get; set; }
        SortDirection SortDirection { get; set; }
    }
}
