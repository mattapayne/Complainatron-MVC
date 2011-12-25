using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Complainatron.Domain;

namespace Complainatron.DataAccess.EntityFramework.ModelConfiguration
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(e => e.Id);
            Property(e => e.DateCreated).IsRequired().HasColumnType("datetime2");
            Property(e => e.FacebookId).IsRequired();
            Property(e => e.Email).IsOptional().HasMaxLength(100);
            Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}
