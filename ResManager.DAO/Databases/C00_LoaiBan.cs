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
    
    public partial class C00_LoaiBan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C00_LoaiBan()
        {
            this.C00_BanAn = new HashSet<C00_BanAn>();
        }
    
        public int Id { get; set; }
        public string TenLoaiBan { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<int> IdTrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C00_BanAn> C00_BanAn { get; set; }
        public virtual Root_QuanLyTrangThai Root_QuanLyTrangThai { get; set; }
    }
}
