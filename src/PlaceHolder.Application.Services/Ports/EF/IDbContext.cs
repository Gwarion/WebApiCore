using System;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Ports.EF
{
    public interface IDbContext : IDisposable
    {
        void Migrate();
        Task<T> ExecuteAsTransaction<T>(Func<Task<T>> action);
    }
}
