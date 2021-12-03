using MeDirect.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeDirect.Core
{
    public interface IUnitOfWork:IDisposable
    {
        IGameSettingRepository GameSettings { get; }
        Task<int> CommitAsync();
    }
}
