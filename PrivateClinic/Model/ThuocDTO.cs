using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinic.Model
{
    public class ThuocDTO
    {
        public int STT {  get; set; }
        public int MaThuoc {  get; set; }
        public string TenThuoc { get; set; }    
        public string DVT {  get; set; }
        public int? SL {  get; set; }
        public double? Gia {  get; set; }
    }
}
