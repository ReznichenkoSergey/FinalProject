using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models.CarMarket
{
    public class Car
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(50)]
        public string NativeId { get; set; }

        [MaxLength(30)]
        public string VinCode { get; set; }

        [Required, 
            MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "smallmoney"), 
            Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(250)]
        public string UrlPage { get; set; }

        [MaxLength(30)]
        public string ColorExterior { get; set; }

        [MaxLength(30)]
        public string ColorInterior { get; set; }

        public DateTime DateUpdateInfo { get; set; }

        public CarState CarState { get; set; } = CarState.New;

        public CarStatus CarStatus { get; set; } = CarStatus.Active;

        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }

        public virtual List<CarHistory> CarHistories { get; set; }

        public virtual List<CarPhotoLink> CarPhotoLinks { get; set; }

        //public Media Media { get; set; }
    }

    public enum CarState
    {
        New,
        IsStock
    }

    public enum CarStatus
    {
        Active,
        Saled
    }
}
