using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Hotel;
using Jet_API1.ViewModel.Orders;

namespace Jet_API1.Services.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponse<Order>> Create(CreateOrderVM city);
        BaseResponse<ICollection<GetOrderVM>> GetAll();
        BaseResponse<ICollection<Order>> GetbyPage(int page);
        Task<BaseResponse<GetOrderVM>> Get(int id);
        Task<BaseResponse<Order>> Update(int id, CreateOrderVM city);
        Task<BaseResponse<Order>> Delete(int id);
    }
}
