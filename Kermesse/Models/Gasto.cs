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
    
    public partial class Gasto
    {
        public int idGasto { get; set; }
        public int kermesse { get; set; }
        public int catGasto { get; set; }
        public System.DateTime fechGasto { get; set; }
        public string concepto { get; set; }
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
