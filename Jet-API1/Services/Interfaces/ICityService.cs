using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Cityes;

namespace Jet_API1.Services.Interfaces
{
    public interface ICityService
    {
        Task<BaseResponse<City>> Create (CreateCityVM city);
        BaseResponse<IQueryable<City>> GetAll ();
        Task<BaseResponse<City>> Get (int id);
        Task<BaseResponse<City>> Update (int id , CreateCityVM city);
        Task<BaseResponse<City>> Delete (int id);
    }
}
