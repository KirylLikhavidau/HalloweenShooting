using Unity.Cinemachine;
using UnityEngine;

namespace GameInput
{
    public class FPInputHandler : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private CinemachineCamera _firstPersonCamera;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private PlayerInputReceiver _playerInput;
        
        [Header("MovementParameters")]
        [SerializeField] private float _acceleration = 20f;
        [SerializeField] private float _walkSpeed = 3.5f;
        [SerializeField] private float _sprintSpeed = 8f;

        [Header("LookingParameters")]
        [SerializeField] private Vector2 _lookSensitivity = new Vector2(0.1f, 0.1f);
        [SerializeField] private float _currentPitch = 0f;

        [Header("CameraParameters")]
        [SerializeField] private float _cameraNormalFOV = 60f;
        [SerializeField] private float _cameraSprintFOV = 70f;
        [SerializeField] private float _cameraFOVSmoothing = 3f;

        [Header("PhysicsParameters")]
        [SerializeField] private float _gravityScale = 3f;
        [SerializeField] private float _jumpHeight = 2f;
        [SerializeField] private float _verticalVelocity = -3f;

        private float _maxSpeed => _sprintInput ? _sprintSpeed : _walkSpeed;
        private bool _sprinting => _sprintInput && _currentSpeed > 0.1f;

        private Vector2 _moveInput;
        private Vector3 _currentVelocity;
        private float _currentSpeed;

        private Vector2 _lookInput;
        private float _pitchLimit = 85f;

        private bool _sprintInput;

        private int _timesJumped = 0;
        private bool _canDoDoubleJump = false;
        private bool _wasGrounded = false;

        private void OnEnable()
        {
            _playerInput.Moving += (value) => _moveInput = value;
            _playerInput.Looking += (value) => _lookInput = value;
            _playerInput.Sprinting += (value) => _sprintInput = value;
            _playerInput.TryJumping += TryJump;
        }

        private void OnDisable()
        {
            _playerInput.Moving -= (value) => _moveInput = value;
            _playerInput.Looking -= (Value) => _lookInput = Value;
            _playerInput.Sprinting -= (value) => _sprintInput = value;
            _playerInput.TryJumping -= TryJump;
        }

        private void Update()
        {
            MoveUpdate();
            LookUpdate();
            CameraFOVUpdate();

            if(!_wasGrounded && _controller.isGrounded)
            {
                _timesJumped = 0;
            }

            _wasGrounded = _controller.isGrounded;
        }

        private void MoveUpdate()
        {
            Vector3 motion = transform.forward * _moveInput.y + transform.right * _moveInput.x;
            motion.y = 0f;
            motion.Normalize();

            if (motion.sqrMagnitude >= 0.01f)
            {
                _currentVelocity = Vector3.MoveTowards(_currentVelocity, motion * _maxSpeed, _acceleration * Time.deltaTime);
            }
            else
            {
                _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, _acceleration * Time.deltaTime);
            }

            if (_controller.isGrounded && _verticalVelocity <= 0.01f)
            {
                _verticalVelocity = -3f;
            }
            else
            {
                _verticalVelocity += Physics.gravity.y * _gravityScale * Time.deltaTime;
            }

            Vector3 fullVelocity = new Vector3(_currentVelocity.x, _verticalVelocity, _currentVelocity.z);
            CollisionFlags flags = _controller.Move(fullVelocity * Time.deltaTime);
            if ((flags & CollisionFlags.Above) != 0 && _verticalVelocity > 0.01f)
            {
                _verticalVelocity = 0f;
            }
            _currentSpeed = _currentVelocity.magnitude;
        }

        private void LookUpdate()
        {
            Vector2 input = new Vector2(_lookInput.x * _lookSensitivity.x, _lookInput.y * _lookSensitivity.y);
            _currentPitch = Mathf.Clamp(_currentPitch - input.y, -_pitchLimit, _pitchLimit);

            _firstPersonCamera.transform.localRotation = Quaternion.Euler(_currentPitch, 0f, 0f);
            transform.Rotate(Vector3.up * input.x);
        }

        private void CameraFOVUpdate()
        {
            float targetFOV = _cameraNormalFOV;

            if (_sprinting)
            {
                float speedRatio = _currentSpeed / _sprintSpeed;
                targetFOV = Mathf.Lerp(_cameraNormalFOV, _cameraSprintFOV, speedRatio);
            }

            _firstPersonCamera.Lens.FieldOfView = Mathf.Lerp(_firstPersonCamera.Lens.FieldOfView, targetFOV, _cameraFOVSmoothing * Time.deltaTime);
        }

        private void TryJump()
        {
            if (!_controller.isGrounded)
            {
                if (!_canDoDoubleJump && _timesJumped >= 2 && _verticalVelocity > 0.01f)
                {
                    return;
                }

                if (!_canDoDoubleJump && _timesJumped >= 2)
                {
                    return;
                }
            }

            _verticalVelocity = Mathf.Sqrt(_jumpHeight * -_jumpHeight * Physics.gravity.y * _gravityScale);

            _timesJumped++;
        }
    }
}
