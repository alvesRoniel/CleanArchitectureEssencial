using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create Product With Valid State")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
            action.Should()
                .NotThrow<DomainExceptioValidation>();
        }

        [Fact(DisplayName = "Create Product With Negative Value")]
        public void CreateProduct_NegativeIdValues_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
            action.Should()
                .Throw<DomainExceptioValidation>()
                .WithMessage("Invalid Id value.");
        }

        [Fact(DisplayName = "Create Product With Short Name Value")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "Product Image");
            action.Should()
                .Throw<DomainExceptioValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters.");
        }

        [Fact(DisplayName = "Create Product With Long Image Name Value")]
        public void CreateProduct_LongImageNameValue_DomainExceptionLongName()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99,
                "Product image tooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooloooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooonnnnng");
            action.Should()
                .Throw<DomainExceptioValidation>()
                .WithMessage("Invalid image name, too long, maximum 250 characters.");
        }

        [Fact(DisplayName = "Create Product With Null Image Name Value")]
        public void CreateProduct_WithNullImageNameValue_NoDomainException()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<DomainExceptioValidation>();
        }

        [Fact(DisplayName = "Create Product With Null Image Name Value NoNullReferenceException")]
        public void CreateProduct_WithNullImageNameValue_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<NullReferenceException>();
        }

        [Fact(DisplayName = "Create Product With Empty Image Name Value")]
        public void CreateCProduct_WithEmptyImageNameValue_NoDomainException()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, "");
            action.Should()
                .NotThrow<DomainExceptioValidation>();
        }

        [Fact(DisplayName = "Create Product With Invalid Price Value")]
        public void CreateCProduct_InvalidPriceValue_DomainException()
        {
            Action action = () => new Product(1, "Product name", "Product Description", -9.99m, 99, "Product Image");
            action.Should()
                .Throw<DomainExceptioValidation>()
                .WithMessage("Invalid Price value.");
        }


        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStokValue_ExceptionNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, value, "Product Image");
            action.Should()
                .Throw<DomainExceptioValidation>()
                .WithMessage("Invalid Stok value.");
        }
    }
}
