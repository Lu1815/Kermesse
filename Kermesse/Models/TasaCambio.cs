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
    
    public partial class TasaCambio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TasaCambio()
        {
            this.TasaCambioDets = new HashSet<TasaCambioDet>();
        }
    
        public int idTasaCambio { get; set; }
        public int monedaO { get; set; }
        public int monedaC { get; set; }
        public string mes { get; set; }
        public int anio { get; set; }
        public int estado { get; set; }
    
        public virtual Moneda Moneda { get; set; }
        public virtual Moneda Moneda1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TasaCambioDet> TasaCambioDets { get; set; }
    }
}