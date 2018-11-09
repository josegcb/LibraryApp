using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class VarcharAttribute : StringLengthAttribute {
        public VarcharAttribute(int maximumLength) : base(maximumLength) {

        }
    }
}
