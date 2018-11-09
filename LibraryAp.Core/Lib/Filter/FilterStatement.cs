using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    public class FilterStatement<TPropertyType> : IFilterStatement {
        readonly Dictionary<TypeGroup, HashSet<Type>> TypeGroups;
        public eLogicOperatorType LogicOperator { get; set; }
        public string PropertyName { get; set; }
        public eBooleanOperatorType BooleanOperator { get; set; }
        public object Value { get; set; }

        public object Value2 { get; set; }

        public FilterStatement() {
            TypeGroups = new Dictionary<TypeGroup, HashSet<Type>>
            {
                { TypeGroup.Text, new HashSet<Type> { typeof(string), typeof(char) } },
                { TypeGroup.Number, new HashSet<Type> { typeof(int), typeof(uint), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(long), typeof(ulong), typeof(Single), typeof(double), typeof(decimal) } },
                { TypeGroup.Boolean, new HashSet<Type> { typeof(bool) } },
                { TypeGroup.Date, new HashSet<Type> { typeof(DateTime) } },                
                { TypeGroup.Nullable, new HashSet<Type> { typeof(Nullable<>) } }
            };
        }
        public FilterStatement(string propertyId, eBooleanOperatorType operation, TPropertyType value, TPropertyType value2 = default(TPropertyType), eLogicOperatorType connector = eLogicOperatorType.And) {
            PropertyName = propertyId;
            LogicOperator = connector;
            BooleanOperator = operation;
            if (typeof(TPropertyType).IsArray) {
                if (operation != eBooleanOperatorType.Contains && operation != eBooleanOperatorType.In) throw new ArgumentException("Only 'Operacao.Contains' and 'Operacao.In' support arrays as parameters.");

                var listType = typeof(List<>);
                var constructedListType = listType.MakeGenericType(typeof(TPropertyType).GetElementType());
                Value = value != null ? Activator.CreateInstance(constructedListType, value) : null;
                Value2 = value2 != null ? Activator.CreateInstance(constructedListType, value2) : null;
            } else {
                Value = value;
                Value2 = value2;
            }

            TypeGroups = new Dictionary<TypeGroup, HashSet<Type>>
            {
                { TypeGroup.Text, new HashSet<Type> { typeof(string), typeof(char) } },
                { TypeGroup.Number, new HashSet<Type> { typeof(int), typeof(uint), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(long), typeof(ulong), typeof(Single), typeof(double), typeof(decimal) } },
                { TypeGroup.Boolean, new HashSet<Type> { typeof(bool) } },
                { TypeGroup.Date, new HashSet<Type> { typeof(DateTime) } },                
                { TypeGroup.Nullable, new HashSet<Type> { typeof(Nullable<>) } }
            };

            Validate();
        }

        public FilterStatement(string propertyId, eBooleanOperatorType operation, Enum value, Enum value2 = default(Enum), eLogicOperatorType connector = eLogicOperatorType.And) {
            PropertyName = propertyId;
            LogicOperator = connector;
            BooleanOperator = operation;
            Value = value;
            Value2 = value2;


            TypeGroups = new Dictionary<TypeGroup, HashSet<Type>>
            {
                { TypeGroup.Text, new HashSet<Type> { typeof(string), typeof(char) } },
                { TypeGroup.Number, new HashSet<Type> { typeof(int), typeof(uint), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(long), typeof(ulong), typeof(Single), typeof(double), typeof(decimal) } },
                { TypeGroup.Boolean, new HashSet<Type> { typeof(bool) } },
                { TypeGroup.Date, new HashSet<Type> { typeof(DateTime) } },
                { TypeGroup.Nullable, new HashSet<Type> { typeof(Nullable<>) } }
            };

            Validate();
        }



        public void Validate() {
            ValidateNumberOfValues();
            ValidateSupportedOperations();
        }

        public void Validate(Type valTypeEnum) {
            ValidateNumberOfValues(valTypeEnum);
            ValidateSupportedOperations();
        }

        private void ValidateNumberOfValues() {
            var vNumberOfValues = NumberOfValuesAcceptable(BooleanOperator);            
            var vFailsForSingleValue = vNumberOfValues == 1 && Value2 != null && !Value2.Equals(default(TPropertyType));
            var vFailsForNoValueAtAll = vNumberOfValues == 0 && Value != null && Value2 != null && (!Value.Equals(default(TPropertyType)) || !Value2.Equals(default(TPropertyType)));
            if (vFailsForSingleValue || vFailsForNoValueAtAll) {
                throw new Exception(string.Format("La operacion '{0}' admite '{1}' valores.", BooleanOperator, vNumberOfValues));
            }
        }

        private void ValidateNumberOfValues(Type valTypeEnum) {
            var vNumberOfValues = NumberOfValuesAcceptable(BooleanOperator);
            var vFailsForSingleValue = vNumberOfValues == 1 && Value2 != null && !Value2.Equals(default(Enum));
            var vFailsForNoValueAtAll = vNumberOfValues == 0 && Value != null && Value2 != null && (!Value.Equals(default(Enum)) || !Value2.Equals(default(Enum)));
            if (vFailsForSingleValue || vFailsForNoValueAtAll) {
                throw new Exception(string.Format("La operacion '{0}' admite '{1}' valores.", BooleanOperator, vNumberOfValues));
            }
        }

        private void ValidateSupportedOperations() {
            List<eBooleanOperatorType> vSupportedOperations = null;
            if (typeof(TPropertyType) == typeof(object)) {
                return;
            }
            vSupportedOperations = SupportedOperations(typeof(TPropertyType));
            if (!vSupportedOperations.Contains(BooleanOperator)) {
                throw new Exception(string.Format("El Tipo '{0}' no sporta la operacion '{1}'.", typeof(TPropertyType).Name, BooleanOperator));
            }
        }

        private void ValidateSupportedOperations(Type valTypeEnum) {
            List<eBooleanOperatorType> vSupportedOperations = null;            
            vSupportedOperations = SupportedOperations(typeof(Enum));
            if (!vSupportedOperations.Contains(BooleanOperator)) {
                throw new Exception(string.Format("El Tipo '{0}' no sporta la operacion '{1}'.", typeof(Enum).Name, BooleanOperator));
            }
        }

        public List<eBooleanOperatorType> SupportedOperations(Type valType) {
            var vSupportedOperations = ExtractSupportedOperationsFromAttribute(valType);
            if (valType.IsArray) {
                vSupportedOperations.Add(eBooleanOperatorType.In);
            }

            var underlyingNullableType = Nullable.GetUnderlyingType(valType);
            if (underlyingNullableType != null) {
                var underlyingNullableTypeOperations = SupportedOperations(underlyingNullableType);
                vSupportedOperations.AddRange(underlyingNullableTypeOperations);
            }

            return vSupportedOperations;
        }
        private void GetCustomSupportedTypes() {
            var configSection = ConfigurationManager.GetSection(ExpressionBuilderConfig.SectionName) as ExpressionBuilderConfig;
            if (configSection == null) {
                return;
            }
            foreach (ExpressionBuilderConfig.SupportedTypeElement supportedType in configSection.SupportedTypes) {
                Type type = Type.GetType(supportedType.Type, false, true);
                if (type != null) {
                    TypeGroups[supportedType.TypeGroup].Add(type);
                }
            }
        }

        public List<eBooleanOperatorType> ExtractSupportedOperationsFromAttribute(Type type) {
            var typeName = type.Name;
            if (type.IsArray) {
                typeName = type.GetElementType().Name;
            }           
            GetCustomSupportedTypes();
            var typeGroup = TypeGroups.FirstOrDefault(i => i.Value.Any(v => v.Name == typeName)).Key;
            var fieldInfo = typeGroup.GetType().GetField(typeGroup.ToString());
            var attrs = fieldInfo.GetCustomAttributes(false);
            var attr = attrs.FirstOrDefault(a => a is SupportedOperationsAttribute);
            return (attr as SupportedOperationsAttribute).SupportedOperations;
        }


        public int NumberOfValuesAcceptable(eBooleanOperatorType operation) {
            var fieldInfo = operation.GetType().GetField(operation.ToString());
            var attrs = fieldInfo.GetCustomAttributes(false);
            var attr = attrs.FirstOrDefault(a => a is NumberOfValuesAttribute);
            return (attr as NumberOfValuesAttribute).NumberOfValues;
        }

    }
}
