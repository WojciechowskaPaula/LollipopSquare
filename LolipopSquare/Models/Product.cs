﻿using System.ComponentModel.DataAnnotations;

namespace LolipopSquare.Models
{
    public class Product
    {
        [Key]
        [Display(Name ="Product ID")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Display(Name ="Product description")]
        [MinLength(10, ErrorMessage ="Description must contains minimum 10 characters")]
        public string Description { get; set; }
        public bool Availability { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
