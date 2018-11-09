using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    public interface IFilter {
        IEnumerable<IFilterStatement> Statements { get; }
        void Add<TPropertyType>(string valPropertyName, eBooleanOperatorType valBooleanOperator, eLogicOperatorType valLogicOperator, TPropertyType value);
        void Add<TPropertyType>(string valPropertyName, eBooleanOperatorType valBooleanOperator, eLogicOperatorType valLogicOperator, TPropertyType value, TPropertyType value2);
        void Clear();
    }
    public interface IFilterStatement {
        eLogicOperatorType LogicOperator { get; set; }
        string PropertyName { get; set; }
        eBooleanOperatorType BooleanOperator { get; set; }
        object Value { get; set; }
        object Value2 { get; set; }
        void Validate();
    }
}
