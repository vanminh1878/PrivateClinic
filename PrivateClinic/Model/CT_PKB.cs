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
    
    public partial class CT_PKB
    {
        public int MaPKB { get; set; }
        public int MaThuoc { get; set; }
        public Nullable<int> SoLuong { get; set; }
    
        public virtual THUOC THUOC { get; set; }
    }
}
