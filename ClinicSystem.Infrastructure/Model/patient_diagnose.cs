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
    
    public partial class PATIENT_DIAGNOSE
    {
        public long ID { get; set; }
        public string DIAGNOSE { get; set; }
        public long PATIENT_VISIT_ID { get; set; }
        public long DISEASE_ID { get; set; }
    
        public virtual DISEASE DISEASE { get; set; }
        public virtual PATIENT_VISIT PATIENT_VISIT { get; set; }
    }
}
