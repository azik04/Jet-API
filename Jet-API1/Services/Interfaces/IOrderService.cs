using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Hotel;
using Jet_API1.ViewModel.Orders;

namespace Jet_API1.Services.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponse<Order>> Create(CreateOrderVM city);
        BaseResponse<IQueryable<Order>> GetAll();
        Task<BaseResponse<Order>> Get(int id);
        Task<BaseResponse<Order>> Update(int id, CreateOrderVM city);
        Task<BaseResponse<Order>> Delete(int id);
    }
}
