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
    
    public partial class NGUOIDUNG
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string MaNhom { get; set; }
    
        public virtual NHOMNGUOIDUNG NHOMNGUOIDUNG { get; set; }
    }
}
