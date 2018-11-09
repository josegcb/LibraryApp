using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib {
    public enum eLogicOperatorType {
        And,
        Or
    }
    public enum eBooleanOperatorType {
        [NumberOfValues(1)]
        [EnumDescription("Igual a")]
        EqualTo,
        [NumberOfValues(1)]
        [EnumDescription("Contiene")]
        Contains,
        [EnumDescription("Inicia con")]
        [NumberOfValues(1)]
        StartsWith,
        [NumberOfValues(1)]
        [EnumDescription("Termina con")]
        EndsWith,
        [NumberOfValues(1)]
        [EnumDescription("No es igual a")]
        NotEqualTo,
        [NumberOfValues(1)]
        [EnumDescription("Mayor que")]
        GreaterThan,
        [NumberOfValues(1)]
        [EnumDescription("Mayor o Igual que")]
        GreaterThanOrEqualTo,
        [NumberOfValues(1)]
        [EnumDescription("Menor que")]
        LessThan,
        [NumberOfValues(1)]
        [EnumDescription("Menor o Igual que")]
        LessThanOrEqualTo,
        [NumberOfValues(2)]
        [EnumDescription("Entre")]
        Between,
        [NumberOfValues(0)]
        [EnumDescription("Es Null")]
        IsNull,
        [NumberOfValues(0)]
        [EnumDescription("Esta vacio")]
        IsEmpty,
        [NumberOfValues(0)]
        [EnumDescription("Es null o espacio en blanco")]
        IsNullOrWhiteSpace,
        [NumberOfValues(0)]
        [EnumDescription("No es Null")]
        IsNotNull,
        [NumberOfValues(0)]
        [EnumDescription("No esta vacio")]
        IsNotEmpty,
        [NumberOfValues(0)]
        [EnumDescription("No esta vacio o no es Espacio en Blanco")]
        IsNotNullNorWhiteSpace,
        [NumberOfValues(1)]
        [EnumDescription("En el conjunto")]
        In
    }
    public enum TypeGroup {

        [SupportedOperations(eBooleanOperatorType.EqualTo, eBooleanOperatorType.NotEqualTo)]
        Default,
        [SupportedOperations(eBooleanOperatorType.Contains, eBooleanOperatorType.EndsWith, eBooleanOperatorType.EqualTo,
                             eBooleanOperatorType.IsEmpty, eBooleanOperatorType.IsNotEmpty, eBooleanOperatorType.IsNotNull, eBooleanOperatorType.IsNotNullNorWhiteSpace,
                             eBooleanOperatorType.IsNull, eBooleanOperatorType.IsNullOrWhiteSpace, eBooleanOperatorType.NotEqualTo, eBooleanOperatorType.StartsWith)]
        Text,
        [SupportedOperations(eBooleanOperatorType.Between, eBooleanOperatorType.EqualTo, eBooleanOperatorType.GreaterThan, eBooleanOperatorType.GreaterThanOrEqualTo,
                             eBooleanOperatorType.LessThan, eBooleanOperatorType.LessThanOrEqualTo, eBooleanOperatorType.NotEqualTo)]
        Number,
        [SupportedOperations(eBooleanOperatorType.EqualTo, eBooleanOperatorType.NotEqualTo)]
        Boolean,
        [SupportedOperations(eBooleanOperatorType.Between, eBooleanOperatorType.EqualTo, eBooleanOperatorType.GreaterThan, eBooleanOperatorType.GreaterThanOrEqualTo,
                             eBooleanOperatorType.LessThan, eBooleanOperatorType.LessThanOrEqualTo, eBooleanOperatorType.NotEqualTo)]
        Date,
        [SupportedOperations(eBooleanOperatorType.IsNotNull, eBooleanOperatorType.IsNull)]
        Nullable
    }
}
