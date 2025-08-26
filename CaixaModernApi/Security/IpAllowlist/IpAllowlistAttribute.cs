using Microsoft.AspNetCore.Mvc;

namespace CaixaModernApi.Security.IpAllowlist
{
    //TypeFilterAttribute permite uso de DI como um atributo regular sem necessidade de 
    // registro no container de DI nem no Filtro do Controlller
    public sealed class IpAllowlistAttribute : TypeFilterAttribute
    {
        public IpAllowlistAttribute() : base(typeof(IpAllowlistAuthorizationFilter)) { }
    }
}