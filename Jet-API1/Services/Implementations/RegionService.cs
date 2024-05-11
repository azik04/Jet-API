using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Cityes;
using Jet_API1.ViewModel.Region;

namespace Jet_API1.Services.Implementations;

public class RegionService : IRegionService
{
    private readonly ApplicationDbContext _db;
    public RegionService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BaseResponse<Region>> Create(CreateRegionVM region)
    {
        try
        {
            Region data = new Region()
            {
                CreateAt = DateTime.Now,
                Name = region.Name,
                CityId = region.CityId,
            };

            await _db.Regions.AddAsync(data);
            await _db.SaveChangesAsync();

            return new BaseResponse<Region>()
            {
                Data = data,
                Description = "City has been successfully created",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Region>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<BaseResponse<Region>> Delete(int id)
    {
        try
        {
            var data = _db.Regions.FirstOrDefault(x => x.Id == id);

            data.IsDeleted = true;
            await _db.SaveChangesAsync();
            return new BaseResponse<Region>()
            {
                Data = data,
                Description = "City has been succesfully Removed",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Region>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }

    }

    public async Task<BaseResponse<Region>> Get(int id)
    {
        try
        {
            var city = _db.Regions.FirstOrDefault(x => x.Id == id);
            city.Hotel = _db.Hotels.Where(x => x.RegionId == id).ToList();
            return new BaseResponse<Region>()
            {
                Data = city,
                Description = "City has been succesfully Found",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Region>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public BaseResponse<IQueryable<Region>> GetAll()
    {
        try
        {
            var data = _db.Regions.Where(x => !x.IsDeleted);
            foreach (var item in data)
            {
                item.Hotel = _db.Hotels.Where(x=>x.RegionId == item.Id).ToList();
            }
            return new BaseResponse<IQueryable<Region>>()
            {
                Data = data,
                Description = "Cities have been successfully retrieved",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IQueryable<Region>>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<BaseResponse<Region>> Update(Region region, int id)
    {
        try
        {
            var data = _db.Regions.FirstOrDefault(x => x.Id == region.Id);
            data.Name = region.Name;
            data.CityId = region.CityId;
            data.UpdateAt = DateTime.Now;
            _db.Regions.Update(data);
            await _db.SaveChangesAsync();
            return new BaseResponse<Region>()
            {
                Data = data,
                Description = $"City:{region.Name} has been succesfully Update",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Region>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }
}
