using LibraryAp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryAp.Web.Models {
    public class AgenciaViewModel {

        public int Id { get; set; }
        public int TenantId { get; set; }

        [Required]
        [Display(Name = "Codigo")]
        [StringLength(5)]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Direccion")]
        [StringLength(250)]
        public string Direccion { get; set; }

        [Display(Name = "Telefono")]
        [StringLength(50)]
        public string Telefono { get; set; }

        //public eStatus StatusRegistro { get; set; }

        public string StatusRegistroStr { get; set; }

        //public eComisionPor ComisionPor { get; set; }

        public string ComisionPorStr { get; set; }

        public decimal PorcentajePorPremio { get; set; }

        [StringLength(300)]
        public string Comentario { get; set; }

    }
}