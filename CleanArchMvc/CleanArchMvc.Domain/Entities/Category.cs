using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchMvc.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Propriedade de Navegação
        public ICollection<Product> Products { get; set; }
    }
}
