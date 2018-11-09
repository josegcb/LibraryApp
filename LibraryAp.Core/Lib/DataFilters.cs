using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {

    public interface IDataFilters {        
        string FilterName { get; set; }
        string ParameterName { get; }
    }
    public class DataFilters {
        public const string MustHaveAgency = "IMustHaveAgency";// FilterName
        public const string MustHaveAgencyParam = "AgenciaId";// ParameterName
    }
}
