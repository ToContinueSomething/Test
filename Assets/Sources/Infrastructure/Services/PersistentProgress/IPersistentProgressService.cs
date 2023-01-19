using Sources.Data;

namespace Sources.Infrastructure.Services.PersistentProgress
{
  public interface IPersistentProgressService : IService
  {
    PlayerProgress Progress { get; set; }
  }
}