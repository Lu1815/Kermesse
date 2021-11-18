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

    public partial class Gasto
    {
        [Display(Name = "Gasto")]
        public int idGasto { get; set; }
        [Display(Name = "Kermesse")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int kermesse { get; set; }
        [Display(Name = "Categoría de gasto")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int catGasto { get; set; }
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.DateTime)]
        public System.DateTime fechGasto { get; set; }
        [Display(Name = "Concepto")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(70, ErrorMessage = "El tamaño máximo es 70 caracteres")]
        public string concepto { get; set; }
        [Display(Name = "Monto")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(53, ErrorMessage = "El tamaño máximo es 53 caracteres")]
        public double monto { get; set; }
        public int usuarioCreacion { get; set; }
        public System.DateTime fechaCreacion { get; set; }
        public Nullable<int> usuarioModificacion { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        public Nullable<int> usuarioEliminacion { get; set; }
        public Nullable<System.DateTime> fechaEliminacion { get; set; }

        public virtual CategoriaGasto CategoriaGasto { get; set; }
        public virtual Kermesse Kermesse1 { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual Usuario Usuario2 { get; set; }
    }
}