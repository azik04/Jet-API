using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Flights;
using Jet_API1.ViewModel.Hotel;
using Jet_API1.ViewModel.Orders;
using Jet_API1.ViewModel.Vehicles;

namespace Jet_API1.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _db;
    public OrderService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BaseResponse<Order>> Create(CreateOrderVM city)
    {
        try
        {
            Order data = new Order()
            {
                CreateAt = DateTime.Now,
                CheckIn = city.CheckIn,
                CheckOut = city.CheckOut,
                HotelId = city.HotelId,
                RegionId = city.RegionId,
                UserName = city.UserName,
                FlightId = city.FlightId,
                VehicleId = city.VehicleId,
                
            };

            await _db.Orders.AddAsync(data);
             _db.SaveChangesAsync();

            return new BaseResponse<Order>()
            {
                Data = data,
                Description = "Order has been successfully created",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Order>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<BaseResponse<Order>> Delete(int id)
    {
        try
        {
            var data = _db.Orders.FirstOrDefault(x => x.Id == id);

            data.IsDeleted = true;
            await _db.SaveChangesAsync();
            return new BaseResponse<Order>()
            {
                Data = data,
                Description = "Order has been succesfully Removed",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Order>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }

    }

    public async Task<BaseResponse<GetOrderVM>> Get(int id)
    {
        try
        {
            var city = _db.Orders.FirstOrDefault(x => x.Id == id);
            city.Hotels = _db.Hotels.FirstOrDefault(x => x.Id == city.HotelId);
            city.Regions = _db.Regions.FirstOrDefault(x => x.Id == city.RegionId);
            city.Flight = _db.Flights.FirstOrDefault(x => x.Id == city.FlightId);
            var vm = new GetOrderVM
            {
                CheckIn = city.CheckIn,
                CheckOut = city.CheckOut,
                FlightId = city.FlightId,
                HotelId = city.HotelId,
                RegionId = city.RegionId,
                UserName = city.UserName,
                VehicleId = city.VehicleId,
                Flight = new FlightVM
                {
                    Name = city.Flight.Name,
                },
                Hotels = new HotelVM
                {
                    Name = city.Hotels.Name
                },
                Vehicle = new VehicleVM { Name = city.Vehicle.Name },
                Regions = new ViewModel.Regions.RegionVM { Name = city.Regions.Name }
            };
            return new BaseResponse<GetOrderVM>()
            {
                Data = vm,
                Description = "Order has been succesfully Found",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GetOrderVM>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public BaseResponse<ICollection<GetOrderVM>> GetAll()
    {
        try
        {
            var data = _db.Orders.Where(x => !x.IsDeleted).ToList();
            var vms = new List<GetOrderVM>();
            foreach (var item in data)
            {
                item.Hotels = _db.Hotels.FirstOrDefault(x => x.Id == item.HotelId);
                item.Regions = _db.Regions.FirstOrDefault(x => x.Id == item.RegionId);
                item.Flight = _db.Flights.FirstOrDefault(x => x.Id == item.FlightId);
                item.Vehicle = _db.Vehicles.FirstOrDefault(x => x.Id == item.VehicleId);
                var vm = new GetOrderVM
                {
                    CheckIn = item.CheckIn,
                    CheckOut = item.CheckOut,
                    FlightId = item.FlightId,
                    HotelId = item.HotelId,
                    RegionId = item.RegionId,
                    UserName = item.UserName,
                    VehicleId = item.VehicleId,
                    Flight = new FlightVM
                    {
                        Name = item.Flight.Name,
                    },
                    Hotels = new HotelVM
                    {
                        Name = item.Hotels.Name
                    },
                    Vehicle = new VehicleVM { Name = item.Vehicle.Name },
                    Regions = new ViewModel.Regions.RegionVM { Name = item.Vehicle.Name}

                };
                vms.Add(vm);
            } 
            return new BaseResponse<ICollection<GetOrderVM>>()
            {
                Data = vms,
                Description = "Order have been successfully retrieved",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<GetOrderVM>>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public BaseResponse<ICollection<Order>> GetbyPage(int page)
    {
        try {
            int pageSize = 3;
            int skip = page * pageSize;
            var data = _db.Orders
                           .Where(x => !x.IsDeleted)
                           .Skip(skip)
                           .Take(pageSize)
                           .ToList();
            foreach (var item in data)
            {
                item.Hotels = _db.Hotels.FirstOrDefault(x => x.Id == item.HotelId);
                item.Regions = _db.Regions.FirstOrDefault(x => x.Id == item.RegionId);
                item.Flight = _db.Flights.FirstOrDefault(x => x.Id == item.FlightId);
            }
            return new BaseResponse<ICollection<Order>>()
            {
                Data = data,
                Description = "Order have been successfully retrieved",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<Order>>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<BaseResponse<Order>> Update(int id, CreateOrderVM order)
    {
        try
        {
            var data = _db.Orders.FirstOrDefault(x => x.Id == id);
            data.CheckIn = order.CheckIn;
            data.CheckOut = order.CheckOut;
            data.UpdateAt = DateTime.Now;
            _db.Orders.Update(data);
            await _db.SaveChangesAsync();
            return new BaseResponse<Order>()
            {
                Data = data,
                Description = $"Order has been succesfully Update",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Order>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }
}
