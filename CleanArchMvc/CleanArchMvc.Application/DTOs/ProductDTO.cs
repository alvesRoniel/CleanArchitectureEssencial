using CleanArchMvc.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchMvc.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The {0} is Required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The {0} is Required")]
        [MinLength(5)]
        [MaxLength(200)]
        [DisplayName("Description")]
        public string Description { get; set; }


        [Required(ErrorMessage = "The {0} is Required")]
        [Column(TypeName ="decimal(18,2)")]
        [DisplayFormat(DataFormatString ="{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "The {0} is Required")]
        [Range(1,9999)]
        [DisplayName("Stok")]
        public int Stok { get; set; }


        [MaxLength(250)]
        [DisplayName("Product Image")]
        public string Image { get; set; }

        [DisplayName("Categories")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
