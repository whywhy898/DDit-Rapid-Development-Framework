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
    public class SystemInfoMapping : EntityTypeConfiguration<SystemInfo>
    {
        public SystemInfoMapping() {
            HasKey(a => a.SystemID);
            this.ToTable("SystemInfo", "Base");

            this.Property(a => a.SystemID).HasColumnName("System_ID");
            this.Property(a => a.SystemTitle).HasColumnName("System_Title");
            this.Property(a => a.SystemCopyright).HasColumnName("System_Version");
            this.Property(a => a.SystemVersion).HasColumnName("System_Copyright");
            this.Property(a => a.IsValidCode).HasColumnName("IsValidCode");

        }
    }
}
