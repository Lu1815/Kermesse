//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kermesse.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Denominacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Denominacion()
        {
            this.ArqueoCajaDets = new HashSet<ArqueoCajaDet>();
        }
    
        public int idDenominacion { get; set; }
        public int moneda { get; set; }
        public decimal valor { get; set; }
        public string valorLetras { get; set; }
        public int estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArqueoCajaDet> ArqueoCajaDets { get; set; }
        public virtual Moneda Moneda1 { get; set; }
    }
}
