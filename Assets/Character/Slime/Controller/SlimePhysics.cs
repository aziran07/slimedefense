
using UnityEngine;

namespace Character.Slime.Controller
{
    public class SlimePhysics : CharacterPhysics
    {
        private Transform GroundChecker { get; set; }
        
        private float GroundCheckDistance { get; set; }
        private Vector2 GroundCheckBoxSize { get; set; }
        private LayerMask GroundLayer { get; set; }
            
        public bool IsGrounded => Physics2D.BoxCast(GroundChecker.position, GroundCheckBoxSize, 0f, Vector2.down, GroundCheckDistance, GroundLayer);

        public SlimePhysics(
            Rigidbody2D rbody,
            SpriteRenderer spRenderer,
            float moveForce,
            float maxMoveSpeed,
            float jumpCoefficient,
            float minJumpForce,
            float maxJumpForce,
            Transform groundChecker,
            float groundCheckDistance,
            Vector2 groundCheckBoxSize,
            LayerMask groundLayer
            ) : base(
            rbody,
            spRenderer,
            moveForce,
            maxMoveSpeed,
            jumpCoefficient,
            minJumpForce,
            maxJumpForce
            )
        {
            this.GroundChecker = groundChecker;
            this.GroundCheckDistance = groundCheckDistance;
            this.GroundCheckBoxSize = groundCheckBoxSize;
            this.GroundLayer = groundLayer;
        }
    }
}
