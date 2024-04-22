using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGS.Models
{
    public partial class Medias
    {
        public int Id { get; set; }
        public string MedName { get; set; }
        public string AdvertiserName { get; set; }
        public string MedDescription { get; set; }
        public string MedConteact { get; set; }
        public string MedImageAname { get; set; }
        public string MedImageApath { get; set; }
        public string MedImageBname { get; set; }
        public string MedImageBpath { get; set; }
        public string MedImageCname { get; set; }
        public string MedImageCpath { get; set; }
        public string MedImageDname { get; set; }
        public string MedImageDpath { get; set; }
        public string MedImageEname { get; set; }
        public string MedImageEpath { get; set; }
        public string MedImageFname { get; set; }
        public string MedImageFpath { get; set; }
        public string MedLcoationname { get; set; }
        public string MedStuation { get; set; }
        public double? MedLcoationlat { get; set; }
        public double? MedLcoationlon { get; set; }
        public DateTime MedDate { get; set; }
        public DateTime MedDele { get; set; }
        public decimal MedPrice { get; set; }
        public int UserId { get; set; }
        public int CatId { get; set; }
        public int CatdetId { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
        [NotMapped]
        public string DetailesName { get; set; }
        [NotMapped]
        public string UserPhone { get; set; }
        public virtual Categories Cat { get; set; }
        public virtual CategoryDetailes Catdet { get; set; }
        public virtual Users User { get; set; }
    }
}
