using ADX2LEDemo.Scripts.Player;
using ADX2LEDemo.Scripts.UtilClass;
using UnityEngine;

namespace ADX2LEDemo.Scripts {
    public class TargetController : MonoBehaviour {
        private int score = 100;
        private float speed = 0.01f;
        private int hp = 1;
        private bool hadDamage = false;
        private bool initializedThis=false;
        
        public delegate void Delegate(int score);

        private Delegate defeatedTarget;
        
        public void Init(int hp,Vector3 instancePos,Delegate defeatedTarget)
        {
            gameObject.name = "Target";
            gameObject.tag = TagStrings.Bullet;
            
            transform.position = instancePos;
            transform.localScale = new Vector3(2f, 0.1f, 2f);
            transform.LookAt(PlayerStatus.Instance.transform);
            transform.Rotate(new Vector3(90, 0, 0));
            
            GetComponent<CapsuleCollider>().isTrigger = true;
            SetParams(hp);

            this.defeatedTarget = defeatedTarget;
            initializedThis = true;
        }

        private void Update() 
        {
            if (!initializedThis||GameSystem.Instance.GetGameOver)
                return;
            
            transform.position = Vector3.MoveTowards(transform.position, PlayerStatus.Instance.transform.position, speed);
        }

        private void SetParams(int hp) {
            Color[] colors = { Color.blue, Color.green, Color.black };
            gameObject.GetComponent<MeshRenderer>().materials[0].color = colors[hp - 1];
            this.hp = hp;
            this.score = hp * 150;
            this.speed = hp * 0.01f;
        }

        private void OnTriggerEnter(Collider other) {
            if (!hadDamage) {
                if (other.CompareTag(TagStrings.Bullet)) {
                    hp--;
                    if(hp <= 0)
                        DefeatTarget();
                } else if (other.CompareTag(TagStrings.Player)) {
                    PlayerDamage();
                }
            } else {
                Debug.LogWarning("Already Damaged.");
            }
            
        }

        private void PlayerDamage() {
            hadDamage = true;
            PlayerStatus.Instance.Damage();
            Destroy(gameObject);
        }

        private void DefeatTarget() {
            defeatedTarget(score);
            Destroy(gameObject);
        }

    }
}