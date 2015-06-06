using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HellManagerWEB.Models
{
    public class SinnerWithSins
    {
        public SinnerWithSins()
        {
            Sinner = new Sinner();
            this.Sins = new List<Sin>();
            this.SinsSelectedIndexes = new List<int>();
            this.Gender = new List<Gender>();
        }

        public Sinner Sinner { get; set; }
        public ICollection<Sin> Sins { get; set; }
        public ICollection<int> SinsSelectedIndexes { get; set; }
        public IEnumerable Gender { get; set; }
        public int SelectedGenderId { get; set; }
    }
}