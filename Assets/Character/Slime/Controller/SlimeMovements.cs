using UnityEngine;

namespace Character.Slime.Controller
{
    public sealed class SlimeMovements : CharacterMovements
    {
        private SlimePhysics _slimePhysics;
        
        public const float ChargeJumpDecisionConst = -0.1f;

        private bool _canDoubleJump;
        
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        private static bool CallByJumpPerformed(float decisionNum) => decisionNum == ChargeJumpDecisionConst;
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
            if (!_slimePhysics.IsGrounded || CallByJumpPerformed(startTime)) return;
            _canDoubleJump = true;
            var holdTime = Time.time - startTime;
            var holdForce = holdTime * _slimePhysics.JumpCoefficient;
            var jumpF = Mathf.Clamp(holdForce, _slimePhysics.MinJumpForce, _slimePhysics.MaxJumpForce);
            _slimePhysics.Rbody.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            Debug.Log($"ChargedJumped, JumpForce = {jumpF}");
        }

        public void DoubleJump(float decisionNum)
        {
            if (_slimePhysics.IsGrounded || !_canDoubleJump || !CallByJumpPerformed(decisionNum)) return;
            _canDoubleJump = false;
            _slimePhysics.Rbody.velocity = new Vector2(_slimePhysics.Rbody.velocity.x, 0);
            _slimePhysics.Rbody.AddForce(Vector2.up * _slimePhysics.MinJumpForce, ForceMode2D.Impulse);
            Debug.Log($"DoubleJumped, JumpForce = {_slimePhysics.MinJumpForce}");
        }
    }
}
