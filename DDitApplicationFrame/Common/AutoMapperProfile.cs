using AutoMapper;
using DDit.Core.Data.Entity.SystemEntity;
using DDit.Core.Data.Entity.SystemEntity.DoEntity;
using DDit.Core.Data.Entity.WorkFlowEntity;
using DDit.Core.Data.Entity.WorkFlowEntity.DoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDitApplicationFrame.Common
{
    public class AutoMapperProfile:Profile
    {
        //automapper6.0一下版本写法
        //protected override void Configure()
        //{
        //    CreateMap<Menu, MenuDo>()
        //   .ForMember(de => de.MenuParentName, op => { op.MapFrom(s => s.Father.MenuName); });
        //}

        public AutoMapperProfile() {

             base.CreateMap<Menu, MenuDo>()
             .ForMember(de => de.MenuParentName, op => { op.MapFrom(s => s.Father.MenuName); });

             base.CreateMap<Message, MessageDo>()
             .ForMember(de => de.SendUserName, op => { op.MapFrom(s => s.SendUserInfo.UserReallyname); });

             base.CreateMap<FlowInfo, FlowTaskDo>()
             .ForMember(de => de.FlowCategory, op => { op.MapFrom(s => s.WorkFlowInfo.SortInfo.DicValue); })
             .ForMember(de => de.UserName, op => { op.MapFrom(s => s.Userinfo.UserReallyname); })
             .ForMember(de => de.FlowName, op => { op.MapFrom(s => s.WorkFlowInfo.FlowName); })
             .ForMember(de => de.FlowRemark, op => { op.MapFrom(s => s.WorkFlowInfo.remark); });

             base.CreateMap<WorkFlow, WorkFlowDo>()
             .ForMember(de => de.CreateUserName, op => { op.MapFrom(s => s.CuserInfo.UserReallyname); })
             .ForMember(de => de.FormName, op => { op.MapFrom(s => s.forminfo.FormName); })
             .ForMember(de => de.IsPerfect, op => { op.MapFrom(s => s.flowSteps.Count > 0 ? true : false); })
             .ForMember(de => de.FlowSortName, op => { op.MapFrom(s => s.SortInfo.DicValue); });

             base.CreateMap<FlowApprove, FlowApproveInfoDo>()
             .ForMember(de => de.FlowName, op => { op.MapFrom(s => s.FlowTaskInfo.WorkFlowInfo.FlowName); })
             .ForMember(de => de.FormId, op => { op.MapFrom(s => s.FlowTaskInfo.FormId); })
             .ForMember(de => de.FormInfoId, op => { op.MapFrom(s => s.FlowTaskInfo.FormInfoId); })
             .ForMember(de => de.StartTime, op => { op.MapFrom(s => s.FlowTaskInfo.CreateTime); })
             .ForMember(de => de.StartUserName, op => { op.MapFrom(s => s.FlowTaskInfo.Userinfo.UserReallyname); })
             .ForMember(de => de.FlowStepName, op => { op.MapFrom(s => s.FlowStepInfo.name); });
            
        }
    }
}