using System;
using UnityEngine;

namespace Sources.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        public event Action Clicked;

        void Enable();
        void Disable();
    }
}