using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CartsProducts
    {
        public Guid CartsProductsID { get; set; }
        public Guid FK_ProductID { get; set; }
        public Guid FK_CartID { get; set; }

    }


}
