using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Models
{

    public interface IGridModel<TModel>
    {
         IEnumerable<TModel> Update { get; set; }
         IEnumerable<TModel> Delete { get; set; }
    }
    public class GridModel<TModel> : IGridModel<TModel>
    {
        public IEnumerable<TModel> Update { get; set; }
        public IEnumerable<TModel> Delete { get; set; }
    }

}
