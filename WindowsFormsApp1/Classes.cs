using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Classes
    {
        // Termék API válaszmodellje
        public class ProductApiResponse
        {
            public ProductContent Content { get; set; }
        }

        public class ProductContent
        {
            public List<Product> Products { get; set; }
        }

        public class Product
        {
            public string Bvin { get; set; }
            public string Sku { get; set; }
            public string ProductName { get; set; }
            public decimal SitePrice { get; set; }
            public decimal SiteCost { get; set; }
            public string ImageFileSmall { get; set; }
            public string VendorId { get; set; }
        }

        // Készlet API válaszmodellje
        public class InventoryApiResponse
        {
            public List<InventoryItem> Content { get; set; }
        }

        public class InventoryItem
        {
            public string Bvin { get; set; }
            public string ProductBvin { get; set; }
            public int QuantityOnHand { get; set; }
            public int QuantityReserved { get; set; }
            public int LowStockPoint { get; set; }
            public int OutOfStockPoint { get; set; }
        }

        // Segédosztály összekapcsolt adat megjelenítésére
        public class ProductWithInventory
        {
            public string Sku { get; set; }
            public string ProductName { get; set; }
            public decimal SitePrice { get; set; }
            public decimal SiteCost { get; set; }
            public int QuantityOnHand { get; set; }
            public int LowStockPoint { get; set; }
            public int OptimalStock { get; set; }
        }

        //Szállítói rendelés osztály
        public class VendorOrder
        {
            public string ProductBvin { get; set; }
            public string VendorId { get; set; }
            public string InventoryBvin { get; set; }
            public string Sku { get; set; }
            public string ProductName { get; set; }
            public int OptimalStock { get; set; }
            public int QuantityNeeded { get; set; }
            public decimal SiteCostTotal { get; set; }
        }

        //Ez a listboxban megjelenítendő szállítók listája (csak szűrésre)
        public class Vendor
        {
            public string Bvin { get; set; }
            public string DisplayName { get; set; }
        }

        public class VendorApiResponse
        {
            public List<Vendor> Content { get; set; }
        }

        // Ez fog megjelenni a szállítói rendelés listában
        public class VendorOrderDisplay
        {
            public string Vendor { get; set; }
            public string Sku { get; set; }
            public string ProductName { get; set; }
            public int QuantityNeeded { get; set; }
            public decimal SiteCostTotal { get; set; }
        }

        public class InventoryUpdateItem
        {
            public string InventoryBvin { get; set; }
            public int QuantityNeeded { get; set; }
        }
    }
}
