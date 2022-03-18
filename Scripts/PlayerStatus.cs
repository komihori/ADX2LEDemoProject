using UnityEngine;

namespace ADX2LEDemo {
    public class PlayerStatus : MonoBehaviour {
        public static PlayerStatus Instance { get; private set; }

        [SerializeField] int hp = 5;

        private void Awake() {
            if (PlayerStatus.Instance == null) {
                PlayerStatus.Instance = this;
            } else {
                Destroy(this);
                return;
            }
        }

        public int GetHP() => hp;
        public int Damage() => hp--;

    }
}