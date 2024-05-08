using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;

namespace Jet_API1.Services.Implementations;

public class PlaceService : IPlaceService
{
    private readonly ApplicationDbContext _db;
    public PlaceService(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<BaseResponse<Place>> Create(Place city)
    {
        try
        {
            Place data = new Place()
            {
                CreateAt = DateTime.Now,
                Name = city.Name,
            };

            await _db.Places.AddAsync(data);
            await _db.SaveChangesAsync();

            return new BaseResponse<Place>()
            {
                Data = data,
                Description = "City has been successfully created",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Place>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<BaseResponse<Place>> Delete(int id)
    {
        try
        {
            var data = _db.Places.FirstOrDefault(x => x.Id == id);
            data.IsDeleted = true;
            await _db.SaveChangesAsync();
            return new BaseResponse<Place>()
            {
                Data = data,
                Description = "City has been succesfully Removed",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Place>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }

    }

    public async Task<BaseResponse<Place>> Get(int id)
    {
        try
        {
            var data = _db.Places.FirstOrDefault(x => x.Id == id);
            return new BaseResponse<Place>()
            {
                Data = data,
                Description = "City has been succesfully Found",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Place>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public BaseResponse<IQueryable<Place>> GetAll()
    {
        try
        {
            var data = _db.Places.Where(x => !x.IsDeleted);
            return new BaseResponse<IQueryable<Place>>()
            {
                Data = data,
                Description = "Cities have been successfully retrieved",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IQueryable<Place>>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<BaseResponse<Place>> Update(Place city)
    {
        try
        {
            var data = _db.Places.FirstOrDefault(x => x.Id == city.Id);
            data.Name = city.Name;
            data.Description = city.Description;
            data.UpdateAt = DateTime.Now;
            _db.Places.Update(data);
            await _db.SaveChangesAsync();
            return new BaseResponse<Place>()
            {
                Data = data,
                Description = $"City:{city.Name} has been succesfully Update",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Place>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }
}
