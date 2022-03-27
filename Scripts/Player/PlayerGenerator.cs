using UnityEngine;

namespace ADX2LEDemo.Scripts.Player
{
    public class PlayerGenerator
    {
        public void PlayerGenerate() {
            var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            new PlayerInitializer(player).InitPlayerObj();
        }
    }
}