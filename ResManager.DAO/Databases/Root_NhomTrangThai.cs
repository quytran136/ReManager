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
    
    public partial class Root_NhomTrangThai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Root_NhomTrangThai()
        {
            this.Root_QuanLyTrangThai = new HashSet<Root_QuanLyTrangThai>();
        }
    
        public int Id { get; set; }
        public string TenTrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Root_QuanLyTrangThai> Root_QuanLyTrangThai { get; set; }
    }
}
