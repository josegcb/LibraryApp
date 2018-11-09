using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    public class Filter<T> : IFilter where T : class {
        private List<IFilterStatement> _Statements;
        public IEnumerable<IFilterStatement> Statements {
            get {
                return _Statements.ToArray();
            }
        }
        public Filter() {
            _Statements = new List<IFilterStatement>();
        }

        public void Add<TPropertyType>(string valPropertyName, eBooleanOperatorType valBooleanOperator, eLogicOperatorType valLogicOperator, TPropertyType value) {
            Add<TPropertyType>(valPropertyName, valBooleanOperator, valLogicOperator, value, default(TPropertyType));
        }
        public void Add<TPropertyType>(string valPropertyName, eBooleanOperatorType valBooleanOperator, eLogicOperatorType valLogicOperator, TPropertyType value, TPropertyType value2) {
            IFilterStatement statement = new FilterStatement<TPropertyType>(valPropertyName, valBooleanOperator, value, value2, valLogicOperator);
            _Statements.Add(statement);
        }
        public void Clear() {
            _Statements.Clear();
        }

        public static implicit operator Func<T, bool>(Filter<T> valLilter) {
            var vFilterBuilder = new FilterBuilder();
            return vFilterBuilder.GetExpression<T>(valLilter).Compile();
        }

    }
}
