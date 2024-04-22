using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGS.Models
{
    public partial class CategoryDetailes
    {
        public CategoryDetailes()
        {
            Medias = new HashSet<Medias>();
        }

        public int Id { get; set; }
        public string DetailesName { get; set; }
        public int CatId { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
        public virtual Categories Cat { get; set; }
        public virtual ICollection<Medias> Medias { get; set; }
    }
}
