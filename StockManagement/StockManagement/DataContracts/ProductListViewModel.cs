namespace StockManagement.DataContracts
{
    public class ProductListViewModel
    {
        public required List<ProductViewModel> Products { get; set; }

        public decimal Total => Products.Sum(x => x.DiscountedPrice);
    }
}
