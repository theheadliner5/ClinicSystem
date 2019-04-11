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
    
    public partial class UNIT_PLAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UNIT_PLAN()
        {
            this.DIAGNOSTICS = new HashSet<DIAGNOSTICS>();
            this.EMPLOYEE_COST = new HashSet<EMPLOYEE_COST>();
        }
    
        public long ID { get; set; }
        public string BUDGET_TYPE { get; set; }
        public System.DateTime DATE_FROM { get; set; }
        public System.DateTime DATE_TO { get; set; }
        public decimal VALUE { get; set; }
        public long UNIT_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIAGNOSTICS> DIAGNOSTICS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE_COST> EMPLOYEE_COST { get; set; }
        public virtual UNIT UNIT { get; set; }
    }
}
