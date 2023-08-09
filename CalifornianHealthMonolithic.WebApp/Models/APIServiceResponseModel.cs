using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.WebApp.Models.ViewModels;

namespace CalifornianHealthMonolithic.WebApp.Models
{
    public class APIServiceResponseModel
    {
        public HttpResponseMessage Response { get; set; }
        public Object? Result { get; set; }
    }
}