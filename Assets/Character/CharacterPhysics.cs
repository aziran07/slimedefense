using UnityEngine;

namespace Character
{
    public abstract class CharacterPhysics
    {
        public Rigidbody2D Rbody { get; set; }
        public SpriteRenderer SpRenderer { get; set; }
        
        public float MoveForce { get; set; }
        public float MaxMoveSpeed { get; set; }
        public float JumpCoefficient { get; set; }
        public float MinJumpForce { get; set; }
        public float MaxJumpForce { get; set; }

        protected CharacterPhysics(
            Rigidbody2D rbody,
            SpriteRenderer spRenderer,
            float moveForce,
            float maxMoveSpeed,
            float jumpCoefficient,
            float minJumpForce,
            float maxJumpForce
            )
        {
            this.Rbody = rbody;
            this.SpRenderer = spRenderer;
            
            this.MoveForce = moveForce;
            this.MaxMoveSpeed = maxMoveSpeed;
            
            this.JumpCoefficient = jumpCoefficient;
            this.MinJumpForce = minJumpForce;
            this.MaxJumpForce = maxJumpForce;
        }
    }
}
