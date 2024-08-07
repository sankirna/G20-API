﻿using G20.Core;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Data;
using G20.Service.Files;
using G20.Service.Messages;
using G20.Service.ProductCombos;
using G20.Service.Products;
using G20.Service.ProductTicketCategoriesMap;
using G20.Service.TicketCategories;
using G20.Service.Venues;
using Nop.Core.Infrastructure;


namespace G20.Service.Orders
{
    public class OrderService : IOrderService
    {
        protected readonly IRepository<Order> _entityRepository;

        public OrderService(IRepository<Order> entityRepository
            
            )
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<Order>> GetOrdersAsync(
            int userId =0,
            OrderStatusEnum? orderStatusEnum=null,
            DateTime? fromDate=null,
            DateTime? toDate = null,
            int pageIndex = 0, 
            int pageSize = int.MaxValue,
            bool getOnlyTotalCount = false)
        {
            var orders = await _entityRepository.GetAllPagedAsync(query =>
            {
                if(userId>0)
                query = query.Where(o => o.UserId == userId);

                if (orderStatusEnum != null)
                {
                    query = query.Where(o => o.OrderStatusId == (int)orderStatusEnum);
                }

                if (fromDate != null)
                {
                    query = query.Where(o => o.CreatedDateTime >= fromDate.ToUTCDataTime());
                }

                if (toDate != null)
                {
                    query = query.Where(o => o.CreatedDateTime <= fromDate.ToUTCDataTime());
                }

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return orders;
        }

        public virtual async Task<Order> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(Order entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        

        public virtual async Task UpdateAsync(Order entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task UpdateOrderStatus(Order entity, OrderStatusEnum orderStatusEnum)
        {
            entity.OrderStatusId = (int)orderStatusEnum;
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task UpdatePaymentStatus(Order entity, PaymentStatus paymentStatus)
        {
            entity.PaymentStatusId = (int)paymentStatus;
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Order entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }

        public virtual async Task<bool> SendOrderNotifications(int orderId)
        {
            var _venueService = EngineContext.Current.Resolve<IVenueService>();
            var _productService = EngineContext.Current.Resolve<IProductService>();
            var _productComboService = EngineContext.Current.Resolve<IProductComboService>();
            var _productTicketCategoryMapService = EngineContext.Current.Resolve<IProductTicketCategoryMapService>();
            var _orderProductItemService = EngineContext.Current.Resolve<IOrderProductItemService>();
            var _orderProductItemDetailService = EngineContext.Current.Resolve<IOrderProductItemDetailService>();
            var _fileService = EngineContext.Current.Resolve<IFileService>();
            var _ticketCategoryService = EngineContext.Current.Resolve<ITicketCategoryService>();
            var _workflowMessageService = EngineContext.Current.Resolve<IWorkflowMessageService>();

            var order = await GetByIdAsync(orderId);
            var orderProductItems = (await _orderProductItemService.GetOrderProductItemsAsync(orderId)).ToList();
            var ticketCategories = (await _ticketCategoryService.GetTicketCategoryAsync(string.Empty)).ToList();
            foreach (var orderProductItem in orderProductItems)
            {
                var productTicketCategoryMap = await _productTicketCategoryMapService.GetByIdAsync(orderProductItem.ProductTicketCategoryMapId);
                var orderProductItemDetails = await _orderProductItemDetailService.GetOrderProductItemDetailsByOrderProductItemIdAsync(orderProductItem.Id);
                var ticketCategory = ticketCategories.FirstOrDefault(x => x.Id == productTicketCategoryMap.TicketCategoryId);
                foreach (var orderProductItemDetail in orderProductItemDetails)
                {
                    var product = await _productService.GetByIdAsync(orderProductItemDetail.ProductId);
                    var venue = new Venue();
                    if (product.VenueId.HasValue)
                    {
                        venue = await _venueService.GetByIdAsync(product.VenueId.Value);
                    }
                    var file = new Core.Domain.File();
                    if (orderProductItemDetail.QRCodeFileId.HasValue)
                    {
                        file = await _fileService.GetByIdAsync(orderProductItemDetail.QRCodeFileId.Value);
                    }
                    await _workflowMessageService.SendOrderNotificationMessageAsync(
                        venue,
                        product,
                        ticketCategory,
                        order,
                        orderProductItem,
                        orderProductItemDetail,
                        file);
                }
            }

            return false;
        }
    }
}
