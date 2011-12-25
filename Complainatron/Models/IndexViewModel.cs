using System.Collections.Generic;
using System.Linq;
using Complainatron.Core.DTOs;
using Complainatron.Core.Paging;
using MvcPaging;

namespace Complainatron.Models
{
    public class IndexViewModel
    {
        public MeDTO Me { get; set; }
        public IPagedList<ComplaintViewModel> Complaints { get; set; }
        public string TagListUrl { get; set; }
        public string Title { get; set; }

        public IndexViewModel()
        {
            Complaints = new PagedList<ComplaintViewModel>(Enumerable.Empty<ComplaintViewModel>(), 1, 10);
        }
    }
}