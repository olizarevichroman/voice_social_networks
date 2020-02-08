using System;
using System.Threading.Tasks;
using VoiceSocialNetworks.DataLayer.Abstractions;

namespace VoiceSocialNetworks.DataLayer.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;

        protected bool isDisposed = false;

        public UserRepository UserRepository { get; private set; }

        public VkUserRepository VkUserRepository { get; private set; }

        public UnitOfWork(
            ApplicationContext context,
            UserRepository userRepository,
            VkUserRepository vkUserRepository)
        {
            _context = context;
            UserRepository = userRepository;
            VkUserRepository = vkUserRepository;
        }
        public async Task<int> CommitChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
            }

            isDisposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
