using DDit.Component.Tools;
using DDit.Core.Data.Entity.WorkFlowEntity;
using DDit.Core.Data.Entity.WorkFlowEntity.DoEntity;
using DDit.Core.Data.IRepositories.IWorkFlowRepositories;
using System;
using Autofac;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;

namespace DDit.Core.Data.Repository.Repositories.WorkFlowRepositories
{
    class FlowTaskRepository : IFlowTaskRepository
    {
        public Tuple<int, List<FlowTaskDo>> GetFlowInfoBySelf(FlowInfo model)
        {
            Mapper.Initialize(a =>
            {
                a.CreateMap<FlowInfo, FlowTaskDo>()
                    .ForMember(de => de.FlowCategory, op => { op.MapFrom(s => s.WorkFlowInfo.SortInfo.DicValue); })
                    .ForMember(de => de.UserName, op => { op.MapFrom(s => s.Userinfo.UserReallyname); })
                    .ForMember(de => de.FlowName, op => { op.MapFrom(s => s.WorkFlowInfo.FlowName); })
                    .ForMember(de => de.FlowRemark, op => { op.MapFrom(s => s.WorkFlowInfo.remark); });
            });

            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {

                var messageRepository = dal.GetRepository<FlowInfo>();
                var conditions = ExpandHelper.True<FlowInfo>().And(a => a.UserId == model.UserId);

                if (model.FlowId > 0)
                    conditions = conditions.And(a => a.FlowId == model.FlowId);

                if (model.UserId > 0)
                    conditions = conditions.And(a => a.UserId == model.UserId);

                if (model.WorkFlowInfo != null) {
                    conditions = conditions.And(a => a.WorkFlowInfo.FlowName.Contains(model.WorkFlowInfo.FlowName));
                }

                var templist = dal.GetRepository<FlowInfo>().Get(conditions, includeProperties: "WorkFlowInfo,Userinfo,WorkFlowInfo.SortInfo").ProjectToQueryable<FlowTaskDo>();

                var count = templist.Count();

                if (model.order != null && model.order.Count() > 0)
                {
                    foreach (var item in model.order)
                    {
                        var column = model.columns.ElementAt(int.Parse(item.column));
                        templist = templist.OrderSort(column.data, item.dir);
                    }
                }

                var result = templist.PageBy(model.pageIndex, model.pageSize).ToList();

                return new Tuple<int, List<FlowTaskDo>>(count, result);
            }
        }


        public Tuple<int, List<FlowApproveInfoDo>> GetFlowApproveInfo(FlowApprove model)
        {
            Mapper.Initialize(a =>
           {
               a.CreateMap<FlowApprove, FlowApproveInfoDo>()
                   .ForMember(de => de.FlowName, op => { op.MapFrom(s => s.FlowTaskInfo.WorkFlowInfo.FlowName); })
                   .ForMember(de => de.FormId, op => { op.MapFrom(s => s.FlowTaskInfo.FormId); })
                   .ForMember(de => de.FormInfoId, op => { op.MapFrom(s => s.FlowTaskInfo.FormInfoId); })
                   .ForMember(de => de.StartTime, op => { op.MapFrom(s => s.FlowTaskInfo.CreateTime); })
                   .ForMember(de => de.StartUserName, op => { op.MapFrom(s => s.FlowTaskInfo.Userinfo.UserReallyname); })
                   .ForMember(de => de.FlowStepName, op => { op.MapFrom(s => s.FlowStepInfo.name); });
           });

            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var messageRepository = dal.GetRepository<FlowApprove>();
                var conditions = ExpandHelper.True<FlowApprove>().And(a=>a.ApproveResult==0);

                if (model.FlowInfoId > 0)
                    conditions = conditions.And(a => a.FlowInfoId == model.FlowInfoId);

                if (!string.IsNullOrEmpty(model.ApproveUser))
                    conditions = conditions.And(a => a.ApproveUser.StartsWith(model.ApproveUser + ",") ||
                                                                   a.ApproveUser.Contains("," + model.ApproveUser + ",") ||
                                                                   a.ApproveUser.EndsWith("," + model.ApproveUser) ||
                                                                   a.ApproveUser == model.ApproveUser);

                var templist = dal.GetRepository<FlowApprove>().Get(conditions, includeProperties: "FlowStepInfo,FlowTaskInfo,FlowTaskInfo.Userinfo,FlowTaskInfo.WorkFlowInfo").ProjectToQueryable<FlowApproveInfoDo>();

                var count = templist.Count();

                if (model.order != null && model.order.Count() > 0)
                {
                    foreach (var item in model.order)
                    {
                        var column = model.columns.ElementAt(int.Parse(item.column));
                        templist = templist.OrderSort(column.data, item.dir);
                    }
                }

                var result = templist.PageBy(model.pageIndex, model.pageSize).ToList();

                return new Tuple<int, List<FlowApproveInfoDo>>(count, result);
            }
        }


        public Tuple<int, List<FlowApproveDo>> GetFlowApproveSelf(FlowApprove model)
        {
            Mapper.Initialize(a =>
            {
                a.CreateMap<FlowApprove, FlowApproveDo>()
                    .ForMember(de => de.ApproveUserName, op => { op.MapFrom(s => s.ApproveUser); })
                    .ForMember(de => de.ReallyApproveUserName, op => { op.MapFrom(s => s.ReallyApproveUserInfo.UserReallyname); })
                    .ForMember(de => de.FlowStepName, op => { op.MapFrom(s => s.FlowStepInfo.name); });
            });

            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var messageRepository = dal.GetRepository<FlowApprove>();
                var conditions = ExpandHelper.True<FlowApprove>();

                if (model.FlowInfoId > 0)
                    conditions = conditions.And(a => a.FlowInfoId == model.FlowInfoId);

                if (!string.IsNullOrEmpty(model.ApproveUser))
                    conditions = conditions.And(a => a.ApproveUser.StartsWith(model.ApproveUser + ",") ||
                                                                   a.ApproveUser.Contains("," + model.ApproveUser + ",") ||
                                                                   a.ApproveUser.EndsWith("," + model.ApproveUser) ||
                                                                   a.ApproveUser == model.ApproveUser);

                var templist = dal.GetRepository<FlowApprove>().Get(conditions, includeProperties: "FlowStepInfo,FlowTaskInfo").ProjectToQueryable<FlowApproveDo>();

                var count = templist.Count();

                if (model.order != null && model.order.Count() > 0)
                {
                    foreach (var item in model.order)
                    {
                        var column = model.columns.ElementAt(int.Parse(item.column));
                        templist = templist.OrderSort(column.data, item.dir);
                    }
                }

                var result = templist.OrderBy(a => a.ApproveId).PageBy(model.pageIndex, model.pageSize).ToList();

                result.ForEach(a =>
                {
                    var name = "";
                    if (a.ApproveUserName.Contains(','))
                    {
                        var cc = a.ApproveUserName.Split(',');
                        foreach (var item in cc)
                        {
                            var approveid=int.Parse(item);
                            var usrmoel = dal.GetRepository<DDit.Core.Data.Entity.SystemEntity.User>().Get(b => b.UserID == approveid).FirstOrDefault();
                            name += usrmoel.UserReallyname + ",";
                        }
                        a.ApproveUserName = name.Substring(0, name.Length - 1);
                    }
                    else {
                        var approveid= int.Parse(a.ApproveUserName);
                        a.ApproveUserName = dal.GetRepository<DDit.Core.Data.Entity.SystemEntity.User>().Get(b => b.UserID == approveid).FirstOrDefault().UserReallyname;
                    }

                });

                return new Tuple<int, List<FlowApproveDo>>(count, result);
            }
        }


        public void ApproveActive(FlowApprove model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var ConnectionString = dal.context.Database.Connection.ConnectionString;

                #region 执行审批

                var flowapproveRepository = dal.GetRepository<FlowApprove>();

                var flowapprove = flowapproveRepository.Get(a => a.ApproveId == model.ApproveId).AsNoTracking().FirstOrDefault();

                flowapprove.ApproveTime = DateTime.Now;
                flowapprove.ApproveResult = model.ApproveResult;
                flowapprove.ApproveRemark = model.ApproveRemark;
                flowapprove.ReallyApproveUser = model.ReallyApproveUser;
                flowapproveRepository.UpdateSup(flowapprove, new List<string>() { "ApproveTime", "ApproveResult", "ApproveRemark", "ReallyApproveUser" });

                var flowinfo = dal.GetRepository<FlowInfo>().Get(a => a.FlowInfoId == flowapprove.FlowInfoId).AsNoTracking().FirstOrDefault();

                if (flowapprove.ApproveResult == 2) {

                    flowinfo.FlowInfoState = FlowState.Reject;

                    dal.GetRepository<FlowInfo>().UpdateSup(flowinfo, new List<string>() { "FlowInfoState" });

                    dal.Save();
                    return;
                }

                #endregion

                #region 流任务处理

                

                var workflowInfo = dal.GetRepository<WorkFlow>().Get(a => a.FlowID == flowinfo.FlowId, includeProperties: "forminfo,flowSteps,flowActives,activeCondis").FirstOrDefault();

                var startStep = workflowInfo.flowSteps.Where(a =>a.StepId == flowapprove.FlowStepId).FirstOrDefault();

                var line = workflowInfo.flowActives.Where(a => a.from == startStep.flowNodeName).ToList();

                var fleidsDis = new Dictionary<string, object>();

                var sql = "select * from " + workflowInfo.forminfo.DBName + " where " + workflowInfo.forminfo.FieldKey + "=" + flowinfo.FormInfoId + "";

                var dt = CommonHelper.SqlQueryForDataTatable(ConnectionString, sql);
                
                for (int i = 0; i < dt.Columns.Count; i++)
		        {
                    var ln = dt.Columns[i].ColumnName;
                    fleidsDis.Add(ln, dt.Rows[0][ln]);
		        }

                Func<string,string,bool,bool> MathCalculate = (condits,Itemto,ispp) =>
                {
                    if ((bool)CommonHelper.MathCalculate(condits))
                    {
                        var nextStep = workflowInfo.flowSteps.Where(c => c.flowNodeName == Itemto).FirstOrDefault();
                        flowinfo.FlowInfoState = nextStep.stepName == "结束" ? FlowState.Finish : FlowState.Underway;
                        flowinfo.FlowStepId = nextStep.StepId;
                        ispp = true;
                    }
                    else
                    {
                        flowinfo.FlowInfoState = FlowState.Failure;
                        flowinfo.FlowStepId = 0;
                    }
                    return ispp;
                };

                Func<WorkFlow, FlowActive, bool> ApproveProcess = (workflow, flowActive) =>
                {
                    var ispp = false;
                    var condit = dal.GetRepository<ActiveCondition>().Get(b => b.ActiveId == flowActive.ActiveId).OrderBy(b => b.Index).ToList();
                    if (condit.Count == 0)
                    {   //没有条件
                        var nextStep = workflow.flowSteps.Where(c => c.flowNodeName == flowActive.to).FirstOrDefault();
                        flowinfo.FlowInfoState = nextStep.stepName == "结束" ? FlowState.Finish : FlowState.Underway;
                        flowinfo.FlowStepId = workflow.flowSteps.Where(b => b.flowNodeName == flowActive.to).FirstOrDefault().StepId;
                        ispp = true;
                    }
                    else if (condit.Count == 1)
                    {  //有一个条件
                        fleidsDis.ToList().ForEach(a =>
                        {
                            if (condit[0].Field == a.Key)
                            {
                                var cond = a.Value + condit[0].Compare + condit[0].CompareValue;
                                ispp = MathCalculate(cond, flowActive.to, ispp);
                            }
                        });

                    }
                    else
                    {  //有多个条件
                        var cond = "";
                        var isppy = false;
                        condit.ForEach(a =>
                        {
                            fleidsDis.ToList().ForEach(c =>
                            {
                                if (a.Field == c.Key) cond += c.Key + a.Compare + a.CompareValue + a.Logic;
                            });
                        });

                        ispp = MathCalculate(cond, flowActive.to, isppy);
                    }
                    return ispp;
                };

                if (line.Count > 1) //多条线
                {
                    foreach (var item in line)
                    {
                        if (ApproveProcess(workflowInfo, item)) break;
                    }
                }
                else {  //一条线

                    ApproveProcess(workflowInfo, line[0]);
                }
              
                dal.GetRepository<FlowInfo>().UpdateSup(flowinfo, new List<string>() { "FlowInfoState", "FlowStepId" });

                var EndStep = workflowInfo.flowSteps.Where(a => a.StepId == flowinfo.FlowStepId).FirstOrDefault();

                if (flowinfo.FlowStepId != 0&&EndStep.stepName!="结束")
                {
                    var flowstep = dal.GetRepository<FlowStep>().Get(a => a.StepId == flowinfo.FlowStepId).FirstOrDefault();

                    var flowApprove = new FlowApprove();
                    flowApprove.FlowInfoId = flowinfo.FlowInfoId;
                    flowApprove.ApproveUser = flowstep.stepUser;
                    flowApprove.ApproveResult = 0;
                    flowApprove.FlowStepId = flowstep.StepId;

                    dal.GetRepository<FlowApprove>().Insert(flowApprove);
                }

                #endregion

                dal.Save();

            }
        }
    }
}
