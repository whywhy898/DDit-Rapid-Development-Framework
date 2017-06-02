using DDit.Core.Data.Entity;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
    public class TestMapping : EntityTypeConfiguration<Test>
    {
        public TestMapping() {

            this.HasKey(a => a.ID);

            this.ToTable("TEST", "TEST");
            this.Property(a => a.ID).HasColumnName("ID");
            this.Property(a => a.Name).HasColumnName("Name");
            this.Property(a => a.Age).HasColumnName("age");
        }
    }
}
