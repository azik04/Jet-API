using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Cityes;

namespace Jet_API1.Services.Interfaces;

public interface ICityService
{
    Task<BaseResponse<City>> Create (CityVM city);
    BaseResponse<ICollection<CityVM>> GetAll ();
    Task<BaseResponse<CityVM>> Get (int id);
    Task<BaseResponse<City>> Update (int id , CityVM city);
    Task<BaseResponse<City>> Delete (int id);
}
