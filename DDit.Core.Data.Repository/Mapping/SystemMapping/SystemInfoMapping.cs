using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.SystemEntity;
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
            this.ToTable("SYSTEMINFO", "BASE");

            this.Property(a => a.SystemID).HasColumnName("SYSTEM_ID");
            this.Property(a => a.SystemTitle).HasColumnName("SYSTEM_TITLE");
            this.Property(a => a.SystemCopyright).HasColumnName("SYSTEM_VERSION");
            this.Property(a => a.SystemVersion).HasColumnName("SYSTEM_COPYRIGHT");
            this.Property(a => a.IsValidCode).HasColumnName("ISVALIDCODE");

        }
    }
}
