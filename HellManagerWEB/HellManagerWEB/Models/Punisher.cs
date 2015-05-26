//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HellManagerWEB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Punisher
    {
        public Punisher()
        {
            this.Sins = new HashSet<Sin>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Nullable<int> PunisherRankId { get; set; }
    
        public virtual PunisherRank PunisherRank { get; set; }
        public virtual ICollection<Sin> Sins { get; set; }
    }
}
