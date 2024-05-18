
using Jet_API1.Context;
using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.Services.Interfaces;
using Jet_API1.ViewModel.Cityes;
using Jet_API1.ViewModel.Flights;
using Jet_API1.ViewModel.Hotel;
using Jet_API1.ViewModel.Regions;

namespace Jet_API1.Services.Implementations;

public class HotelService : IHotelService
{
    private readonly ApplicationDbContext _db;
    public HotelService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BaseResponse<Hotell>> Create(CreateHotelVM hotel)
    {
        try
        {
            Hotell data = new Hotell()
            {
                CreateAt = DateTime.Now,
                Name = hotel.Name,
                RegionId = hotel.RegionId,
            };

            await _db.Hotels.AddAsync(data);
            await _db.SaveChangesAsync();

            return new BaseResponse<Hotell>()
            {
                Data = data,
                Description = "City has been successfully created",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Hotell>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<BaseResponse<Hotell>> Delete(int id)
    {
        try
        {
            var data = _db.Hotels.FirstOrDefault(x => x.Id == id);
            data.IsDeleted = true;
            await _db.SaveChangesAsync();
            return new BaseResponse<Hotell>()
            {
                Data = data,
                Description = "City has been succesfully Removed",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Hotell>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }

    }

    public async Task<BaseResponse<GetHotelVM>> Get(int id)
    {
        try
        {
            var city = _db.Hotels.FirstOrDefault(x => x.Id == id);
            city.Region = _db.Regions.FirstOrDefault(x => x.Id == city.RegionId);
            var vm = new GetHotelVM
            {
                Name = city.Name,
                Region = new GetRegionVM
                {
                    Name = city.Name,
                },
                RegionId = city.RegionId,
            };
            return new BaseResponse<GetHotelVM>()
            {
                Data = vm,
                Description = "City has been succesfully Found",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GetHotelVM>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public BaseResponse<ICollection<GetHotelVM>> GetAll()
    {
        try
        {
            var data = _db.Hotels.Where(x => !x.IsDeleted).ToList();
            var vms = new List<GetHotelVM>();
            foreach (var item in data)
            {
                    item.Region = _db.Regions.SingleOrDefault(x => x.Id == item.RegionId);
                    var vm = new GetHotelVM()
                    {
                        Name = item.Name,
                        Region = new GetRegionVM
                        {
                            Name= item.Name,
                        }, 
                        RegionId = item.RegionId
                    };
                    vms.Add(vm);
            }

            return new BaseResponse<ICollection<GetHotelVM>>()
            {
                Data = vms,
                Description = "Hotels have been successfully retrieved",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<GetHotelVM>>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<BaseResponse<Hotell>> Update(int id, CreateHotelVM hotel)
    {
        try
        {
            var data = _db.Hotels.FirstOrDefault(x => x.Id == id);
            data.Name = hotel.Name;
            data.UpdateAt = DateTime.Now;
            _db.Hotels.Update(data);
            await _db.SaveChangesAsync();
            return new BaseResponse<Hotell>()
            {
                Data = data,
                Description = $"City:{hotel.Name} has been succesfully Update",
                StatusCode = Enum.StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Hotell>()
            {
                Description = ex.Message,
                StatusCode = Enum.StatusCode.Error
            };
        }
    }
}
