using UnityEngine;

namespace ADX2LEDemo {
    public class TargetCon : MonoBehaviour {
        int score = 100;
        float speed = 0.01f;
        int hp = 1;
        bool alreadyDamege = false;

        void Start() {
            gameObject.tag = "Target";
            transform.LookAt(PlayerStatus.Instance.transform);
            transform.Rotate(new Vector3(90, 0, 0));
        }

        void Update() {
            if (GameSystem.Instance.GetGameOver())
                return;
            transform.position = Vector3.MoveTowards(transform.position, PlayerStatus.Instance.transform.position, speed);
        }

        public void SetParams(int hp) {
            Color[] colors = { Color.blue, Color.green, Color.black };
            gameObject.GetComponent<MeshRenderer>().materials[0].color = colors[hp - 1];
            this.hp = hp;
            this.score = hp * 150;
            this.speed = hp * 0.01f;
        }

        private void OnTriggerEnter(Collider other) {
            if (!alreadyDamege) {
                if (other.CompareTag("Bullet")) {
                    hp--;
                    if(hp <= 0)
                        DefeatTarget();
                } else if (other.CompareTag("Player")) {
                    PlayerDamage();
                }
            } else {
                Debug.LogWarning("Alredy Damage.");
            }
            
        }

        private void PlayerDamage() {
            alreadyDamege = true;
            PlayerStatus.Instance.Damage();
            GameSystem.Instance.AddDestroyTargetNum();
            Destroy(gameObject);
        }

        private void DefeatTarget() {
            GameSystem.Instance.AddScore(score);
            GameSystem.Instance.AddDestroyTargetNum();
            Destroy(gameObject);
        }

    }
}
