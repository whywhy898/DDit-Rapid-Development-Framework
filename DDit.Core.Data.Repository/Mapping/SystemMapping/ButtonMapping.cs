
using DDit.Core.Data.Entity.SystemEntity;
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
            ToTable("BUTTON","BASE");
            this.Property(a => a.ButtonID).HasColumnName("BUTTON_ID");
            this.Property(a => a.ButtonName).HasColumnName("BUTTON_NAME");
            this.Property(a => a.ButtonOpID).HasColumnName("BUTTON_OPID");
            this.Property(a => a.ButtonMagic).HasColumnName("BUTTON_MAGIC");
            this.Property(a => a.ButtonOperation).HasColumnName("BUTTON_OPERATION");
            this.Property(a => a.ButtonDescribe).HasColumnName("BUTTON_DESCRIBE");
            this.Property(a => a.CreateTime).HasColumnName("CREATE_TIME");
            this.Property(a => a.UpdateTime).HasColumnName("UPDATE_TIME");

        }

    }
}
