using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Complainatron.Core.Search
{
    public interface ISearchParameters
    {
        object GetSearchCriteria();
    }
}
