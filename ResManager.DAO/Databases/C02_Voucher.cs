//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ResManager.DAO.Databases
{
    using System;
    using System.Collections.Generic;
    
    public partial class C02_Voucher
    {
        public int Id { get; set; }
        public string MaGiamGia { get; set; }
        public Nullable<System.DateTime> NgayHieuLuc { get; set; }
        public Nullable<System.DateTime> NgayHetHan { get; set; }
        public Nullable<int> SoLuongKichHoat { get; set; }
        public Nullable<int> SoLuongToiDa { get; set; }
        public Nullable<decimal> KhauTru { get; set; }
        public Nullable<decimal> PhanTramKhauTru { get; set; }
    }
}
