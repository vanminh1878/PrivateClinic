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
    
    public partial class CT_PNT
    {
        public int SoPhieuNhap { get; set; }
        public Nullable<int> MaThuoc { get; set; }
        public Nullable<int> SoLuongNhap { get; set; }
        public Nullable<double> DonGiaNhap { get; set; }
        public Nullable<double> ThanhTien { get; set; }
    
        public virtual PHIEUNHAPTHUOC PHIEUNHAPTHUOC { get; set; }
        public virtual THUOC THUOC { get; set; }
    }
}
