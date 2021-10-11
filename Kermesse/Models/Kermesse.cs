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

    public partial class Kermesse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kermesse()
        {
            this.ArqueoCajas = new HashSet<ArqueoCaja>();
            this.Gastoes = new HashSet<Gasto>();
            this.IngresoComunidads = new HashSet<IngresoComunidad>();
            this.ListaPrecios = new HashSet<ListaPrecio>();
        }

        [Display(Name = "C�digo")]
        public int idKermesse { get; set; }
        [Display(Name = "C�digo de parroquia")]
        public int parroquia { get; set; }
        [Display(Name = "Nombre de Kermesse")]
        public string nombre { get; set; }
        [Display(Name = "Fecha de inicio")]
        public System.DateTime fInicio { get; set; }
        [Display(Name = "Fecha de finalizaci�n")]
        public System.DateTime fFinal { get; set; }
        [Display(Name = "Descripci�n")]
        public string descripcion { get; set; }
        [Display(Name = "Estado")]
        public int estado { get; set; }
        [Display(Name = "Usuario creador")]
        public int usuarioCreacion { get; set; }
        [Display(Name = "Fecha de creaci�n")]
        public System.DateTime fechaCreacion { get; set; }
        [Display(Name = "Usuario que modific�")]
        public Nullable<int> usuarioModificacion { get; set; }
        [Display(Name = "Fecha de modifici�n")]
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        [Display(Name = "Usuario que elimin�")]
        public Nullable<int> usuarioEliminacion { get; set; }
        [Display(Name = "Fecha de eliminaci�n")]
        public Nullable<System.DateTime> fechaEliminacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArqueoCaja> ArqueoCajas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gasto> Gastoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngresoComunidad> IngresoComunidads { get; set; }
        public virtual Parroquia Parroquia1 { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual Usuario Usuario2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListaPrecio> ListaPrecios { get; set; }
    }
}
