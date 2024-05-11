using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Cityes;
using Jet_API1.ViewModel.Flights;
using Jet_API1.ViewModel.Hotel;

namespace Jet_API1.Services.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _db;
        public HotelService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<BaseResponse<Hotel>> Create(CreateHotelVM hotel)
        {
            try
            {
                Hotel data = new Hotel()
                {
                    CreateAt = DateTime.Now,
                    Name = hotel.Name,
                    RegionId = hotel.RegionId,
                };

                await _db.Hotels.AddAsync(data);
                await _db.SaveChangesAsync();

                return new BaseResponse<Hotel>()
                {
                    Data = data,
                    Description = "City has been successfully created",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Hotel>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<Hotel>> Delete(int id)
        {
            try
            {
                var data = _db.Hotels.FirstOrDefault(x => x.Id == id);
                data.IsDeleted = true;
                await _db.SaveChangesAsync();
                return new BaseResponse<Hotel>()
                {
                    Data = data,
                    Description = "City has been succesfully Removed",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Hotel>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }

        }

        public async Task<BaseResponse<Hotel>> Get(int id)
        {
            try
            {
                var city = _db.Hotels.FirstOrDefault(x => x.Id == id);
               
                return new BaseResponse<Hotel>()
                {
                    Data = city,
                    Description = "City has been succesfully Found",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Hotel>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public BaseResponse<IQueryable<Hotel>> GetAll()
        {
            try
            {
                var data = _db.Hotels.Where(x => !x.IsDeleted);
               
                return new BaseResponse<IQueryable<Hotel>>()
                {
                    Data = data,
                    Description = "Cities have been successfully retrieved",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IQueryable<Hotel>>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<Hotel>> Update(int id, CreateHotelVM hotel)
        {
            try
            {
                var data = _db.Hotels.FirstOrDefault(x => x.Id == id);
                data.Name = hotel.Name;
                data.UpdateAt = DateTime.Now;
                _db.Hotels.Update(data);
                await _db.SaveChangesAsync();
                return new BaseResponse<Hotel>()
                {
                    Data = data,
                    Description = $"City:{hotel.Name} has been succesfully Update",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Hotel>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }
    }
}
