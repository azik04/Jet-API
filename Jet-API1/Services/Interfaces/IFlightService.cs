using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Cityes;
using Jet_API1.ViewModel.Flights;

namespace Jet_API1.Services.Interfaces
{
    public interface IFlightService
    {
        Task<BaseResponse<Flight>> Create(CreateFlightVM flight);
        BaseResponse<ICollection<Flight>> GetAll();
        Task<BaseResponse<Flight>> Get(int id);
        Task<BaseResponse<Flight>> Update(int id, CreateFlightVM city);
        Task<BaseResponse<Flight>> Delete(int id);
    }
}
