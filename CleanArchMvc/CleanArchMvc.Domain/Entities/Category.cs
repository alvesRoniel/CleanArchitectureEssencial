using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : EntityBase
    {
        public Category(string name)
        {
            ValidateDemain(name);
        }

        public Category(int id, string name)
        {
            DomainExceptioValidation.When(id < 0, "Invalid Id value");
            Id = id;
            CreatedDate = DateTime.Now;
            ValidateDemain(name);
            
        }

        public string Name { get; private set; }

        //Propriedade de Navegação
        public ICollection<Product> Products { get; set; }

        private void ValidateDemain(string name)
        {
            DomainExceptioValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
            DomainExceptioValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters");
            
            Name = name;
        }

        public void Update(string name)
        {
            ValidateDemain(name);
            ModifiedDate = DateTime.Now;
        }
    }
}
