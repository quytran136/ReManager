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
    
    public partial class C01_NhomMon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C01_NhomMon()
        {
            this.C01_SapXepNhom = new HashSet<C01_SapXepNhom>();
        }
    
        public int Id { get; set; }
        public string TenNhomMon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C01_SapXepNhom> C01_SapXepNhom { get; set; }
    }
}
