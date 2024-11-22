using UnityEngine;

namespace Character.Slime.Controller
{
    public class SlimeController : MonoBehaviour
    {
        private SlimeInputHandler _inputHandler;
        private SlimeMovements _movements;

        [SerializeField] private float moveForce;
        [SerializeField] private float maxMoveSpeed;
        
        [SerializeField] private float jumpCoefficient;
        [SerializeField] private float minJumpForce;
        [SerializeField] private float maxJumpForce;
        
        [SerializeField] private Transform groundChecker;
        private const float GroundCheckRadius = 0.1f;
        [SerializeField] private LayerMask groundLayer;

        public void Awake()
        {
            var rb = GetComponent<Rigidbody2D>();
            var spRen = GetComponent<SpriteRenderer>();
            
            var slimePhysics = new SlimePhysics(
                rb,
                spRen,
                moveForce,
                maxMoveSpeed,
                jumpCoefficient,
                minJumpForce,
                maxJumpForce,
                groundChecker,
                GroundCheckRadius,
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

        public void FixedUpdate()
        {
            _inputHandler.RunMoveEvents(_inputHandler.CurrentMoveVector);
        }
    }
}