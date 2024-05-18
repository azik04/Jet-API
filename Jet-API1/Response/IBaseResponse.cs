using Jet_API1.Enum;

namespace Jet_API1.BaseResponse;

public interface IBaseResponse<T>
{
    T Data { get; set; }
    string Description { get; set; }
    StatusCode StatusCode { get; set; }
}
