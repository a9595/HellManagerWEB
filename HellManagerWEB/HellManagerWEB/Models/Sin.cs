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
    
    public partial class Sin
    {
        public Sin()
        {
            this.Sinners = new HashSet<Sinner>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int PunishmentId { get; set; }
        public int PunisherId { get; set; }
        public Nullable<int> SinDegreeId { get; set; }
    
        public virtual Punisher Punisher { get; set; }
        public virtual Punishment Punishment { get; set; }
        public virtual SinDegree SinDegree { get; set; }
        public virtual ICollection<Sinner> Sinners { get; set; }
    }
}
