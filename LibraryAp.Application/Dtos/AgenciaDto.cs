using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Lib;
using LibraryAp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAp.Dtos {
    [AutoMapFrom(typeof(Agencia))]
    public class AgenciaBase : EntityDto {
        [SearchFieldAttribute]
        new public int Id { get; set; }
        public int TenantId { get; set; }

        [Required]
        [Display(Name = "Codigo")]
        [Varchar(5, ErrorMessage = "Longitud Maxima 5")]
        [Key]
        [SearchFieldAttribute]
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
        [SearchFieldAttribute]
        public eStatus StatusRegistro { get; set; }

        public eComisionPor ComisionPor { get; set; }

        [DecimalPrecision(18, 3)]
        [SearchFieldAttribute]
        public decimal PorcentajePorPremio { get; set; }

        [Varchar(300, ErrorMessage = "Longitud Maxima 300")]
        public string Comentario { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }        
    }
    public class AgenciaInput {
        public int Id { get; set; }
    }

    public class AgenciaOutput : AgenciaBase {
        public string StatusRegistroStr {
            get {
                return EnumHelper.GetDescription(StatusRegistro);
            }
        }

        public string ComisionPorStr {
            get {
                return EnumHelper.GetDescription(ComisionPor);
            }
        }

    }

    public class AgenciaCreateInput : AgenciaBase {
        
        
        
    }

    public class AgenciaDeleteInput {
        public int Id { get; set; }
        public Byte[] TimeStamp { get; set; }
    }

    public class AgenciaUpdateInput : AgenciaBase {
        
        
    
    }
}
