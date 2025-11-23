using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Products;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(ICreateProductService createProductService, IDeleteProductService deleteProductService, IGetProductService getProductService, IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
        }

        // Create product (POST /products)
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateProduct([FromBody] ProductModel model)
        {
            var result = _createProductService.Create(model.Name, model.Price, model.Quantity);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = result.Errors });

            return Request.CreateResponse(HttpStatusCode.Created, new ProductData(result.Value));
        }

        // Update product (PUT /products/{id})
        [HttpPut]
        [Route("{productId:guid}")]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
                return DoesNotExist();

            var result = _updateProductService.Update(product, model.Name, model.Price, model.Quantity);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = result.Errors });

            return Request.CreateResponse(HttpStatusCode.OK, new ProductData(result.Value));
        }

        // Delete product (DELETE /products/{id})
        [HttpDelete]
        [Route("{productId:guid}")]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            var result = _deleteProductService.DeleteById(productId);
            if (!result.Success)
                return DoesNotExist();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // Get single product (GET /products/{id})
        [HttpGet]
        [Route("{productId:guid}")]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
                return DoesNotExist();

            return Request.CreateResponse(HttpStatusCode.OK, new ProductData(product));
        }

        // Get products list (GET /products/list?skip=0&take=50&name=...&price=...&quantity=...)
        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetProducts(int skip = 0, int take = 50, string name = null, decimal? price = null, int? quantity = null)
        {
            if (take > 100) take = 100;

            var products = _getProductService.GetProducts(name, price, quantity)
                                             .Skip(skip).Take(take)
                                             .Select(p => new ProductData(p))
                                             .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        // Delete all products (DELETE /products/clear)
        [HttpDelete]
        [Route("clear")]
        public HttpResponseMessage DeleteAllProducts([FromUri] bool confirm = false)
        {
            if (!confirm)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Confirmation required" });

            _deleteProductService.DeleteAll();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
