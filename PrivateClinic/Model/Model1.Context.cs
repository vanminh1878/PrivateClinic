﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrivateClinic.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QUANLYPHONGMACHTUEntities1 : DbContext
    {
        public QUANLYPHONGMACHTUEntities1()
            : base("name=QUANLYPHONGMACHTUEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BACSI> BACSIs { get; set; }
        public virtual DbSet<BAOCAODOANHTHU> BAOCAODOANHTHUs { get; set; }
        public virtual DbSet<BAOCAOSUDUNGTHUOC> BAOCAOSUDUNGTHUOCs { get; set; }
        public virtual DbSet<BENHNHAN> BENHNHANs { get; set; }
        public virtual DbSet<CACHDUNG> CACHDUNGs { get; set; }
        public virtual DbSet<CHUCNANG> CHUCNANGs { get; set; }
        public virtual DbSet<CT_BCDT> CT_BCDT { get; set; }
        public virtual DbSet<CT_PKB> CT_PKB { get; set; }
        public virtual DbSet<CT_PNT> CT_PNT { get; set; }
        public virtual DbSet<DVT> DVTs { get; set; }
        public virtual DbSet<HOADON> HOADONs { get; set; }
        public virtual DbSet<LOAIBENH> LOAIBENHs { get; set; }
        public virtual DbSet<LOAITHUOC> LOAITHUOCs { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual DbSet<NHOMNGUOIDUNG> NHOMNGUOIDUNGs { get; set; }
        public virtual DbSet<PHIEUKHAMBENH> PHIEUKHAMBENHs { get; set; }
        public virtual DbSet<PHIEUNHAPTHUOC> PHIEUNHAPTHUOCs { get; set; }
        public virtual DbSet<THAMSO> THAMSOes { get; set; }
        public virtual DbSet<THUOC> THUOCs { get; set; }
        public virtual DbSet<PHANQUYEN> PHANQUYENs { get; set; }
    }
}
