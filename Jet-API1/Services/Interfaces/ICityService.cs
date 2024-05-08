using Jet_API1.Model;
using Jet_API1.Response;

namespace Jet_API1.Services.Interfaces
{
    public interface ICityService
    {
        Task<BaseResponse<City>> Create (City city);
        BaseResponse<IQueryable<City>> GetAll ();
        Task<BaseResponse<City>> Get (int id);
        Task<BaseResponse<City>> Update (City city);
        Task<BaseResponse<City>> Delete (int id);
    }
}
