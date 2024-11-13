using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Slime.Controller
{
    public sealed class SlimeInputHandler
    {
        private SlimeActions _slimeActions;
        public SlimeMovements Movements { get; private set; }
        public Vector2 CurrentMoveVector { get; private set; }
        
        public float ChargeStartedTime { get; private set; }
        public bool OnCharge { get; private set; }
        
        public event System.Action<Vector2> MoveEvents;
        public event System.Action<float> JumpEvents;
        
        private SpriteRenderer _spriteRenderer;
        
        public SlimeInputHandler(SlimePhysics physics)
        {
            Movements = new SlimeMovements(physics);
            _slimeActions = new SlimeActions();
            
            _spriteRenderer = physics.SpRenderer;
            
            ChargeStartedTime = 0;

            var moveInput = _slimeActions.SlimeControls.Move;
            var jumpInput = _slimeActions.SlimeControls.Jump;
            var chargeInput = _slimeActions.SlimeControls.Charge;

            moveInput.performed += OnMovePerformed;
            moveInput.canceled += OnMoveCanceled;
            
            jumpInput.performed += OnJumpPerformed;
            jumpInput.canceled += OnJumpCanceled;
            
            chargeInput.performed += OnChargePerformed;
            chargeInput.canceled += OnChargeCanceled;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            CurrentMoveVector = context.ReadValue<Vector2>();
            _spriteRenderer.flipX = CurrentMoveVector.x < 0;
        }

        private void OnMoveCanceled(InputAction.CallbackContext _)
        {
            CurrentMoveVector = Vector2.zero;
        }

        private void OnJumpPerformed(InputAction.CallbackContext _)
        {
            JumpEvents?.Invoke(OnCharge?ChargeStartedTime:Time.time);
        }

        private void OnJumpCanceled(InputAction.CallbackContext _)
        {
        }

        private void OnChargePerformed(InputAction.CallbackContext _)
        {
            ChargeStartedTime = Time.time;
            OnCharge = true;
        }

        private void OnChargeCanceled(InputAction.CallbackContext _)
        {
            OnCharge = false;
        }
        
        public void RunJumpEvents(float jumpParameter) => JumpEvents?.Invoke(jumpParameter);

        public void RunMoveEvents(Vector2 inputVector) => MoveEvents?.Invoke(inputVector);
        
        public void Enable() => _slimeActions.Enable();

        public void Disable() => _slimeActions.Disable();
    }
}
