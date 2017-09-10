using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.FormEntity;
using DDit.Core.Data.Entity.WorkFlowEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DDit.Core.Data.IRepositories.IFormRepositories
{
   public interface IFormInfoRepository
    {
       Tuple<int, List<FormInfo>> GetFormInfoItems(FormInfo model);

       void AddFormInfo(FormInfo model);

       void ModifyFormInfo(FormInfo model);

       ResultEntity RemoveFormInfo(FormInfo model);

       List<string> GetTabFields(int formId);

       void InsertFormElemenet(List<FormElement> model);

       bool IsCompleteConfig(int formId);

       List<FormElement> GetElementInfo(int formId);

       FormInfo GetForminfoSingle(int formId, int forminfoId);

       void ExecuteFlow(FlowInfo flowinfo, List<string> fieldArray);

       List<dynamic> GetDBName(string value);

       List<dynamic> GetDBKeys(string DBName);

       List<dynamic> GetFormInfoBind(); 

    }
}
