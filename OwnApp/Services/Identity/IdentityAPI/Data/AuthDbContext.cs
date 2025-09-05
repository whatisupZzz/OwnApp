using BuildingBlocks.EFCore;
using BuildingBlocks.Message.Events;
using Microsoft.EntityFrameworkCore.Storage;

namespace IdentityAPI.Data
{
    public class AuthDbContext: DbContext,IDbContext
    {
        private readonly ILogger<AuthDbContext>? _logger;
        private IDbContextTransaction _currentTransaction;
        public AuthDbContext(DbContextOptions<AuthDbContext> options, ILogger<AuthDbContext>? logger = null) : base(options)
        { 
            _logger = logger;
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            throw new NotImplementedException();
        }

        public Task ExecuteTransactionalAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
