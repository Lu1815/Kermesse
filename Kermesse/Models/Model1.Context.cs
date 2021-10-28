﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDKermesseEntities : DbContext
    {
        public BDKermesseEntities()
            : base("name=BDKermesseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ArqueoCaja> ArqueoCajas { get; set; }
        public virtual DbSet<ArqueoCajaDet> ArqueoCajaDets { get; set; }
        public virtual DbSet<CategoriaGasto> CategoriaGastoes { get; set; }
        public virtual DbSet<CategoriaProducto> CategoriaProductoes { get; set; }
        public virtual DbSet<Comunidad> Comunidads { get; set; }
        public virtual DbSet<ControlBono> ControlBonoes { get; set; }
        public virtual DbSet<Denominacion> Denominacions { get; set; }
        public virtual DbSet<Gasto> Gastoes { get; set; }
        public virtual DbSet<IngresoComunidad> IngresoComunidads { get; set; }
        public virtual DbSet<IngresoComunidadDet> IngresoComunidadDets { get; set; }
        public virtual DbSet<Kermesse> Kermesses { get; set; }
        public virtual DbSet<ListaPrecio> ListaPrecios { get; set; }
        public virtual DbSet<ListaPrecioDet> ListaPrecioDets { get; set; }
        public virtual DbSet<Moneda> Monedas { get; set; }
        public virtual DbSet<Opcion> Opcions { get; set; }
        public virtual DbSet<Parroquia> Parroquias { get; set; }
        public virtual DbSet<Producto> Productoes { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<RolOpcion> RolOpcions { get; set; }
        public virtual DbSet<RolUsuario> RolUsuarios { get; set; }
        public virtual DbSet<TasaCambio> TasaCambios { get; set; }
        public virtual DbSet<TasaCambioDet> TasaCambioDets { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<VwListaPrecio> VwListaPrecios { get; set; }
    }
}
