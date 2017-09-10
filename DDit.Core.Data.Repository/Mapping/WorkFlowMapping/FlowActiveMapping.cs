using DDit.Core.Data.Entity.WorkFlowEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.WorkFlowMapping
{
    public class FlowActiveMapping : EntityTypeConfiguration<FlowActive>
    {
        public FlowActiveMapping()
        {
            HasKey(a => a.ActiveId);
            ToTable("FLOWACTIVE", "FLOW");

            this.Property(a => a.ActiveId).HasColumnName("ActiveId");
            this.Property(a => a.FlowId).HasColumnName("FlowId");

            this.Property(a => a.from).HasColumnName("Viewfrom");
            this.Property(a => a.to).HasColumnName("Viewto");
            this.Property(a => a.type).HasColumnName("type");

            this.Property(a => a.name).HasColumnName("name");

            this.Property(a => a.alt).HasColumnName("alt");

            this.Property(a => a.FlowLineName).HasColumnName("FlowLineName");

            HasMany(a => a.ConditionInfo).WithRequired().HasForeignKey(m=>m.ActiveId);
        }
    }
}
