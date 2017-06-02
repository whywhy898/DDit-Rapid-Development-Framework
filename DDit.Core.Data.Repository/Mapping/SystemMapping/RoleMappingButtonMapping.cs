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
    public class RoleMappingButtonMapping : EntityTypeConfiguration<RoleMappingButton>
    {
        public RoleMappingButtonMapping()
        {
            HasKey(a => a.ID);
            ToTable("ROLE_BUTTON", "Base");
            this.Property(a => a.ID).HasColumnName("ID");
            this.Property(a => a.RoleID).HasColumnName("ROLE_ID");
            this.Property(a => a.MenuID).HasColumnName("MENU_ID");
            this.Property(a => a.ButtonID).HasColumnName("BUTTON_ID");

            HasRequired(a => a.RoleModel).WithMany(p => p.rbList).HasForeignKey(a => a.RoleID);

            HasRequired(a => a.ButtonModel).WithMany(p => p.rbList).HasForeignKey(a => a.ButtonID);
        }
    }
}
