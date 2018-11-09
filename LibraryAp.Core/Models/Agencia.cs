using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Lib;
using LibraryAp.MultiTenancy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAp.Models {
    public class Agencia :  Entity, IMustHaveTenant {        

        [Index("U_Agencia", 0, IsUnique = true)]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        [Required]
        [Display(Name = "Codigo")]
        [Varchar(5, ErrorMessage = "Longitud Maxima 5")]
        [Index("U_Agencia", 1, IsUnique = true)]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [Varchar(100, ErrorMessage = "Longitud Maxima 100")]
        public string Nombre { get; set; }
        
        [Display(Name = "Direccion")]
        [Varchar(250, ErrorMessage = "Longitud Maxima 250")]
        public string Direccion { get; set; }

        [Display(Name = "Telefono")]
        [Varchar(50, ErrorMessage = "Longitud Maxima 50")]
        public string Telefono { get; set; }

        public eStatus StatusRegistro { get; set; }

        public eComisionPor ComisionPor { get; set; }
        
        [DecimalPrecision(18, 3)]
        public decimal PorcentajePorPremio { get; set; }

        [Varchar(300, ErrorMessage = "Longitud Maxima 300")]
        public string Comentario { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }
        public Tenant Tenant { get; set; }       
    }
}
