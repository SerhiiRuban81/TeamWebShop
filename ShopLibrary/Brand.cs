using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    class Brand
    {
        public int Id { get; set; }

        [Display(Name = "Brand name")]
        public string BrandName { get; set; } = default!;

        [Display(Name = "Country")]
        public string Country { get; set; } = default!;

        public ICollection<Product> Products { get; set; } = default!;
    }
}
