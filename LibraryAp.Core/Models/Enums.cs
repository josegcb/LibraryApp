using Lib;

namespace LibraryAp.Models {
    public enum  eStatus {
        [EnumDescription("Activo")]
        Activo = 0,

        [EnumDescription("Inactivo")]
        Inactivo
    }

    public enum eComisionPor {
        [EnumDescription("Por Monto")]
        Monto = 0,

        [EnumDescription("Por Utilidad")]
        Utilidad
    }
}