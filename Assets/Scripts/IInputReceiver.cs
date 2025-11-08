using System;
using UnityEngine;

namespace GameInput
{
    internal interface IInputReceiver
    {
        event Action<Vector2> Moving;
        event Action<Vector2> Looking;
        event Action<bool> Sprinting;
        event Action TryJumping;
    }
}
