using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Orders;

namespace Jet_API1.Services.Implementations
{
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
                };

                await _db.Orders.AddAsync(data);
                await _db.SaveChangesAsync();

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

        public async Task<BaseResponse<Order>> Get(int id)
        {
            try
            {
                var city = _db.Orders.FirstOrDefault(x => x.Id == id);
                city.Hotels = _db.Hotels.FirstOrDefault(x => x.Id == city.HotelId);
                city.Regions = _db.Regions.FirstOrDefault(x => x.Id == city.RegionId);
                city.Flight = _db.Flights.FirstOrDefault(x => x.Id == city.FlightId);
                return new BaseResponse<Order>()
                {
                    Data = city,
                    Description = "Order has been succesfully Found",
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

        public BaseResponse<IQueryable<Order>> GetAll()
        {
            try
            {
                var data = _db.Orders.Where(x => !x.IsDeleted);
                foreach (var item in data)
                {
                    item.Hotels = _db.Hotels.FirstOrDefault(x => x.Id == item.HotelId);
                    item.Regions = _db.Regions.FirstOrDefault(x => x.Id == item.RegionId);
                    item.Flight = _db.Flights.FirstOrDefault(x => x.Id == item.FlightId);
                }
                return new BaseResponse<IQueryable<Order>>()
                {
                    Data = data,
                    Description = "Order have been successfully retrieved",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IQueryable<Order>>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<Order>> Update(Order order)
        {
            try
            {
                var data = _db.Orders.FirstOrDefault(x => x.Id == order.Id);
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
}
