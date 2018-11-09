using Abp.Domain.Entities.Auditing;
using Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace LibraryAp.Models {
    public class Caja : Entity , IMustHaveAgency {
        [ForeignKey("Agencia")]
        [Index("U_Caja", 0, IsUnique = true)]
        public int AgenciaId { get; set; }

        [Required]
        [Display(Name = "Codigo")]
        [Varchar(5, ErrorMessage = "Longitud Maxima 5")]
        [Index("U_Caja", 1, IsUnique = true)]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        [Varchar(100, ErrorMessage = "Longitud Maxima 100")]
        public string Descripcion { get; set; }

        public eStatus StatusRegistro { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }

        public Agencia Agencia { get; set; }


    }
}
