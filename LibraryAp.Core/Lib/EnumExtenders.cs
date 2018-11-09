using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    public static class EnumExtenders {

        public static string GetDescription(this Enum valValue) {
            return GetDescription(valValue, 0);
        }

        public static string GetDescription(this Enum valValue, int valIndex) {
            string vResult = string.Empty;

            FieldInfo vField = valValue.GetType().GetField(valValue.ToString());
            if (vField != null) {
                EnumDescriptionAttribute[] vAttributes = (EnumDescriptionAttribute[])vField.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                Array.Sort(vAttributes);
                if (vAttributes.Length > 0) {
                    bool vEncontrado = false;
                    foreach (EnumDescriptionAttribute vAttribute in vAttributes) {
                        if (vAttribute.Index == valIndex) {
                            vResult = vAttribute.Description;
                            vEncontrado = true;
                            break;
                        }
                    }
                    if (!vEncontrado) {
                        vResult = vAttributes[0].Description;
                    }
                } else {
                    vResult = valValue.ToString();
                }
            }
            return vResult;
        }

        public static List<string> GetDescriptions(this Enum valValue) {
            List<string> vDescriptions = new List<string>();
            FieldInfo vField = valValue.GetType().GetField(valValue.ToString());
            if (vField != null) {
                EnumDescriptionAttribute[] vAttributes = (EnumDescriptionAttribute[])vField.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                Array.Sort(vAttributes);
                if (vAttributes.Length > 0) {
                    foreach (EnumDescriptionAttribute vAttribute in vAttributes) {
                        vDescriptions.Add(vAttribute.Description);
                    }
                }
            }

            return vDescriptions;
        }

        public static bool IsDefined(this Enum valValue) {
            return Enum.IsDefined(valValue.GetType(), valValue);
        }
    }

}
