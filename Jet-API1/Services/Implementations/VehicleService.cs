using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Places;
using Jet_API1.ViewModel.Vehicles;

namespace Jet_API1.Services.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _db;
        public VehicleService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<BaseResponse<Vehicle>> Create(VehicleVM vehicle)
        {
            try
            {
                Vehicle data = new Vehicle()
                {
                    CreateAt = DateTime.Now,
                    Name = vehicle.Name,
                };

                await _db.Vehicles.AddAsync(data);
                await _db.SaveChangesAsync();

                return new BaseResponse<Vehicle>()
                {
                    Data = data,
                    Description = "Vehicle has been successfully created",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Vehicle>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<Vehicle>> Delete(int id)
        {
            try
            {
                var data = _db.Vehicles.FirstOrDefault(x => x.Id == id);

                data.IsDeleted = true;
                await _db.SaveChangesAsync();
                return new BaseResponse<Vehicle>()
                {
                    Data = data,
                    Description = "Vehicle has been succesfully Removed",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Vehicle>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }

        }

        public async Task<BaseResponse<VehicleVM>> Get(int id)
        {
            try
            {
                var vehicle = _db.Vehicles.FirstOrDefault(x => x.Id == id);
                var vm = new VehicleVM()
                {
                    Name = vehicle.Name
                };
                return new BaseResponse<VehicleVM>()
                {
                    Data = vm,
                    Description = "Vehicle has been succesfully Found",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<VehicleVM>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public BaseResponse<ICollection<VehicleVM>> GetAll()
        {
            try
            {
                var data = _db.Vehicles.Where(x => !x.IsDeleted).ToList();
                var vms = new List<VehicleVM>();
                foreach (var item in data)
                {
                    var vm = new VehicleVM()
                    {
                        Name = item.Name,
                    };
                    vms.Add(vm);
                }
                return new BaseResponse<ICollection<VehicleVM>>()
                {
                    Data = vms,
                    Description = "Vehicle have been successfully retrieved",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ICollection<VehicleVM>>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<Vehicle>> Update(int id, VehicleVM vehicle)
        {
            try
            {
                var data = _db.Vehicles.FirstOrDefault(x => x.Id == id);
                data.Name = vehicle.Name;
                data.UpdateAt = DateTime.Now;
                _db.Vehicles.Update(data);
                await _db.SaveChangesAsync();
                return new BaseResponse<Vehicle>()
                {
                    Data = data,
                    Description = $"Vehicle:{vehicle.Name} has been succesfully Update",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Vehicle>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }
    }
}