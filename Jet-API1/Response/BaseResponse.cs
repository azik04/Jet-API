using Jet_API1.BaseResponse;
using Jet_API1.Enum;

namespace Jet_API1.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T Data { get; set; }
        public string Description { get; set ; }
        public StatusCode StatusCode { get ; set ; }
    }
}
