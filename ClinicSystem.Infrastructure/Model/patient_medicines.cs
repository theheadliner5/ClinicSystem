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
    
    public partial class PATIENT_MEDICINES
    {
        public long ID { get; set; }
        public System.DateTime LAST_MOD_DATE { get; set; }
        public string DOSE { get; set; }
        public System.DateTime TREATMENT_DATE { get; set; }
        public long PATIENT_VISIT_ID { get; set; }
        public long MEDICINE_ORDER_ID { get; set; }
    
        public virtual MEDICINE_ORDER MEDICINE_ORDER { get; set; }
        public virtual PATIENT_VISIT PATIENT_VISIT { get; set; }
    }
}
