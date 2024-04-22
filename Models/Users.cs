using System;
using System.Collections.Generic;

namespace IGS.Models
{
    public partial class Users
    {
        public Users()
        {
            Categories = new HashSet<Categories>();
            Medias = new HashSet<Medias>();
        }

        public int Id { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public bool UserReadPrivacy { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Categories> Categories { get; set; }
        public virtual ICollection<Medias> Medias { get; set; }
    }
}
