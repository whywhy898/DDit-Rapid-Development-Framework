using DDit.Component.Tools;
using DDit.Core.Data.Entity.FormEntity;
using DDit.Core.Data.IRepositories.IFormRepositories;
using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using System.Data;
using DDit.Core.Data.Entity.WorkFlowEntity;
using DDit.Core.Data.Entity;

namespace DDit.Core.Data.Repository.Repositories.FormRepositories
{
    class FormInfoRepository : IFormInfoRepository
    {
        public Tuple<int, List<FormInfo>> GetFormInfoItems(FormInfo model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
             
                var forminfo = dal.GetRepository<FormInfo>();

                var conditions = ExpandHelper.True<FormInfo>();

                if (!string.IsNullOrEmpty(model.FormName))
                    conditions = conditions.And(a => a.FormName.Contains(model.FormName));

                var templist = forminfo.Get(filter: conditions, includeProperties: "elementPropertys");

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

                result.ForEach(a =>
                {
                    a.isConfiguration = a.elementPropertys.Count > 0 ? true : false;
                });

                return new Tuple<int, List<FormInfo>>(count, result);
            }
        }

        public List<string> GetTabFields(int formId)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>()) {

                var formModel = dal.GetRepository<FormInfo>().Get(a => a.FormId == formId).FirstOrDefault();

                var sql = @"select  (case when  CONVERT (nvarchar,c.value) is null then '无说明' else  CONVERT (nvarchar,c.value) end)+
                          '('+b.name+')' as name from syscolumns b left JOIN sys.extended_properties c on b.id=c.major_id AND b.colid = c.minor_id where b.id = Object_id('" + formModel.DBName+ "')";

                var obj = dal.context.Database.SqlQuery<string>(sql).ToList<string>();

                dal.Save();

                return obj;
            }
        }

        public void InsertFormElemenet(List<FormElement> model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>()) {
                
                var formrepotiys = dal.GetRepository<FormElement>();

                var formid=model[0].FormId;
                var ishas = dal.GetRepository<FormElement>().Get(a => a.FormId == formid).ToList();
                if (ishas.Count > 0) {
                    formrepotiys.Delete(ishas);
                }
                formrepotiys.Insert(model);
                dal.Save();
            }
        }

        public bool IsCompleteConfig(int formId)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                return dal.GetRepository<FormElement>().Get(a => a.FormId == formId).Any();
            }
        }

        public List<FormElement> GetElementInfo(int formId)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                return dal.GetRepository<FormElement>().Get(a => a.FormId == formId, includeProperties: "element").OrderBy(a=>a.ElementOrder).ToList();
            }
        }
 
        public FormInfo GetForminfoSingle(int formId,int forminfoId)
        {
             using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
             {
                 var ConnectionString = dal.context.Database.Connection.ConnectionString;
                 var result = dal.GetRepository<FormInfo>().Get(a => a.FormId == formId, includeProperties: "elementPropertys").FirstOrDefault();

                 result.elementPropertys = dal.GetRepository<FormElement>().Get(a => a.FormId == formId, includeProperties: "element").OrderBy(a => a.ElementOrder).ToList();

                 if (forminfoId != 0) { 

                     var sql="select * from "+result.DBName+" where "+result.FieldKey+"="+forminfoId+"";

                     var dt = CommonHelper.SqlQueryForDataTatable(ConnectionString, sql);

                     result.elementPropertys.ForEach(a =>
                     {
                         a.ElementValue = dt.Rows[0][a.FieldIden].ToString();
                     });
                     
                 }

                 return result;
             }
        }

        public void ExecuteFlow(FlowInfo flowinfo, List<string> formObj)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var  fleidsDis=new Dictionary<string,object>();

                #region 保存表单
                string keys = "(", values = "(";

                var formMolde = dal.GetRepository<FormInfo>().Get(a => a.FormId == flowinfo.FormId).FirstOrDefault();

                var fieldinfosql = @"SELECT syscolumns.name as name,systypes.name as type FROM syscolumns, systypes WHERE syscolumns.xusertype = systypes.xusertype 
                                   AND syscolumns.id = object_id('" + formMolde.DBName + "')";
                var cc = dal.context.Database.SqlQuery<fieldInfo>(fieldinfosql).ToList();
  
                formObj.ForEach(a =>
                {
                    var temp = a.Split('=');

                    fleidsDis.Add(temp[0].ToString(),temp[1]);

                    if (temp[0] == "formid" || temp[0] == "flowid") return;

                    if (temp[1] == "") return; 

                    var singlefieldinfo = cc.Where(b => b.name == temp[0]).FirstOrDefault();

                    keys += temp[0] + ",";
                    if (singlefieldinfo.type != "int" )
                       values +="'"+ temp[1] + "',";
                    else
                       values += temp[1] + ",";
                });

                keys = keys.Substring(0, keys.Length - 1)+")";

                values = values.Substring(0, values.Length - 1) + ")";

                var sql = "insert into " + formMolde.DBName + keys + " values " + values;

                dal.context.Database.ExecuteSqlCommand(sql,new object[]{});

                #endregion

                #region 得到插入表单信息Id
                var newidsql="select max("+formMolde.FieldKey+") from "+formMolde.DBName;

                var newid = dal.context.Database.SqlQuery<int>(newidsql).ToList();

                dal.Save();

                flowinfo.FormInfoId =newid[0];
                #endregion

                #region 得到flow信息

                var workflowInfo = dal.GetRepository<WorkFlow>().Get(a => a.FlowID == flowinfo.FlowId, includeProperties: "flowSteps,flowActives,activeCondis").FirstOrDefault();

                var startStep=workflowInfo.flowSteps.Where(a=>a.name=="开始").FirstOrDefault();

                var line = workflowInfo.flowActives.Where(a => a.from == startStep.flowNodeName).ToList();

                Func<string, string, bool, bool> MathCalculate = (condits, Itemto, ispp) =>
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
                else
                {  //一条线

                    ApproveProcess(workflowInfo, line[0]);
                }

                dal.GetRepository<FlowInfo>().Insert(flowinfo);

                #endregion

                #region 流进度创建

                if (flowinfo.FlowStepId != 0) {

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

        public void AddFormInfo(FormInfo model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                model.CreatTime = DateTime.Now;
                dal.GetRepository<FormInfo>().Insert(model);
                dal.Save();
            }
        }

        public void ModifyFormInfo(FormInfo model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<FormInfo>().UpdateSup(model, new List<string>() { "CreatTime" }, false);
                dal.Save();
            }
        }

        public Entity.ResultEntity RemoveFormInfo(FormInfo model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var forminfo = dal.GetRepository<FormInfo>().Get(a=>a.FormId==model.FormId,includeProperties:"elementPropertys").FirstOrDefault();

                var workflow = dal.GetRepository<WorkFlow>().Get(a => a.FormID == model.FormId).ToList();

                if (workflow.Count > 0) return new ResultEntity() { result = false, message = "该表单已经被工作流使用无法删除！" };

                dal.GetRepository<FormInfo>().Delete(forminfo);

                dal.GetRepository<FormElement>().Delete(forminfo.elementPropertys);

                dal.Save();

                return new ResultEntity() { result = true};
            }
        }

        public List<dynamic> GetDBName(string value)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var QuerySQL = "select s.[name]+'.'+t.name from sys.tables as t join sys.schemas as s on t.schema_id = s.schema_id where t.name like '%"+value+"%' ";

                var result = dal.context.Database.SqlQuery<string>(QuerySQL).Select(a => new { 
                    id=a,
                    text=a
                }).ToList<dynamic>();

                return result;
            }
        }

        public List<dynamic> GetDBKeys(string DBName)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var QuerySQL = "select  b.name as name from syscolumns b left JOIN sys.extended_properties c on b.id=c.major_id AND b.colid = c.minor_id where b.id = Object_id('"+DBName+"')";

                var result = dal.context.Database.SqlQuery<string>(QuerySQL).Select(a => new
                {
                    id = a,
                    text = a
                }).ToList<dynamic>();

                return result;
            }
        }


        public List<dynamic> GetFormInfoBind()
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var result = dal.GetRepository<FormInfo>().Get().Select(a => new { 
                   id=a.FormId,
                   text=a.FormName
                }).ToList<dynamic>();

                return result;
            }
        }
    }

    public class fieldInfo{
        public string name { get; set; }

        public string type { get; set; }
    }
}
