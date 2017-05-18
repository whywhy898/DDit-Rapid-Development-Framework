
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
    public class ButtonMapping : EntityTypeConfiguration<Button>
    {

        public ButtonMapping() {
            HasKey(a => a.ButtonID);
            ToTable("Button", "Base");
            this.Property(a => a.ButtonID).HasColumnName("Button_ID");
            this.Property(a => a.ButtonName).HasColumnName("Button_Name");
            this.Property(a => a.ButtonOpID).HasColumnName("Button_OpID");
            this.Property(a => a.ButtonMagic).HasColumnName("Button_Magic");
            this.Property(a => a.ButtonOperation).HasColumnName("Button_Operation");
            this.Property(a => a.ButtonDescribe).HasColumnName("Button_Describe");
            this.Property(a => a.CreateTime).HasColumnName("Create_Time");
            this.Property(a => a.UpdateTime).HasColumnName("Update_Time");

        }

    }
}
