
using UnityEngine;

namespace Character
{
    public abstract class CharacterMovements
    {
        public abstract void Move(Vector2 direction);
        public abstract void Jump(float jumpTime);
    }
}

