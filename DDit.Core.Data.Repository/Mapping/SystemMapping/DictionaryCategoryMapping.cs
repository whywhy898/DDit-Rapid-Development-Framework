
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.SystemMapping
{
    public class DictionaryCategoryMapping : EntityTypeConfiguration<DictionaryCategory>
    {
        public DictionaryCategoryMapping() {

            HasKey(a => a.ID);
            ToTable("DICTIONARYCATEGORY", "Base");
            this.Property(a => a.ID).HasColumnName("ID");
            this.Property(a => a.Category).HasColumnName("CATEGORY");
            this.Property(a => a.Enabled).HasColumnName("ENABLED");
            this.Property(a => a.CreateTime).HasColumnName("CREATE_TIME");
            this.Property(a => a.UpdateTime).HasColumnName("UPDATE_TIME");

           
        }

    }
}
