using CleanArchMvc.Domain.Validation;
using System;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : EntityBase
    {
        public Product(string name, string description, decimal price, int stok, string image)
        {
            ValidateDemain(name, description, price, stok, image);
        }

        public Product(int id, string name, string description, decimal price, int stok, string image)
        {
            DomainExceptioValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            CreatedDate = DateTime.Now;
            ValidateDemain(name, description, price, stok, image);
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stok { get; set; }
        public string Image { get; set; }

        //Chave estrangeira 
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        private void ValidateDemain(string name, string description, decimal price, int stok, string image)
        {
            DomainExceptioValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required.");
            DomainExceptioValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters.");

            DomainExceptioValidation.When(string.IsNullOrEmpty(description), "Invalid description. Description is required.");
            DomainExceptioValidation.When(description.Length < 5, "Invalid description, too short, minimum 3 characters.");

            DomainExceptioValidation.When(price < 0, "Invalid Price value.");
            DomainExceptioValidation.When(stok < 0, "Invalid Stok value.");

            DomainExceptioValidation.When(image?.Length > 250, "Invalid image name, too long, maximum 250 characters.");

            Name = name;
            Description = description;
            Price = price;
            Stok = stok;
            Image = image; 
        }

        public void Update(string name, string description, decimal price, int stok, string image, int categoryId)
        {
            ValidateDemain(name, description, price, stok, image);
            ModifiedDate = DateTime.Now;
            CategoryId = categoryId;
        }
    }
}
