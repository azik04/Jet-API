using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Cityes;
using Jet_API1.ViewModel.Orders;
using Jet_API1.ViewModel.Places;
using Microsoft.EntityFrameworkCore;

namespace Jet_API1.Services.Implementations;

public class PlaceService : IPlaceService
{
    private readonly ApplicationDbContext _db;
    public PlaceService(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<BaseResponse<Place>> Create(CreatePalaceVM city)
    {
        try
        {
            Place data = new Place()
            {
                CreateAt = DateTime.Now,
                Description = city.Description,
                CityId = city.CityId,
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

    public async Task<BaseResponse<GetPlaceVM>> Get(int id)
    {
        try
        {
            var data = _db.Places.FirstOrDefault(x => x.Id == id);
            data.City = _db.City.FirstOrDefault(x => x.Id == data.CityId);
            var vm = new GetPlaceVM
            {
                Name = data.Name,
                Description = data.Description,
                CityId = data.CityId,
                City = new CityVM { Name = data.City.Name },
            };
            return new BaseResponse<GetPlaceVM>()
            {
                Data = vm,
                Description = "City has been succesfully Found",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GetPlaceVM>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public BaseResponse<ICollection<GetPlaceVM>> GetAll()
    {
        try
        {
            var data = _db.Places.Where(x => !x.IsDeleted).ToList();
            var vms = new List<GetPlaceVM>();
            foreach (var item  in data)
            {
                item.City = _db.City.SingleOrDefault(x => x.Id == item.CityId);
                var vm = new GetPlaceVM
                {
                    Name = item.Name,
                    Description = item.Description,
                    CityId = item.CityId,
                    City = new CityVM { Name = item.City.Name },
                };
                vms.Add(vm);
            }
            return new BaseResponse<ICollection<GetPlaceVM>>()
            {
                Data = vms,
                Description = "Cities have been successfully retrieved",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<GetPlaceVM>>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<BaseResponse<Place>> Update(int id, CreatePalaceVM place)
    {
        try
        {
            var data = _db.Places.FirstOrDefault(x => x.Id == id);
            data.Name = place.Name;
            data.Description = place.Description;
            data.UpdateAt = DateTime.Now;
            _db.Places.Update(data);
            await _db.SaveChangesAsync();
            return new BaseResponse<Place>()
            {
                Data = data,
                Description = $"City:{data.Name} has been succesfully Update",
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
