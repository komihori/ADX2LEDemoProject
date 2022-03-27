using System;
using ADX2LEDemo.Scripts.UtilClass;
using UnityEngine;

namespace ADX2LEDemo.Scripts.Player {
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

        public int GetHP => hp;
        public int Damage() => hp--;

    }
}