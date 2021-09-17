using CleanArchMvc.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CleanArchMvc.Application.ProductsCQRS.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
