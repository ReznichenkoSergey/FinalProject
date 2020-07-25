using FinalProject.Models.CarMarket;

namespace FinalProject.Models.ViewModels
{
    public class CarInfoViewModel
    {
        public int Id { get; set; }

        public string VinCode { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ColorExterior { get; set; }

        public string ColorInterior { get; set; }

        public string CarState { get; set; }

        public int DealerId { get; set; }

        public string DealerName { get; set; }
    }
}
