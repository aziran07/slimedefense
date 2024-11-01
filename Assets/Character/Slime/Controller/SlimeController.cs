using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Slime.Controller
{
    public class SlimeController : MonoBehaviour
    {
        private SlimeActions _slimeActions;
        public InputAction moveInput;
        public InputAction jumpInput;
        
        public float moveForce;
        public float jumpCoefficient;
        public float maxJumpForce;
        public float minJumpForce;
        public float maxMoveSpeed;
        public LayerMask platformLayer;
        public Transform groundChecker;
        public float groundCheckRadius;

        private float _jumpStartTime;

        private Vector2 _currentMoveVector;
        
        public Rigidbody2D slimeRigidbody;
        public SpriteRenderer slimeSpriteRenderer;

        public event System.Action<Vector2> MoveEvent;
        public event System.Action<float> JumpEvent;

        public bool IsGrounded => Physics2D.Raycast(groundChecker.position, Vector2.down, groundCheckRadius, platformLayer);

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _currentMoveVector = context.ReadValue<Vector2>();
            slimeSpriteRenderer.flipX = _currentMoveVector.x < 0;
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {  
            _currentMoveVector = Vector2.zero;
        }

        private void OnJumpPerformed(InputAction.CallbackContext _)
        {
            if (!IsGrounded) return;
            _jumpStartTime = Time.time;
        }

        private void OnJumpCanceled(InputAction.CallbackContext _)
        {
            if (!IsGrounded) return;
            JumpEvent?.Invoke(_jumpStartTime);
        }

        private void HorizontalMove(Vector2 inputVector)
        {
            var targetVelocity = new Vector2(inputVector.x * maxMoveSpeed, slimeRigidbody.velocity.y);
            // 기본적인 x축 이동만 담당한다. 따라서 현재 속도의 x값만 재설정 (y축은 점프를 하고 있는 경우도 있으므로)
            slimeRigidbody.velocity = Vector2.Lerp(slimeRigidbody.velocity, targetVelocity, moveForce * Time.fixedDeltaTime);
        }

        private void ChargedJump(float startTime)
        {
            var holdTime = Time.time - startTime; //단위는 초(s)
            var holdForce = holdTime * jumpCoefficient;
            var jumpForce = Mathf.Clamp(holdForce, minJumpForce, maxJumpForce); // 최대, 최소를 정해둔다
            slimeRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        private void Awake()
        {
            _slimeActions = new SlimeActions();
            moveInput = _slimeActions.SlimeControls.Move;
            jumpInput = _slimeActions.SlimeControls.Jump;

            slimeRigidbody = GetComponent<Rigidbody2D>();
            slimeSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _slimeActions.Enable();
            moveInput.performed += OnMovePerformed;
            moveInput.canceled += OnMoveCanceled;
            jumpInput.performed += OnJumpPerformed;
            jumpInput.canceled += OnJumpCanceled;
            MoveEvent += HorizontalMove;
            // 이동은 현재 x축만 이동하도록
            // 특수한 이동을 쓰고싶다면 새로운 이동만들고 원래거 떼고 이벤트에 붙이면 될듯?
            JumpEvent += ChargedJump;
            // 점프는 현재 차지후 점프 (스페이스 입력이 끝나고 그 입력시간에 따라 점프 높이가 다름) 를 쓰게 만듬
            // 점프도 일반적인 점프를 쓰고싶다면 이동처럼 이벤트를 수정하면 될듯?
        }

        private void OnDisable()
        {
            moveInput.performed -= OnMovePerformed;
            moveInput.canceled -= OnMoveCanceled;
            jumpInput.performed -= OnJumpPerformed;
            jumpInput.canceled -= OnJumpCanceled;
            MoveEvent -= HorizontalMove;
            JumpEvent -= ChargedJump;
            _slimeActions.Disable();
        }

        private void FixedUpdate()
        {
            MoveEvent?.Invoke(_currentMoveVector);
        }
    }
}