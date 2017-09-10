using DDit.Core.Data.Entity.FormEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.FormMapping
{
    public class FormElementMapping : EntityTypeConfiguration<FormElement>
    {
        public FormElementMapping() {

            HasKey(a => a.FEId);
            ToTable("Form_Element", "Form");

            Property(a => a.FEId).HasColumnName("FEId");
            Property(a => a.FormId).HasColumnName("FormId");
            Property(a => a.ElementId).HasColumnName("ElementId");
            Property(a => a.FieldIden).HasColumnName("FieldIden");
            Property(a => a.ElementLable).HasColumnName("ElementLable");
            Property(a => a.ElementValid).HasColumnName("ElementValid");
            Property(a => a.ElementHeight).HasColumnName("ElementHeight");
            Property(a => a.ElementOrder).HasColumnName("ElementOrder");
            Property(a => a.DataDictionary).HasColumnName("DataDictionary");
            Property(a => a.ElementDefValue).HasColumnName("ElementDefValue");
            Property(a => a.ElementFormatType).HasColumnName("ElementFormatType");
          
            HasRequired(a => a.element).WithMany().HasForeignKey(a => a.ElementId);
        }
    }
}
