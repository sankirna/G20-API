using G20.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.ProductTicketCategoriesMap
{
    public static class ProductTicketCategoryMapExtension
    {
        public static bool IsOutOfStock(this ProductTicketCategoryMap productTicketCategoryMap, int quantity)
        {
            if (productTicketCategoryMap == null)
                return false;

            var isOutofStock = false;

            if (productTicketCategoryMap.Available <= 0)
            {
                isOutofStock = true;
            }

            var totalQuantity = productTicketCategoryMap.Sold + quantity;
            if (totalQuantity> productTicketCategoryMap.Available ) {
                isOutofStock = true;
            }
            return isOutofStock;
        }
    }
}
