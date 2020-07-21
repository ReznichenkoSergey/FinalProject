using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.CarMarket
{
    public class Dealer
    {
        [Required]
        public int Id { get; set; }

        public int NativeId { get; set; }

        [Required,
            MaxLength(100)]
        public string Name { get; set; }

        [DefaultValue(true)]
        public DealerStatus Status { get; set; } = DealerStatus.Active;

        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(30)]
        public string City { get; set; }

        [MaxLength(5)]
        public string CountryState { get; set; }

        [MaxLength(10)]
        public string Zip { get; set; }

        [MaxLength(30)]
        public string ContactPhone { get; set; }

        [MaxLength(30)]
        public string WebSite { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual List<Car> Cars { get; set; }
    }

    public enum DealerStatus
    {
        Active,
        Closed,
        Unknown
    }
}
