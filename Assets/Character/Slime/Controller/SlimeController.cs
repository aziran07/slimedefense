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
        
        [Header("PlatformCheckSettings")]
        private const float PlatformCheckDistance = 0.1f;
        private const float PlatformCheckerOffset = 0.25f;
        [SerializeField] private LayerMask platformLayer;

        public void Awake()
        {
            var tform = GetComponent<Transform>();
            var rb = GetComponent<Rigidbody2D>();
            var spRen = GetComponent<SpriteRenderer>();
            
            var boxCollider = GetComponent<BoxCollider2D>();
            var verticalCheckBoxSize = new Vector2(boxCollider.size.x, boxCollider.size.y / 2);
            var horizontalCheckBoxSize = new Vector2(boxCollider.size.x / 2, boxCollider.size.y);
            
            var slimePhysics = new SlimePhysics(
                tform,
                rb,
                spRen,
                moveForce,
                maxMoveSpeed,
                jumpCoefficient,
                minJumpForce,
                maxJumpForce,
                PlatformCheckDistance,
                PlatformCheckerOffset,
                verticalCheckBoxSize,
                horizontalCheckBoxSize,
                platformLayer
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