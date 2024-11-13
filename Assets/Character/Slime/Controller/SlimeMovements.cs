using UnityEngine;

namespace Character.Slime.Controller
{
    public sealed class SlimeMovements : CharacterMovements
    {
        private SlimePhysics _slimePhysics;
        private const float MaxHoldTime = 2f;
        
        private bool _canDoubleJump;
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
            _canDoubleJump = true;
            _slimePhysics.Rbody.AddForce(Vector2.up * _slimePhysics.MinJumpForce, ForceMode2D.Impulse);
            Debug.Log($"NormalJumped, JumpForce = {_slimePhysics.MinJumpForce}");
        }

        public void ChargeJump(float startTime)
        {
            if (!_slimePhysics.IsGrounded) return;
            Debug.Log($"ChargeStartTime = {startTime}");
            _canDoubleJump = true;
            var holdTime = Mathf.Clamp(Time.time - startTime, 0f, MaxHoldTime);
            Debug.Log($"HoldTime = {holdTime}");
            var jumpF = Mathf.Lerp(_slimePhysics.MinJumpForce, _slimePhysics.MaxJumpForce, holdTime/MaxHoldTime);
            // 선형보간으로 더하기
            _slimePhysics.Rbody.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            Debug.Log($"ChargedJumped, JumpForce = {jumpF}");
        }

        public void DoubleJump(float _)
        {
            if (_slimePhysics.IsGrounded || !_canDoubleJump) return;
            _canDoubleJump = false;
            _slimePhysics.Rbody.velocity = new Vector2(_slimePhysics.Rbody.velocity.x, 0);
            _slimePhysics.Rbody.AddForce(Vector2.up * _slimePhysics.MinJumpForce, ForceMode2D.Impulse);
            Debug.Log($"DoubleJumped, JumpForce = {_slimePhysics.MinJumpForce}");
        }
    }
}
