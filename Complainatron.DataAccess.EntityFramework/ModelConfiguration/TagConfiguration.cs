using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Complainatron.Domain;

namespace Complainatron.DataAccess.EntityFramework.ModelConfiguration
{
    internal class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            HasKey(e => e.Id);
            Property(e => e.Name).IsRequired().HasMaxLength(50);
            Property(e => e.DateCreated).IsRequired().HasColumnType("datetime2");
        }
    }
}
