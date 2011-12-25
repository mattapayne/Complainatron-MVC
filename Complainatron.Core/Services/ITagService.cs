using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Complainatron.Domain;
using Complainatron.Core.DTOs;

namespace Complainatron.Core.Services
{
    public interface ITagService : IService<Tag>
    {
        Tag FindByName(string tagName);
        IEnumerable<CountedTag> GetTagsWithCount();
    }
}
