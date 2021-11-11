//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kermesse.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TasaCambioDet
    {

        [Display(Name = "Tasa de Cambio")]
        public int idTasaCambioDet { get; set; }
        [Display(Name = "Tasa de Cambio")]

        public int tasaCambio { get; set; }
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.DateTime)]
        public System.DateTime fecha { get; set; }
        [Display(Name = "Tipo de Cambio")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public decimal tipoCambio { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int estado { get; set; }
        [Display(Name = "Mes")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public virtual TasaCambio TasaCambio1 { get; set; }
    }
}