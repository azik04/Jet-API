using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Flights;
using Jet_API1.ViewModel.Hotel;

namespace Jet_API1.Services.Interfaces;

public interface IHotelService
{
    Task<BaseResponse<Hotell>> Create(CreateHotelVM city);
    BaseResponse<ICollection<GetHotelVM>> GetAll();
    Task<BaseResponse<GetHotelVM>> Get(int id);
    Task<BaseResponse<Hotell>> Update(int id, CreateHotelVM city);
    Task<BaseResponse<Hotell>> Delete(int id);
}
