using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Slime.Controller
{
    public class SlimeController : MonoBehaviour
    {
        private SlimeActions _slimeActions;
        public InputAction slimeMoveAction;
        
        public float moveForce;
        public float jumpForce;
        public float maxMoveSpeed;
        
        public Rigidbody2D slimeRigidbody;
        public Collider2D slimeCollider;
        public Animator slimeAnimator;
        public SpriteRenderer slimeSpriteRenderer;
        
        public Transform groundDetector;
    
        public event System.Action<Vector2> OnMoveEvent;
    
        public bool IsJumping =>
            !Physics2D.Raycast(groundDetector.position, Vector2.down, 0.1f, LayerMask.GetMask("Platform"));

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {  
            OnMoveEvent?.Invoke(Vector2.zero);
        }

        private void Move(Vector2 moveVector)
        {
            slimeSpriteRenderer.flipX = moveVector.x < 0;
            slimeRigidbody.AddForce(moveVector * moveForce, ForceMode2D.Impulse);
        }
        private void Awake()
        {
            _slimeActions = new SlimeActions();
            slimeMoveAction = _slimeActions.SlimeControls.Move;
            
            slimeRigidbody = GetComponent<Rigidbody2D>();
        
            slimeCollider = GetComponent<Collider2D>();
            slimeAnimator = GetComponent<Animator>();
            slimeSpriteRenderer = GetComponent<SpriteRenderer>();
            groundDetector = transform.Find("GroundDetector");
        }

        private void OnEnable()
        {
            _slimeActions.Enable();
            slimeMoveAction.performed += OnMovePerformed;
            slimeMoveAction.canceled += OnMoveCanceled;
            OnMoveEvent += Move;
        }

        private void OnDisable()
        {
            _slimeActions.Disable();
            slimeMoveAction.performed -= OnMovePerformed;
            slimeMoveAction.canceled -= OnMoveCanceled;
            OnMoveEvent -= Move;
        }
    }
}