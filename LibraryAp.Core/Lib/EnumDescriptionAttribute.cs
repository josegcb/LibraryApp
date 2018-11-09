using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumDescriptionAttribute : Attribute, IComparable<EnumDescriptionAttribute> {
        #region Variables        
        #endregion

        #region Propiedades
        public string Description {
            get;
            set;
        }

        public int Index {
            get;
            set;
        }

        #endregion

        #region Constructores
        public EnumDescriptionAttribute(string iniDescription) {
            Description = iniDescription;
        }
        
        #endregion

        #region Métodos Públicos
        public override string ToString() {
            return Description;
        }

        public int CompareTo(EnumDescriptionAttribute valEnumDescription) {
            if (valEnumDescription == null) {
                return 1;
            }
            return Index.CompareTo(valEnumDescription.Index);
        }
        #endregion
    }
}
