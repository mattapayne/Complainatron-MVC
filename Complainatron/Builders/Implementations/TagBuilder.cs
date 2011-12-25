using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Complainatron.Models;
using Complainatron.Domain;

namespace Complainatron.Builders.Implementations
{
    public class TagBuilder : ITagBuilder
    {
        public TagViewModel BuildViewModel(Tag tag)
        {
            return new TagViewModel() { 
                Id = tag.Id,
                Name = tag.Name
            };
        }
    }
}