using DDit.Core.Data.Entity.WorkFlowEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.WorkFlowMapping
{
    public class FlowInfoMapping : EntityTypeConfiguration<FlowInfo>
    {
        public FlowInfoMapping() {


            HasKey(a => a.FlowInfoId);
            ToTable("FLOWINFO", "FLOW");

            this.Property(a => a.FlowInfoId).HasColumnName("FlowInfoId");
            this.Property(a => a.FlowId).HasColumnName("FlowId");

            this.Property(a => a.FormId).HasColumnName("FormId");
            this.Property(a => a.FormInfoId).HasColumnName("FormInfoId");
            this.Property(a => a.UserId).HasColumnName("UserId");
            this.Property(a => a.FlowStepId).HasColumnName("FlowStepId");

            this.Property(a => a.FlowInfoState).HasColumnName("FlowInfoState");
            this.Property(a => a.CreateTime).HasColumnName("CreateTime");

            HasMany(a => a.Approves).WithRequired(a=>a.FlowTaskInfo).HasForeignKey(m => m.FlowInfoId);

            HasRequired(a => a.WorkFlowInfo).WithMany().HasForeignKey(m => m.FlowId);

            HasRequired(a => a.Userinfo).WithMany().HasForeignKey(m => m.UserId);
        
        }
    }
}
