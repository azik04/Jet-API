using Jet_API1.Model;
using Jet_API1.Response;
using Jet_API1.ViewModel.Cityes;
using Jet_API1.ViewModel.Places;
using Jet_API1.ViewModel.Regions;

namespace Jet_API1.Services.Interfaces;

public interface IRegionService
{
    Task<BaseResponse<Region>> Create(CreateRegionVM region);
    BaseResponse<ICollection<GetRegionVM>> GetAll();
    Task<BaseResponse<GetRegionVM>> Get(int id);
    Task<BaseResponse<Region>> Update(int id, CreateRegionVM city);
    Task<BaseResponse<Region>> Delete(int id);
}
