using System;
using MediatorExample.CustomMediator;
using MediatorExample.Models.Product;

namespace MediatorExample.Handlers.Product
{
    public class GetProductsHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    {
        public async Task<GetProductsResponse> HandleAsync(GetProductsRequest request,CancellationToken cancellationToken)
        {

            var response = new GetProductsResponse
            {
                Products = Enumerable.Range(0, new Random().Next(10)).Select((x, i) => new Models.Product.Product
                {
                    Id = Guid.NewGuid(),
                    Name = $"Product - {i}",
                    Price = new Random().Next(10, 2000) 
                }).ToList()
            };

            return await Task.FromResult(response);
        }
    }
}

