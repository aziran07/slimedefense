using UnityEngine;

namespace Character.Slime.Controller
{
    public class SlimeController : MonoBehaviour
    {
        private SlimeInputHandler _inputHandler;
        private SlimeMovements _movements;

        [Header("MoveSettings")]
        [SerializeField] private float moveForce;
        [SerializeField] private float maxMoveSpeed;
        
        [Header("JumpSettings")]
        [SerializeField] private float jumpCoefficient;
        [SerializeField] private float minJumpForce;
        [SerializeField] private float maxJumpForce;
        
        [Header("GroundCheckSettings")]
        private const float GroundCheckDistance = 0.1f;
        [SerializeField] private Transform groundChecker;
        [SerializeField] private LayerMask groundLayer;

        public void Awake()
        {
            var rb = GetComponent<Rigidbody2D>();
            var spRen = GetComponent<SpriteRenderer>();
            
            var boxCollider = GetComponent<BoxCollider2D>();
            var groundCheckBoxSize = new Vector2(boxCollider.size.x, boxCollider.size.y / 2);
            
            var slimePhysics = new SlimePhysics(
                rb,
                spRen,
                moveForce,
                maxMoveSpeed,
                jumpCoefficient,
                minJumpForce,
                maxJumpForce,
                groundChecker,
                GroundCheckDistance,
                groundCheckBoxSize,
                groundLayer
                );

            _inputHandler = new SlimeInputHandler(slimePhysics);
        }

        public void OnEnable()
        {
            _inputHandler.Enable();
            _inputHandler.MoveEvents += _inputHandler.Movements.Move;
            _inputHandler.JumpEvents += _inputHandler.Movements.DoubleJump;
            _inputHandler.JumpEvents += _inputHandler.Movements.ChargeJump;
        }

        public void OnDisable()
        {
            _inputHandler.Disable();
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            _inputHandler.RunMoveEvents(_inputHandler.CurrentMoveVector);
        }
    }
}