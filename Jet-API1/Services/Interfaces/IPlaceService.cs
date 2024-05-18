using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Orders;
using Jet_API1.ViewModel.Places;

namespace Jet_API1.Services.Interfaces;

public interface IPlaceService
{
    Task<BaseResponse<Place>> Create(CreatePalaceVM city);
    BaseResponse<ICollection<GetPlaceVM>> GetAll();
    Task<BaseResponse<GetPlaceVM>> Get(int id);
    Task<BaseResponse<Place>> Update(int id, CreatePalaceVM city);
    Task<BaseResponse<Place>> Delete(int id);
}
