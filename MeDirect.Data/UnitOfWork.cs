using MeDirect.Core;
using MeDirect.Core.Repositories;
using MeDirect.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeDirect.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private  GameSettingRepository _gameSettingRepository;
        private readonly MeDirectDbContext _context;
        public UnitOfWork(MeDirectDbContext context)
        {
            _context = context;
        }
        public IGameSettingRepository GameSettings => _gameSettingRepository = _gameSettingRepository ?? new GameSettingRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
