//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClinicSystem.Infrastructure.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UNIT_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UNIT_TYPE()
        {
            this.UNIT = new HashSet<UNIT>();
        }
    
        public long ID { get; set; }
        public System.DateTime LAST_MOD_DATE { get; set; }
        public string UNIT_NAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UNIT> UNIT { get; set; }
    }
}
