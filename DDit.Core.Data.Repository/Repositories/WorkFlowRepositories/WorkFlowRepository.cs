using DDit.Component.Tools;
using DDit.Core.Data.Entity.WorkFlowEntity;
using DDit.Core.Data.IRepositories.IWorkFlowRepositories;
using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper;
using Z.EntityFramework.Plus;
using DDit.Core.Data.Entity.WorkFlowEntity.DoEntity;
using System.Web.Script.Serialization;
using System.Dynamic;
using DDit.Core.Data.Entity;

namespace DDit.Core.Data.Repository.Repositories.WorkFlowRepositories
{
    class WorkFlowRepository : IWorkFlowRepository
    {

        public Tuple<int, List<WorkFlowDo>> GetWorkFlowItem(WorkFlow model)
        {

            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var messageRepository = dal.GetRepository<WorkFlow>();
                var conditions = ExpandHelper.True<WorkFlow>();

                if (!string.IsNullOrEmpty(model.FlowName))
                    conditions = conditions.And(a => a.FlowName.Contains(model.FlowName));

                var templist = messageRepository.Get(filter: conditions, includeProperties: "CuserInfo,forminfo,SortInfo,flowSteps").ProjectToQueryable<WorkFlowDo>();

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

                return new Tuple<int, List<WorkFlowDo>>(count, result);
            }
        }


        public void SaveFlowViewInfo(int flowid, string jsonstr)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var fa = dal.GetRepository<FlowActive>();

                var fs= dal.GetRepository<FlowStep>();

                dal.Set<FlowActive>().Where(a => a.FlowId == flowid).Delete();

                dal.Set<FlowStep>().Where(a => a.FlowId == flowid).Delete();

                dal.Set<ActiveCondition>().Where(a => a.FlowId == flowid).Delete();

                #region 分析json字符串
                var siSerializer = new JavaScriptSerializer();
                var objects = siSerializer.Deserialize<FlowView>(jsonstr);

                var nodes = objects.nodes;
                var steps = new List<FlowStep>();
                foreach (var ss in nodes.Keys)
                {
                    var nodekey = ss;
                    var step = ExpandHelper.DicToObject<FlowStep>(nodes[nodekey]);
                    step.flowNodeName = nodekey;
                    step.stepName = step.name;
                    step.FlowId = flowid;
                    steps.Add(step);
                }

                var lines = objects.lines;
                var actives = new List<FlowActive>();
                foreach (var item in lines.Keys)
                {
                    var linekey = item;
                    var linekalue = lines[linekey];
                    var active = ExpandHelper.DicToObject<FlowActive>(linekalue);

                    var condis = new List<ActiveCondition>();
                    if (linekalue.ContainsKey("ConditionInfo"))
                    {
                        foreach (var cc in linekalue["ConditionInfo"])
                        {
                            var condition = ExpandHelper.DicToObject<ActiveCondition>(cc);
                            condition.FlowId = flowid;
                            condis.Add(condition);
                        }
                    }
                    active.ConditionInfo = condis;
                    active.FlowId = flowid;
                    active.FlowLineName = linekey;
                    actives.Add(active);
                }
                #endregion

                fa.Insert(actives);

                fs.Insert(steps);

                dal.Save();
            }
        }


        public FlowView GetFlowViewInfo(int flowid)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var fv = new FlowView();
                var fa = dal.GetRepository<FlowActive>();
                var fs = dal.GetRepository<FlowStep>();

                var nodesModel=fs.Get(a => a.FlowId == flowid).ToList();

                var nodes = new ExpandoObject() as IDictionary<string, Object>;
                nodesModel.ForEach(a =>
                {
                    nodes.Add(a.flowNodeName, a);
                });

                fv.nodes = nodes;

                var linesModel = fa.Get(a => a.FlowId == flowid,includeProperties:"ConditionInfo").ToList();
                var lines = new ExpandoObject() as IDictionary<string, Object>;
                linesModel.ForEach(a =>
                {
                    lines.Add(a.FlowLineName,a);
                });

                fv.lines = lines;
                fv.title = "dditFlow";
                fv.areas = new { };

                return fv;
            }
        }


        public bool HasFlowView(int flowid)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var flowModel = dal.GetRepository<WorkFlow>().Get(a => a.FlowID == flowid, includeProperties: "flowSteps").FirstOrDefault();

                return flowModel.flowSteps.Count > 0 ? true : false;
            }
        }


        public void AddWorkFlow(WorkFlow model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<WorkFlow>().Insert(model);

                dal.Save();
            }
        }


        public void MoidfyWorkFlow(WorkFlow model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<WorkFlow>().UpdateSup(model, new List<string>() { "CreateTime", "CreateUser" }, false);

                dal.Save();
            }
        }

        public bool RemoveWorkFlow(WorkFlow model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var flowinfo = dal.GetRepository<FlowInfo>().Get(a => a.FlowId == model.FlowID).ToList();

                if (flowinfo.Count > 0) return false;

                dal.GetRepository<WorkFlow>().Delete(model.FlowID);

                dal.Save();
                return true;
            }
        }
    }
}
