using System;
using UnityEngine.InputSystem;

namespace Sources.Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private readonly PlayerInput _playerInput;

        public event Action Clicked;

        public InputService()
        {
            _playerInput = new PlayerInput();
            Enable();
        }

        public void Enable()
        {
            _playerInput.Enable();
            _playerInput.Shooting.Enable();
            _playerInput.Shooting.Click.performed += OnClicked;
        }

        public void Disable()
        {
            _playerInput.Disable();
            _playerInput.Shooting.Disable();
            _playerInput.Shooting.Click.performed -= OnClicked;
        }

        private void OnClicked(InputAction.CallbackContext obj) => 
            Clicked?.Invoke();
    }
}