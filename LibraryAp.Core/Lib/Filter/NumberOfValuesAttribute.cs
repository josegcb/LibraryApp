using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    class NumberOfValuesAttribute : Attribute {
        [Range(0, 2, ErrorMessage = "Las operaciones solo pueden tener 0, 1 o 2 parametros")]
        [DefaultValue(1)]
        public int NumberOfValues { get; private set; }

        public NumberOfValuesAttribute() {
            NumberOfValues = 1;
        }
        public NumberOfValuesAttribute(int initNumberOfValues) {
            NumberOfValues = initNumberOfValues;
        }
    }
}
