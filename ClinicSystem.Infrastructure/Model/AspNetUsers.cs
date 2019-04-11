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
    
    public partial class ASPNETUSERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASPNETUSERS()
        {
            this.ASPNETUSERCLAIMS = new HashSet<ASPNETUSERCLAIMS>();
            this.ASPNETUSERLOGINS = new HashSet<ASPNETUSERLOGINS>();
            this.PERSON = new HashSet<PERSON>();
            this.ASPNETROLES = new HashSet<ASPNETROLES>();
        }
    
        public string ID { get; set; }
        public string EMAIL { get; set; }
        public bool EMAILCONFIRMED { get; set; }
        public string PASSWORDHASH { get; set; }
        public string SECURITYSTAMP { get; set; }
        public string PHONENUMBER { get; set; }
        public bool PHONENUMBERCONFIRMED { get; set; }
        public bool TWOFACTORENABLED { get; set; }
        public Nullable<System.DateTime> LOCKOUTENDDATEUTC { get; set; }
        public bool LOCKOUTENABLED { get; set; }
        public int ACCESSFAILEDCOUNT { get; set; }
        public string USERNAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASPNETUSERCLAIMS> ASPNETUSERCLAIMS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASPNETUSERLOGINS> ASPNETUSERLOGINS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSON> PERSON { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASPNETROLES> ASPNETROLES { get; set; }
    }
}
