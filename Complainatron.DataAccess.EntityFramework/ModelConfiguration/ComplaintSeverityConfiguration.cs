using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Complainatron.Domain;

namespace Complainatron.DataAccess.EntityFramework.ModelConfiguration
{
    internal class ComplaintSeverityConfiguration : EntityTypeConfiguration<ComplaintSeverity>
    {
        public ComplaintSeverityConfiguration()
        {
            HasKey(e => e.Id);
            Property(e => e.IsDefault).IsRequired();
            Property(e => e.DateCreated).IsRequired().HasColumnType("datetime2");
            Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}
