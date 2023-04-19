using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CATSTAGRAM.Models
{
    public class CatPhoto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string AuthorName { get; set; }

        [Required]
        [EmailAddress]
        public string AuthorEmail { get; set; }

        public string ImageTitle { get; set; }


        public string ImageDescription { get; set; }

        [Display(Name = "Image")]
        public string ImageFileName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        [Display(Name = "Image")]
        public byte[] ImageData { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public string Comments { get; set; }

        public void LoadImageData()
        {
            using (var ms = new MemoryStream())
            {
                ImageFile.CopyTo(ms);
                ImageData = ms.ToArray();
            }
        }
    }
}
