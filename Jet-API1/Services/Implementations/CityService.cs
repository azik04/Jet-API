using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Jet_API1.Services.Implementations
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _db;
        public CityService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<BaseResponse<City>> Create(City city)
        {
            try
            {
                City data = new City()
                {
                    CreateAt = DateTime.Now,
                    Name = city.Name,
                };

                await _db.City.AddAsync(data);
                await _db.SaveChangesAsync();

                return new BaseResponse<City>()
                {
                    Data = data,
                    Description = "City has been successfully created",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<City>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<City>> Delete(int id)
        {
            try
            {
                var data = _db.City.FirstOrDefault(x => x.Id == id);
                data.IsDeleted = true;
                await _db.SaveChangesAsync();
                return new BaseResponse<City>()
                {
                    Data = data,
                    Description = "City has been succesfully Removed",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<City>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
            
        }

        public async Task<BaseResponse<City>> Get(int id)
        {
            try
            {
                var place = _db.Places.Where(x => x.CityId == id);
                var data = _db.City.FirstOrDefault(x => x.Id == id);
                return new BaseResponse<City>()
                {
                    Data = data,
                    Description = "City has been succesfully Found",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<City>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public BaseResponse<IQueryable<City>> GetAll()
        {
            try
            {
                var data = _db.City.Where(x => !x.IsDeleted);
                return new BaseResponse<IQueryable<City>>()
                {
                    Data = data,
                    Description = "Cities have been successfully retrieved",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IQueryable<City>>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }

        public async Task<BaseResponse<City>> Update(City city)
        {
            try
            {
                var data = _db.City.FirstOrDefault(x => x.Id == city.Id);
                data.Name = city.Name;
                data.UpdateAt = DateTime.Now;
                _db.City.Update(data);
                await _db.SaveChangesAsync();
                return new BaseResponse<City>()
                {
                    Data = data,
                    Description = $"City:{city.Name} has been succesfully Update",
                    StatusCode = Enum.StatusCode.Ok
                };
            }
            catch(Exception ex) 
            {
                return new BaseResponse<City>()
                {
                    Description = ex.Message,
                    StatusCode = Enum.StatusCode.Error
                };
            }
        }
    }
}
