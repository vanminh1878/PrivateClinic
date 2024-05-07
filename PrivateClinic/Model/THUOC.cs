//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class THUOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public THUOC()
        {
            this.BAOCAOSUDUNGTHUOCs = new HashSet<BAOCAOSUDUNGTHUOC>();
            this.CT_PKB = new HashSet<CT_PKB>();
            this.CT_PNT = new HashSet<CT_PNT>();
        }
    
        public int MaThuoc { get; set; }
        public int MaLoaiThuoc { get; set; }
        public string TenThuoc { get; set; }
        public int MaDVT { get; set; }
        public int MaCachDung { get; set; }
        public Nullable<double> DonGiaNhap { get; set; }
        public Nullable<double> DonGiaBan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAOCAOSUDUNGTHUOC> BAOCAOSUDUNGTHUOCs { get; set; }
        public virtual CACHDUNG CACHDUNG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PKB> CT_PKB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PNT> CT_PNT { get; set; }
        public virtual DVT DVT { get; set; }
        public virtual LOAITHUOC LOAITHUOC { get; set; }
    }
}