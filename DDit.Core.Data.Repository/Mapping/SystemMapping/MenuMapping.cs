
using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
    public class MenuMapping : EntityTypeConfiguration<Menu>
    {
        public MenuMapping() {

            HasKey(a=>a.MenuID);

            this.ToTable("MENU", "Base");

            this.Property(a => a.MenuID).HasColumnName("MENU_ID");
            this.Property(a => a.MenuName).HasColumnName("MENU_NAME");
            this.Property(a => a.MenuOrder).HasColumnName("MENU_ORDER");
            this.Property(a => a.MenuParentID).HasColumnName("MENU_PARENTID");
            this.Property(a => a.MenuUrl).HasColumnName("MENU_URL");
            this.Property(a => a.MenuIcon).HasColumnName("MENU_ICON");
            this.Property(a => a.IsVisible).HasColumnName("ISVISIBLE");
            this.Property(a => a.CreateTime).HasColumnName("CREATE_TIME");
            this.Property(a => a.UpdateTime).HasColumnName("UPDATE_TIME");

            HasMany(t => t.Childs)
                        .WithOptional(t => t.Father)
                        .HasForeignKey(t => t.MenuParentID);
        }

    }
}
