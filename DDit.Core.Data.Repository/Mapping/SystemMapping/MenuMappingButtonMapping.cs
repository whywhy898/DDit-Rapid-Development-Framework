
using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
    public class MenuMappingButtonMapping : EntityTypeConfiguration<MenuMappingButton>
    {
        public MenuMappingButtonMapping() {

            HasKey(a => a.ID);
            ToTable("MENU_BUTTON", "BASE");
            this.Property(a => a.ID).HasColumnName("ID");
            this.Property(a => a.MenuID).HasColumnName("MENU_ID");
            this.Property(a => a.ButtonID).HasColumnName("BUTTON_ID");
            this.Property(a => a.OrderBy).HasColumnName("ORDERBY");

            HasRequired(a => a.MenuModel).WithMany(p => p.mbList).HasForeignKey(a=>a.MenuID);

            HasRequired(a => a.ButtonModel).WithMany(p => p.mbList).HasForeignKey(a=>a.ButtonID);

        }
    }
}
