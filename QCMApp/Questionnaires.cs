//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QCMApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Questionnaires
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Questionnaires()
        {
            this.Elements = new HashSet<Elements>();
            this.Site = new HashSet<Site>();
        }
    
        public int Id { get; set; }
        public string intitule { get; set; }
        public string couleur { get; set; }
        public Nullable<int> note { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<bool> actif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Elements> Elements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Site> Site { get; set; }
    }
}
