using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Hotel;

namespace Jet_API1.Services.Interfaces
{
    public interface IHotelService
    {
        Task<BaseResponse<Hotel>> Create(CreateHotelVM city);
        BaseResponse<IQueryable<Hotel>> GetAll();
        Task<BaseResponse<Hotel>> Get(int id);
        Task<BaseResponse<Hotel>> Update(Hotel city , int id);
        Task<BaseResponse<Hotel>> Delete(int id);
    }
}
