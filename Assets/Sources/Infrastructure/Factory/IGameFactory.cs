using System.Collections.Generic;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic.Board;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer();
        void CreateSpawners();
        void CreateBoard();
        TimerBoard TimerBoard { get; }
    }
}