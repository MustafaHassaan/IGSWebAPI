using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGS.Models
{
    public partial class Categories
    {
        public Categories()
        {
            CategoryDetailes = new HashSet<CategoryDetailes>();
            Medias = new HashSet<Medias>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImageName { get; set; }
        public string CategoryImagePath { get; set; }
        public int UserId { get; set; }

        [NotMapped]
        public string UserPhone { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<CategoryDetailes> CategoryDetailes { get; set; }
        public virtual ICollection<Medias> Medias { get; set; }
    }
}
