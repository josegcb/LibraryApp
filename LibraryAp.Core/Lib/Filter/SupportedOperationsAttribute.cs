using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    class SupportedOperationsAttribute : Attribute {
        public List<eBooleanOperatorType> SupportedOperations { get; private set; }
        public SupportedOperationsAttribute(params eBooleanOperatorType[] initSupportedOperations) {
            SupportedOperations = new List<eBooleanOperatorType>(initSupportedOperations);
        }
    }
}
