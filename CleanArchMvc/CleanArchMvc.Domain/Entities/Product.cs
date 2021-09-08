using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchMvc.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stok { get; set; }
        public string image { get; set; }

        //Chave estrangeira 
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
