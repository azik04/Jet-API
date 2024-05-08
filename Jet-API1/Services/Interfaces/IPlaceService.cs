using Jet_API1.Model;
using Jet_API1.Response;

namespace Jet_API1.Services.Interfaces
{
    public interface IPlaceService
    {
        Task<BaseResponse<Place>> Create(Place city);
        BaseResponse<IQueryable<Place>> GetAll();
        Task<BaseResponse<Place>> Get(int id);
        Task<BaseResponse<Place>> Update(Place city);
        Task<BaseResponse<Place>> Delete(int id);
    }
}
