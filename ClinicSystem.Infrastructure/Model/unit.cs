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
    
    public partial class UNIT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UNIT()
        {
            this.EMPLOYEE = new HashSet<EMPLOYEE>();
            this.PATIENT_VISIT = new HashSet<PATIENT_VISIT>();
            this.UNIT_PLAN = new HashSet<UNIT_PLAN>();
            this.UNIT1 = new HashSet<UNIT>();
        }
    
        public long ID { get; set; }
        public System.DateTime LAST_MOD_DATE { get; set; }
        public long CLINIC_ID { get; set; }
        public long UNIT_TYPE_ID { get; set; }
        public Nullable<long> UNIT_ID { get; set; }
    
        public virtual CLINIC CLINIC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE> EMPLOYEE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PATIENT_VISIT> PATIENT_VISIT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UNIT_PLAN> UNIT_PLAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UNIT> UNIT1 { get; set; }
        public virtual UNIT UNIT2 { get; set; }
        public virtual UNIT_TYPE UNIT_TYPE { get; set; }
    }
}
