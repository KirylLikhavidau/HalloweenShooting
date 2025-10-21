using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action<Vector2> Moving;
        public event Action<Vector2> Looking;
        public event Action<bool> Sprinting;
        public event Action TryJumping;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnMove(InputValue value)
        {
            Moving?.Invoke(value.Get<Vector2>());
        }

        private void OnLook(InputValue value)
        {
            Looking?.Invoke(value.Get<Vector2>());
        }

        private void OnSprint(InputValue value)
        {
            Sprinting?.Invoke(value.isPressed);
        }

        private void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                TryJumping?.Invoke();
            }
        }
    }
}
