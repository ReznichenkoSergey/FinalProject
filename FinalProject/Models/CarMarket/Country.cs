using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.CarMarket
{
    public class Country
    {
        [Required]
        public int Id { get; set; }

        [Required, 
            MaxLength(3)]
        public string Code { get; set; }

        [Required,
            MaxLength(100)]
        public string Name { get; set; }

        public virtual List<Dealer> Dealers { get; set; }
    }
}
