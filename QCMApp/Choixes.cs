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
    
    public partial class Choixes
    {
        public int Id { get; set; }
        public string intitule { get; set; }
        public bool statut { get; set; }
        public string imagePath { get; set; }
        public int element_id { get; set; }
        public Nullable<int> image_id { get; set; }
    
        public virtual Elements Elements { get; set; }
    }
}