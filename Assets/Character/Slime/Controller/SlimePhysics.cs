
using UnityEngine;

namespace Character.Slime.Controller
{
    public class SlimePhysics : CharacterPhysics
    {
        private float PlatformCheckDistance { get; set; }
        private float PlatformCheckerOffset { get; set; }
        private Vector2 VerticalCheckBoxSize { get; set; }
        private Vector2 HorizontalCheckBoxSize { get; set; }
        private LayerMask PlatformLayer { get; set; }
            
        public bool IsGrounded => Physics2D.BoxCast(Tform.position - new Vector3(0, PlatformCheckerOffset, 0),
            VerticalCheckBoxSize, 0f, Vector2.down, PlatformCheckDistance, PlatformLayer);
        public bool OnLeftSide => Physics2D.BoxCast(Tform.position - new Vector3(PlatformCheckerOffset, 0, 0),
            HorizontalCheckBoxSize, 0f, Vector2.left, PlatformCheckDistance, PlatformLayer);
        
        public bool OnRightSide => Physics2D.BoxCast(Tform.position + new Vector3(PlatformCheckerOffset, 0, 0),
            HorizontalCheckBoxSize, 0f, Vector2.right, PlatformCheckDistance, PlatformLayer);
        public bool OnUpSide => Physics2D.BoxCast(Tform.position + new Vector3(0, PlatformCheckerOffset, 0),
            VerticalCheckBoxSize, 0f, Vector2.up, PlatformCheckDistance, PlatformLayer);
        

        
        public SlimePhysics(
            Transform tform,
            Rigidbody2D rbody,
            SpriteRenderer spRenderer,
            float moveForce,
            float maxMoveSpeed,
            float jumpCoefficient,
            float minJumpForce,
            float maxJumpForce,
            float platformCheckDistance,
            float platformCheckerOffset,
            Vector2 verticalCheckBoxSize,
            Vector2 horizontalCheckBoxSize,
            LayerMask platformLayer
            ) : base(
            tform,
            rbody,
            spRenderer,
            moveForce,
            maxMoveSpeed,
            jumpCoefficient,
            minJumpForce,
            maxJumpForce
            )
        {
            this.PlatformCheckDistance = platformCheckDistance;
            this.PlatformCheckerOffset = platformCheckerOffset;
            
            this.PlatformLayer = platformLayer;
            
            this.VerticalCheckBoxSize = verticalCheckBoxSize;
            this.HorizontalCheckBoxSize = horizontalCheckBoxSize;
        }
    }
}
