using Infrastructure.StaticData;

namespace Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        CarColorParams GetCarColorParams();
        ShadersPropNames GetShaderPropNames();
    }
}