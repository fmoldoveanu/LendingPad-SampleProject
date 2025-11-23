using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Orders;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrderController(ICreateOrderService createOrderService, IDeleteOrderService deleteOrderService, IGetOrderService getOrderService, IUpdateOrderService updateOrderService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }

        // Create order (POST /orders)
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateOrder([FromBody] OrderModel model)
        {
            var result = _createOrderService.Create(model.OrderDate, model.CustomerId, model.TotalAmount);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = result.Errors });

            return Request.CreateResponse(HttpStatusCode.Created, new OrderData(result.Value));
        }

        // Update order (PUT /orders/{id})
        [HttpPut]
        [Route("{orderId:guid}")]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
                return DoesNotExist();

            var result = _updateOrderService.Update(order, model.OrderDate, model.CustomerId, model.TotalAmount);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = result.Errors });

            return Request.CreateResponse(HttpStatusCode.OK, new OrderData(result.Value));
        }

        // Delete order (DELETE /orders/{id})
        [HttpDelete]
        [Route("{orderId:guid}")]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            var result = _deleteOrderService.DeleteById(orderId);
            if (!result.Success)
                return DoesNotExist();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // Get single order (GET /orders/{id})
        [HttpGet]
        [Route("{orderId:guid}")]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
                return DoesNotExist();

            return Request.CreateResponse(HttpStatusCode.OK, new OrderData(order));
        }

        // Get orders list (GET /orders/list?skip=0&take=50&customerId=...&totalAmount=...)
        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetOrders(int skip = 0, int take = 50, DateTime? orderDate = null, Guid? customerId = null, decimal? totalAmount = null)
        {
            if (take > 100) take = 100;

            var orders = _getOrderService.GetOrders(orderDate, customerId, totalAmount)
                                         .Skip(skip).Take(take)
                                         .Select(o => new OrderData(o))
                                         .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        // Delete all orders (DELETE /orders/clear)
        [HttpDelete]
        [Route("clear")]
        public HttpResponseMessage DeleteAllOrders([FromUri] bool confirm = false)
        {
            if (!confirm)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Confirmation required" });

            _deleteOrderService.DeleteAll();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
