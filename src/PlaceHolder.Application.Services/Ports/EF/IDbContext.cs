using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Ports.EF
{
    public interface IDbContext : IDisposable
    {
        public void Migrate();
    }
}
