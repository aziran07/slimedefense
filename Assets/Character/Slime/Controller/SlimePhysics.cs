
using UnityEngine;

namespace Character.Slime.Controller
{
    public class SlimePhysics : CharacterPhysics
    {
        private Transform GroundChecker { get; set; }
        private float GroundCheckRadius { get; set; }
        private LayerMask GroundLayer { get; set; }
            
        public bool IsGrounded => Physics2D.Raycast(GroundChecker.position, Vector2.down, GroundCheckRadius, GroundLayer);

        public SlimePhysics(
            Rigidbody2D rbody,
            SpriteRenderer spRenderer,
            float moveForce,
            float maxMoveSpeed,
            float jumpCoefficient,
            float minJumpForce,
            float maxJumpForce,
            Transform groundChecker,
            float groundCheckRadius,
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
            this.GroundCheckRadius = groundCheckRadius;
            this.GroundLayer = groundLayer;
        }
    }
}
