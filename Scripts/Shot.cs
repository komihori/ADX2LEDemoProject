using UnityEngine;

namespace ADX2LEDemo {
    public class Shot : MonoBehaviour {
        [SerializeField] float span = 0.5f;
        [SerializeField] Transform shotPos;
        [SerializeField] float speed = 10f;
        private float time = 0f;

        void Start() {
            time = span;
        }

        void Update() {
            time += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Mouse0) && time > span) {
                BulletGenerate();
            }

        }

        public void SetParams(Transform shotPos, float span = 0.5f, float speed = 10f) {
            this.span = span;
            this.speed = speed;
            this.shotPos = shotPos;
        }

        private void BulletGenerate() {
            GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.name = "Bullet";
            bullet.tag = "Bullet";
            bullet.transform.position = shotPos.transform.position;
            bullet.transform.rotation = this.transform.rotation;
            bullet.transform.localScale = Vector3.one * 0.5f;
            bullet.AddComponent<BulletCon>().SetParams(speed);
        }

        public class BulletCon : MonoBehaviour {
            [SerializeField] float destroyTime = 5f;
            Rigidbody rb = null;
            float speed;
            private void Start() {
                rb = gameObject.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                this.GetComponent<SphereCollider>().isTrigger = true;
                this.GetComponent<MeshRenderer>().materials[0].color = Color.red;
            }

            private void Update() {
                if (GameSystem.Instance.GetGameOver())
                    return;

                rb.velocity = transform.right * speed;
                destroyTime -= Time.deltaTime;
                if (destroyTime <= 0)
                    Destroy(gameObject);
            }

            private void OnTriggerEnter(Collider other) {
                if (other.CompareTag("Target")) {
                    //Destroy(other.gameObject);
                    Destroy(gameObject);
                }

            }

            public void SetParams(float speed) {
                this.speed = speed;
            }

        }

    }

}