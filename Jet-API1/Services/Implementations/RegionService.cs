using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Cityes;
using Jet_API1.ViewModel.Places;
using Jet_API1.ViewModel.Regions;

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

    public async Task<BaseResponse<GetRegionVM>> Get(int id)
    {
        try
        {
            var city = _db.Regions.FirstOrDefault(x => x.Id == id);
            city.City = _db.City.FirstOrDefault(x => x.Id == city.CityId);
            var vm = new GetRegionVM
            {
                Name = city.Name,
                CityId = city.CityId,
                City = new CityVM { Name = city.City.Name},
            };
            return new BaseResponse<GetRegionVM>()
            {
                Data = vm,
                Description = "City has been succesfully Found",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GetRegionVM>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public BaseResponse<ICollection<GetRegionVM>> GetAll()
    {
        try
        {
            var data = _db.Regions.Where(x => !x.IsDeleted).ToList();
            var vms = new List<GetRegionVM>();
            foreach (var item in data)
            {
                item.City = _db.City.FirstOrDefault(x=>x.Id == item.CityId);
                var vm = new GetRegionVM
                {
                    Name = item.Name,
                    City = new CityVM { Name = item.City.Name },
                    CityId = item.CityId,
                };
                vms.Add(vm);
            }
            return new BaseResponse<ICollection<GetRegionVM>>()
            {
                Data = vms,
                Description = "Cities have been successfully retrieved",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<GetRegionVM>>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<BaseResponse<Region>> Update(int id, CreateRegionVM region)
    {
        try
        {
            var data = _db.Regions.FirstOrDefault(x => x.Id == id);
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
