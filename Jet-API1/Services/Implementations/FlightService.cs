using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Cityes;
using Jet_API1.ViewModel.Flights;
using Jet_API1.ViewModel.Vehicles;

namespace Jet_API1.Services.Implementations
{
    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext _db;
        public FlightService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<BaseResponse<Flight>> Create(CreateFlightVM flight)
        {
            try
            {
                Flight data = new Flight()
                {
                    CreateAt = DateTime.Now,
                    Name = flight.Name,
                    Description = flight.Description,
                    VehicleId = flight.VehicleId,
                };
                await _db.Flights.AddAsync(data);
                await _db.SaveChangesAsync();

                return new BaseResponse<Flight>()
                {
                    Data = data,
                    Description = "Flight has been successfully created",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Flight>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<Flight>> Delete(int id)
        {
            try
            {
                var data = _db.Flights.FirstOrDefault(x => x.Id == id);

                data.IsDeleted = true;
                await _db.SaveChangesAsync();
                return new BaseResponse<Flight>()
                {
                    Data = data,
                    Description = "Flight has been succesfully Removed",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Flight>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }

        }

        public async Task<BaseResponse<GetFlightVM>> Get(int id)
        {
            try
            {
                var flight = _db.Flights.FirstOrDefault(x => x.Id == id);
                flight.Vehicle = _db.Vehicles.SingleOrDefault(x => x.Id == flight.VehicleId);
                var vm = new GetFlightVM()
                {
                    Description = flight.Description,
                    Name = flight.Name,
                    Vehicle = new VehicleVM { Name = flight.Vehicle.Name },
                    VehicleId = flight.VehicleId,
                };
                return new BaseResponse<GetFlightVM>()
                {
                    Data = vm,
                    Description = "Flight has been succesfully Found",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetFlightVM>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public BaseResponse<ICollection<GetFlightVM>> GetAll()
        {
            try
            {
                var data = _db.Flights.Where(x => !x.IsDeleted).ToList();
                var vm = new List<GetFlightVM>();
                foreach (var item in data)
                {
                    item.Vehicle = _db.Vehicles.SingleOrDefault(x => x.Id == item.VehicleId);
                    var flight = new GetFlightVM()
                    {
                        Description = item.Description,
                        Name = item.Name,
                        Vehicle = new VehicleVM { Name = item.Vehicle.Name },
                        VehicleId = item.VehicleId,
                    };
                    vm.Add(flight);
                };
                return new BaseResponse<ICollection<GetFlightVM>>()
                {
                    Data = vm,
                    Description = "Flight have been successfully retrieved",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ICollection<GetFlightVM>>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<Flight>> Update(int id, CreateFlightVM flight)
        {
            try
            {
                var data = _db.Flights.FirstOrDefault(x => x.Id == id);
                data.Name = flight.Name;
                data.Description = flight.Description;
                data.VehicleId = flight.VehicleId;
                data.UpdateAt = DateTime.Now;
                _db.Flights.Update(data);
                await _db.SaveChangesAsync();
                return new BaseResponse<Flight>()
                {
                    Data = data,
                    Description = $"Flight:{flight.Name} has been succesfully Update",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Flight>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }
    }
}
