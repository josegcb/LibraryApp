using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LibraryAp.Models;

namespace Lib {
    public class FilterHelper {
        public static IList<object> ListProperties<T>() {
            IList<object> vResult = new List<object>();
            T valInstace = Activator.CreateInstance<T>();
            PropertyInfo[] vProperties = valInstace.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var vProperty in vProperties) {
                var vAttributes = (SearchFieldAttribute[])vProperty.GetCustomAttributes(typeof(SearchFieldAttribute), false);
                string NamePropertyType = vProperty.PropertyType.Name;
                object Descripciones = null;
                if (vAttributes.Length > 0) {
                    if (vProperty.PropertyType.IsEnum) {
                        NamePropertyType = "Enum";
                        Descripciones = ListValoresEnum(vProperty.PropertyType);
                    }
                    foreach (SearchFieldAttribute vAttribute in vAttributes) {
                        vResult.Add(new {
                            Nombre = vProperty.Name,
                            Operaciones = ListValores(vProperty.PropertyType),
                            Tipo = NamePropertyType,
                            Valores = Descripciones
                        });
                    }
                }
            }
            return vResult;
        }

        private static object ListValoresEnum(Type propertyType) {
            List<object> vResult = new List<object>();            
            foreach (Enum item in Enum.GetValues(propertyType)) {
                vResult.Add(new {
                    Value = item,
                    Descripcion = item.GetDescription()
                });
            }
            return vResult;
        }

        private static object ListValores(Type valPropertyType) {
            FilterStatement<string> vFilterStatement = new FilterStatement<string>();
            List<eBooleanOperatorType> vList = vFilterStatement.SupportedOperations(valPropertyType);
            List<object> vResult = new List<object>();
            foreach (var item in vList) {
                vResult.Add(new {
                    Descripcion = item.GetDescription(),
                    Value = item,
                    NumeroDeParametros = vFilterStatement.NumberOfValuesAcceptable(item)
                });
            }
            return vResult;
        }

        public static Filter<T> CreateFilters<T>(List<EntityFilter> valFilter) where T : class {
            var filter = new Filter<T>();
            T valInstace = Activator.CreateInstance<T>();
            PropertyInfo[] vProperties = valInstace.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (EntityFilter item in valFilter) {
                var vProperty = vProperties.FirstOrDefault(p => p.Name.Equals(item.PropertyName, StringComparison.CurrentCultureIgnoreCase));
                eBooleanOperatorType vBooleanOperatorType = (eBooleanOperatorType)Enum.Parse(typeof(eBooleanOperatorType), item.BooleanOperator);
                eLogicOperatorType vLogicOperatorType = (eLogicOperatorType)Enum.Parse(typeof(eLogicOperatorType), item.LogicOperator);
                object vValue1 = item.Value1; 
                object vValue2 = item.Value2;                
                GetValue(vProperty, item, vBooleanOperatorType, ref vValue1, ref vValue2);
                filter.Add(item.PropertyName, vBooleanOperatorType, vLogicOperatorType, vValue1, vValue2);
            }
            return filter;
        }

        private static void GetValue(PropertyInfo valProperty, EntityFilter valItem, eBooleanOperatorType valBooleanOperatorType, ref object valValue1, ref object valValue2) {
            var vType = Nullable.GetUnderlyingType(valProperty.PropertyType) ?? valProperty.PropertyType;
            int vNumberOfValuesAcceptable = new FilterStatement<string>().NumberOfValuesAcceptable(valBooleanOperatorType);
            if (vNumberOfValuesAcceptable > 0) {
                if (vType == typeof(DateTime)) {
                    valValue1 = Convert.ToDateTime(valValue1);
                } else if (vType == typeof(int)) {
                    valValue1 = Convert.ToInt32(valValue1);
                } else if (vType == typeof(decimal)) {
                    valValue1 = Convert.ToDecimal(valValue1);
                } else if (vType == typeof(bool)) {
                    valValue1 = Convert.ToBoolean(valItem.Value3);
                } else if (vType.IsEnum) {
                    valValue1 = Enum.ToObject(vType, Convert.ToInt32(valItem.Value3));
                }
            } else {
                valValue1 = null;
            }
            if (vNumberOfValuesAcceptable == 2) {
                if (vType == typeof(DateTime)) {
                    valValue2 = Convert.ToDateTime(valValue2);
                } else if (vType == typeof(int)) {
                    valValue2 = Convert.ToInt32(valValue2);
                } else if (vType == typeof(decimal)) {
                    valValue2 = Convert.ToDecimal(valValue2);
                }
            } else {
                valValue2 = null;
            }
        }

    }    
}
