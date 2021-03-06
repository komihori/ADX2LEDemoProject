using UnityEngine;

namespace ADX2LEDemo {
    public class GameSystem : MonoBehaviour {
        public static GameSystem Instance { get; private set; }

        public int score { get; private set; } = 0;

        public int destroyTargetNum { get; private set; } = 0;

        private int maxTargetNum = 4;

        private int nowTargetNum = 0;

        private int weekTarget = 0;

        private GUIStyle style;

        bool gameOver = false;

        float guiX = 10;
        float guiY = 10;

        private void Awake() {
            if (GameSystem.Instance == null) {
                GameSystem.Instance = this;
            } else {
                Destroy(this);
                return;
            }
            PlayerGenerate();
        }

        void Start() {
            style = new GUIStyle();
            style.fontSize = 32;
        }

        void Update() {
            if (nowTargetNum < maxTargetNum && !gameOver) {
                if (weekTarget < 3) {
                    GenerateTarget(1);
                    weekTarget++;
                } else {
                    GenerateTarget(3);
                    weekTarget = 0;
                }
                nowTargetNum++;
            }
            if (PlayerStatus.Instance.GetHP() <= 0) {
                GameOver();
            }
        }

        public void PlayerGenerate() {
            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.tag = "Player";
            player.name = "Player";
            player.transform.position = new Vector3(70, 1, 70);
            player.GetComponent<CapsuleCollider>().isTrigger = true;
            Rigidbody rb = player.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Camera.main.transform.SetParent(player.transform);
            Camera.main.transform.localPosition = new Vector3(0f, 1.3f, 0f);
            Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(player.transform);
            cube.transform.localPosition = new Vector3(0.7f, 0.8f, -0.54f);
            cube.transform.localRotation = Quaternion.Euler(new Vector3(0f, -8.31f, 0f));
            cube.transform.localScale = new Vector3(1.6f, 0.4f, 0.4f);
            GameObject shotPos = new GameObject("ShotPos");
            shotPos.transform.SetParent(cube.transform);
            shotPos.transform.localPosition = new Vector3(0.5f, 0f, 0f);
            player.AddComponent<PlayerRotation>();
            player.AddComponent<PlayerStatus>();
            player.AddComponent<Shot>().SetParams(shotPos.transform);
        }

        public void AddScore(int addScore) => score += addScore;

        public void AddDestroyTargetNum() {
            nowTargetNum--;
            destroyTargetNum++;
        }

        public void GenerateTarget(int hp) {
            GameObject target = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            target.name = "Target";
            target.transform.localScale = new Vector3(2f, 0.1f, 2f);
            target.transform.position = RandomSpownPos();
            target.GetComponent<CapsuleCollider>().isTrigger = true;
            target.AddComponent<TargetCon>().SetParams(hp);
        }

        public Vector3 RandomSpownPos() {
            float[] xPos = { Random.Range(30f, 50f), Random.Range(90f, 110f) };
            float[] zPos = { Random.Range(30f, 50f), Random.Range(90f, 110f) };
            return new Vector3(xPos[Random.Range(0, 2)], 1f, zPos[Random.Range(0, 2)]);
        }

        private void GameOver() {
            style.fontSize = 72;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;
            Cursor.lockState = CursorLockMode.None;
            gameOver = true;
        }

        public bool GetGameOver() => gameOver;

        private void OnGUI() {
            Rect rect = new Rect(0, 540, 1920, 400);
            if (!gameOver) {
                GUI.Label(rect, string.Format("Score:{0}\nHP:{1}", score, PlayerStatus.Instance.GetHP()), style);
            } else {
                GUI.Label(rect, "GameOver\n" + score.ToString(), style);
                if (GUI.Button(new Rect(guiX, guiY, 400, 300), "Reset"))
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");

            }

        }
    }
}