
using DDit.Core.Data.SystemEntity.Entity;
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
            ToTable("Menu_Button", "Base");
            this.Property(a => a.ID).HasColumnName("ID");
            this.Property(a => a.MenuID).HasColumnName("Menu_ID");
            this.Property(a => a.ButtonID).HasColumnName("Button_ID");
            this.Property(a => a.OrderBy).HasColumnName("OrderBy");

            HasRequired(a => a.MenuModel).WithMany(p => p.mbList).HasForeignKey(a=>a.MenuID);

            HasRequired(a => a.ButtonModel).WithMany(p => p.mbList).HasForeignKey(a=>a.ButtonID);

        }
    }
}
