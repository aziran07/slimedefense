using UnityEngine;

namespace Character.Slime.Controller
{
    public class SlimeMovements : CharacterMovements
    {
        private SlimePhysics _slimePhysics;
        
        public bool canDoubleJump { get; private set; }
        public SlimeMovements(SlimePhysics slimePhysics)
        {
            _slimePhysics = slimePhysics;
        }

        public override void Move(Vector2 direction)
        {
            var targetVelocity = new Vector2(direction.x * _slimePhysics.MaxMoveSpeed, _slimePhysics.Rbody.velocity.y);
            _slimePhysics.Rbody.velocity = targetVelocity;
        }

        public override void Jump(float _)
        {
            if (!_slimePhysics.IsGrounded) return;
            canDoubleJump = true;
            _slimePhysics.Rbody.AddForce(Vector2.up * _slimePhysics.MinJumpForce, ForceMode2D.Impulse);
            Debug.Log($"NormalJumped, JumpForce = {_slimePhysics.MinJumpForce}");
        }

        public void ChargedJump(float startTime)
        {
            if (!_slimePhysics.IsGrounded) return;
            canDoubleJump = true;
            var holdTime = Time.time - startTime;
            var holdForce = holdTime * _slimePhysics.JumpCoefficient;
            var jumpF = Mathf.Clamp(holdForce, _slimePhysics.MinJumpForce, _slimePhysics.MaxJumpForce);
            _slimePhysics.Rbody.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            Debug.Log($"ChargedJumped, JumpForce = {jumpF}");
        }

        public void DoubleJump(float _)
        {
            if (_slimePhysics.IsGrounded || !canDoubleJump) return;
            canDoubleJump = false;
            _slimePhysics.Rbody.AddForce(Vector2.up * _slimePhysics.MinJumpForce, ForceMode2D.Impulse);
            Debug.Log($"DoubleJumped, JumpForce = {_slimePhysics.MinJumpForce}");
        }
    }
}
