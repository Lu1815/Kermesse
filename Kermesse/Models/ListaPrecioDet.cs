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
    
    public partial class ListaPrecioDet
    {
        public int idListaPrecioDet { get; set; }
        public int listaPrecio { get; set; }
        public int producto { get; set; }
        public double precioVenta { get; set; }
    
        public virtual ListaPrecio ListaPrecio1 { get; set; }
        public virtual Producto Producto1 { get; set; }
    }
}
