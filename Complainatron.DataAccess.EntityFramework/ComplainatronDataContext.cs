using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Complainatron.Core.DataAccess;
using Complainatron.Domain;
using Complainatron.DataAccess.EntityFramework.ModelConfiguration;

namespace Complainatron.DataAccess.EntityFramework
{
    public class ComplainatronDataContext : DbContext, IDbContext
    {
        public IDbSet<Complaint> Complaints { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<Log> Logs { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<ComplaintSeverity> ComplaintSeverities { get; set; }

        public ComplainatronDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder b)
        {
            b.Configurations.Add(new ComplaintConfiguration());
            b.Configurations.Add(new TagConfiguration());
            b.Configurations.Add(new LogConfiguration());
            b.Configurations.Add(new UserConfiguration());
            b.Configurations.Add(new ComplaintSeverityConfiguration());

            base.OnModelCreating(b);
        }
       
        public new IDbContextSet<T> Set<T>() where T : class
        {
            return new EntityFrameworkDbContextSet<T>(this);
        }

        public IDbPropertyValues GetCurrentValues<T>(T entry) where T : class
        {
            var x = this.Entry<T>(entry);
            return new EntityFrameworkDbPropertyValues(x.OriginalValues);
        }
    }
}
