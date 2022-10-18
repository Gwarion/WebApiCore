using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PlaceHolder.DependencyInjection
{
    public interface IAdapterInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}
