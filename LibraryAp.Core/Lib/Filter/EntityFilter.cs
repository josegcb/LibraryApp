using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    public class EntityFilter {
        public string TypeName { get; set; }
        public string PropertyName { get; set; }
        public string BooleanOperator { get; set; }
        public string LogicOperator { get; set; }
        public object Value1 { get; set; }
        public object Value2 { get; set; }
        public object Value3 { get; set; }
    }
}
