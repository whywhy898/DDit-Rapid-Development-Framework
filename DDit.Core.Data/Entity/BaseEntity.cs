using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using DDit.Component.Tools;

namespace DDit.Core.Data.Entity
{
   [ModelBinder(typeof(PropertyModelBinder))]
   public class BaseEntity
    {
        [NotMapped]
        [JsonIgnore]
        [PropertyModelBinder("start")]
        public int pageIndex { get; set; }

        [NotMapped]
        [JsonIgnore]
        [PropertyModelBinder("length")]
        public int pageSize { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string draw { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<Orderby> order { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<Datacolumn> columns { get; set; }          
    }

   public class Orderby {
       public string column { get; set; }

       public string dir { get; set; }
   }

   public class Datacolumn {
       public string data { get; set; }

       public string name { get; set; }

       public bool searchable { get; set; }

       public bool orderable { get; set; }
   }

}
