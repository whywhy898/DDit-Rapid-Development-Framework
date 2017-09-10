using DDit.Core.Data.Entity.WorkFlowEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.WorkFlowMapping
{
    public class WorkFlowMapping : EntityTypeConfiguration<WorkFlow>
    {
        public WorkFlowMapping() {

            HasKey(a => a.FlowID);
            ToTable("WorkFlow", "FLOW");

            this.Property(a => a.FlowID).HasColumnName("FlowID");
            this.Property(a => a.FlowSort).HasColumnName("FlowSort");
            this.Property(a => a.FlowID).HasColumnName("FlowID");
            this.Property(a => a.FlowName).HasColumnName("FlowName");
            this.Property(a => a.remark).HasColumnName("remark");
            this.Property(a => a.CreateTime).HasColumnName("CreateTime");
            this.Property(a => a.CreateUser).HasColumnName("CreateUser");


            HasRequired(a => a.CuserInfo).WithMany().HasForeignKey(m => m.CreateUser);

            HasRequired(a => a.forminfo).WithMany().HasForeignKey(m => m.FormID);

            HasRequired(a => a.SortInfo).WithMany().HasForeignKey(m => m.FlowSort);

            HasMany(a => a.flowSteps).WithRequired().HasForeignKey(m => m.FlowId);

            HasMany(a => a.flowActives).WithRequired().HasForeignKey(m => m.FlowId);

            HasMany(a => a.activeCondis).WithRequired().HasForeignKey(m => m.FlowId);
        }
    }
}
