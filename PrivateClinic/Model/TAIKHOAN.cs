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
    
    public partial class TAIKHOAN
    {
        public int TaiKhoanID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public Nullable<int> MaBS { get; set; }
        public string ChucVu { get; set; }
    
        public virtual BACSI BACSI { get; set; }
        public virtual CHUCVU CHUCVU1 { get; set; }
    }
}