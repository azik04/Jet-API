using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Region;
using Jet_API1.ViewModel.Vehicles;

namespace Jet_API1.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<BaseResponse<Vehicle>> Create(CreateVehicleVM vehicle);
        BaseResponse<IQueryable<Vehicle>> GetAll();
        Task<BaseResponse<Vehicle>> Get(int id);
        Task<BaseResponse<Vehicle>> Update(int id, CreateVehicleVM city);
        Task<BaseResponse<Vehicle>> Delete(int id);
    }
}
