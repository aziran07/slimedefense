using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Slime.Controller
{
    public sealed class SlimeInputHandler : SlimeActions
    {
        public SlimeMovements Movements { get; private set; }
        public Vector2 CurrentMoveVector { get; private set; }
        
        public float JumpStartedTime { get; private set; }
        public float JumpCanceledTime { get; private set; }
        
        public event System.Action<Vector2> MoveEvents;
        public event System.Action<float> JumpEvents;
        
        private SpriteRenderer _spriteRenderer;
        
        public SlimeInputHandler(SlimePhysics physics)
        {
            Movements = new SlimeMovements(physics);
            _spriteRenderer = physics.SpRenderer;
            
            JumpStartedTime = 0;
            
            var moveInput = this.SlimeControls.Move;
            var jumpInput = this.SlimeControls.Jump;

            moveInput.performed += OnMovePerformed;
            moveInput.canceled += OnMoveCanceled;
            
            jumpInput.performed += OnJumpPerformed;
            jumpInput.canceled += OnJumpCanceled;
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
            JumpStartedTime = Time.time;
            JumpEvents?.Invoke(SlimeMovements.ChargeJumpDecisionConst);
        }

        private void OnJumpCanceled(InputAction.CallbackContext _)
        {
            JumpCanceledTime = Time.time;
            JumpEvents?.Invoke(JumpStartedTime);
        }
        
        public void RunJumpEvents(float jumpParameter) => JumpEvents?.Invoke(jumpParameter);

        public void RunMoveEvents(Vector2 inputVector) => MoveEvents?.Invoke(inputVector);
    }
}
