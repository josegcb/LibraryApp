using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib {   
    public class ModelBuilderHelper {
        public static void StringConfiguration(DbModelBuilder valModelBuilder) {
            valModelBuilder.Properties<string>().Configure(c => ConfigureVarchar(c));
        }

        private static void ConfigureVarchar(ConventionPrimitivePropertyConfiguration valConventionPrimitivePropertyConfiguration) {
            var vAttributes = valConventionPrimitivePropertyConfiguration.ClrPropertyInfo.GetCustomAttributes(typeof(VarcharAttribute), false);
            if (vAttributes.Length > 0) {
                valConventionPrimitivePropertyConfiguration.HasColumnType("varchar");
            }

        }

        public static void RowVersionConfiguration(DbModelBuilder valModelBuilder) {
            valModelBuilder.Properties<Byte[]>().Configure(c => ConfigureTimestamp(c));
        }

        private static void ConfigureTimestamp(ConventionPrimitivePropertyConfiguration valConventionPrimitivePropertyConfiguration) {
            var vAttributes = valConventionPrimitivePropertyConfiguration.ClrPropertyInfo.GetCustomAttributes(typeof(TimestampAttribute), false);
            if (vAttributes.Length > 0 && valConventionPrimitivePropertyConfiguration.ClrPropertyInfo.Name.StartsWith("Timestamp", StringComparison.OrdinalIgnoreCase)) {
                valConventionPrimitivePropertyConfiguration.IsRowVersion();
            }
        }

        public static void DecimalPrecisionConfiguration(DbModelBuilder valModelBuilder) {
            valModelBuilder.Properties<decimal>().Configure(c => ConfigurePrecision(c));
        }

        private static void ConfigurePrecision(ConventionPrimitivePropertyConfiguration valConventionPrimitivePropertyConfiguration) {
            byte vPrecision = 18;
            byte vScale = 2;
            var vAttributes = valConventionPrimitivePropertyConfiguration.ClrPropertyInfo.GetCustomAttributes(typeof(DecimalPrecisionAttribute), false);
            if (vAttributes.Length > 0) {
                foreach (DecimalPrecisionAttribute vAttribute in vAttributes) {
                    vPrecision = vAttribute.Precision;
                    vScale = vAttribute.Scale;
                }
            }
            valConventionPrimitivePropertyConfiguration.HasPrecision(vPrecision, vScale);
        }
    }
}
