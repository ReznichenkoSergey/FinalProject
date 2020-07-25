using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.CarMarket
{
    public class CarPhotoLink
    {
        [Required]
        public int Id { get; set; }

        [Required, 
            MaxLength(250)]
        public string Url { get; set; }

        public int CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
