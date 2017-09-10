using DDit.Core.Data.Entity.FormEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.FormMapping
{
   public class FormInfoMapping:EntityTypeConfiguration<FormInfo>
    {
       public FormInfoMapping() {

           HasKey(a => a.FormId);
           ToTable("FormInfo", "Form");
           Property(a => a.FormId).HasColumnName("FormId");
           Property(a => a.FormName).HasColumnName("FormName");
           Property(a => a.DBName).HasColumnName("DBName");
           Property(a => a.FieldKey).HasColumnName("FieldKey");
           Property(a => a.remark).HasColumnName("remark");
           Property(a => a.CreatTime).HasColumnName("CreatTime");

           HasMany(a => a.elementPropertys).WithOptional().HasForeignKey(m => m.FormId);
       }
    }
}
