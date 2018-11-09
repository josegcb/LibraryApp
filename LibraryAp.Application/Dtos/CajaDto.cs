using LibraryAp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAp.Dtos {
    public class CajaInput {
        public int Id { get; set; }
    }

    public class CajaOutput  {
        public int Id { get; set; }
        public int AgenciaId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public eStatus StatusRegistro { get; set; }
        public string StatusRegistroStr { get; set; }
        public Byte[] TimeStamp { get; set; }
    }

    public class CreateCajaInput {       
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public eStatus StatusRegistro { get; set; }
        public Byte[] TimeStamp { get; set; }        

    }

    public class DeleteCajaInput {
        public int Id { get; set; }
        public Byte[] TimeStamp { get; set; }
    }

    public class UpdateCajaInput {
        public int Id { get; set; }
        public int AgenciaId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public eStatus StatusRegistro { get; set; }
        public Byte[] TimeStamp { get; set; }
    }
}
