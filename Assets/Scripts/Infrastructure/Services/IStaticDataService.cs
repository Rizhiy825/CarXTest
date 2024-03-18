using Infrastructure.StaticData;

namespace Infrastructure.Services
{
    public interface IStaticDataService
    {
        CarColorParams GetCarColorParams();
        ShadersPropNames GetShaderPropNames();
    }
}