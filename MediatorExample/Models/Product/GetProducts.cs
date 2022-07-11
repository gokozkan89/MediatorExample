using System;
using MediatorExample.CustomMediator;

namespace MediatorExample.Models.Product
{
	public class GetProductsRequest : IRequest<GetProductsResponse>
	{
		
	}
	public class GetProductsResponse
    {
        public IList<Product> Products { get; set; }
    }
}

