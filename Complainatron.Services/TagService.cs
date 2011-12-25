using System;
using System.Linq;
using Complainatron.Core.DataAccess;
using Complainatron.Core.Services;
using Complainatron.Domain;
using System.Collections.Generic;
using Complainatron.Core.DTOs;

namespace Complainatron.Services
{
    public class TagService : BaseService<Tag>, ITagService
    {
        public TagService(IDbContext context)
            : base(context)
        {

        }

        public Tag FindByName(string tagName)
        {
            return DbContext.Set<Tag>().Where(t => t.Name.ToLower() == tagName.ToLower()).FirstOrDefault();
        }

        public IEnumerable<CountedTag> GetTagsWithCount()
        {
            return DbContext.Set<Tag>().Select(t => new CountedTag() { Name = t.Name, Count = t.Complaints.Count }).ToList();
        }
    }
}