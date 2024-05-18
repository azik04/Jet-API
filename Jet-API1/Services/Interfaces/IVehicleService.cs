using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Vehicles;

namespace Jet_API1.Services.Interfaces;

public interface IVehicleService
{
    Task<BaseResponse<Vehicle>> Create(VehicleVM vehicle);
    BaseResponse<ICollection<VehicleVM>> GetAll();
    Task<BaseResponse<VehicleVM>> Get(int id);
    Task<BaseResponse<Vehicle>> Update(int id, VehicleVM city);
    Task<BaseResponse<Vehicle>> Delete(int id);
}
