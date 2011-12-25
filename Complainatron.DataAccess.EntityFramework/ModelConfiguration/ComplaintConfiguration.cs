using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Complainatron.Domain;

namespace Complainatron.DataAccess.EntityFramework.ModelConfiguration
{
    internal class ComplaintConfiguration : EntityTypeConfiguration<Complaint>
    {
        public ComplaintConfiguration()
        {
            HasKey(e => e.Id);
            Property(e => e.ComplaintText).IsRequired().IsMaxLength();
            Property(e => e.DateCreated).IsRequired().HasColumnType("datetime2");
            Property(e => e.FacebookUserId).IsRequired();
            Property(e => e.FacebookUserName).HasMaxLength(100).IsRequired();
            Property(e => e.Latitude).IsOptional();
            Property(e => e.Longitude).IsOptional();
            HasRequired(e => e.Severity).WithMany().HasForeignKey(e => e.ComplaintSeverityId).WillCascadeOnDelete(false);
            HasMany(e => e.Tags).WithMany(t => t.Complaints).Map(conf => conf.MapLeftKey("ComplaintId").MapRightKey("TagId"));
        }
    }
}
