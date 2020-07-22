using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.CarMarket
{
    public class CarHistory
    {
        [Required]
        public int Id { get; set; }

        [Column(TypeName = "smallmoney"), 
            DataType(DataType.Currency), 
            Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(250)]
        public string PageUrl { get; set; }

        public CarState CarState { get; set; }

        public DateTime? DateLastSeen { get; set; }

        public DateTime? DateScraped { get; set; }

        public DateTime DateFirstSeen { get; set; }

        public virtual Dealer Dealer { get; set; }

        public virtual Car Car { get; set; }
    }
}
