using System.Linq;
using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class OrderListIndex : AbstractIndexCreationTask<Order>
    {
        public OrderListIndex()
        {
            Map = orders => from order in orders
                            select new
                            {
                                order.OrderDate,
                                order.CustomerId,
                                order.TotalAmount
                            };

            Index(x => x.CustomerId, FieldIndexing.NotAnalyzed);
            Index(x => x.OrderDate, FieldIndexing.NotAnalyzed);
            Index(x => x.TotalAmount, FieldIndexing.NotAnalyzed);
        }
    }
}
