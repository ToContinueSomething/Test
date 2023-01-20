using System.Collections.Generic;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic;
using Sources.Logic.Board;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public Timer Timer { get; }
        public Board Board { get; }
        
        GameObject CreatePlayer();
        void CreateSpawners();
        void CreateBoard();
        void AddHandlers();
        void CreateTimer();
    }
}