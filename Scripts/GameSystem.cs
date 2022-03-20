using ADX2LEDemoAssets.Scripts.Player;
using ADX2LEDemoAssets.Scripts.UtilClass;
using UnityEngine;

namespace ADX2LEDemo.Scripts {
    public class GameSystem : MonoBehaviour {
        public static GameSystem Instance { get; private set; }

        public int score { get; private set; } = 0;

        public int destroyTargetNum { get; private set; } = 0;

        private int maxTargetNum = 4;

        private int nowTargetNum = 0;

        private int weekTarget = 0;

        private GUIStyle style;

        private bool gameOver = false;

        private float guiX = 10;
        private float guiY = 10;

        private void Awake() {
            if (GameSystem.Instance == null) {
                GameSystem.Instance = this;
            } else {
                Destroy(this);
                return;
            }
            
            new PlayerGenerator().PlayerGenerate();
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
            if (PlayerStatus.Instance.GetHP <= 0) {
                GameOver();
            }
        }
        
        public void AddScore(int addScore) => score += addScore;

        public void AddDestroyTargetNum() {
            nowTargetNum--;
            destroyTargetNum++;
        }

        private void GenerateTarget(int hp) {
            GameObject target = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            target.name = "Target";
            target.transform.localScale = new Vector3(2f, 0.1f, 2f);
            target.transform.position = RandomSpownPos();
            target.GetComponent<CapsuleCollider>().isTrigger = true;
            target.AddComponent<TargetCon>().SetParams(hp);
        }

        private Vector3 RandomSpownPos() {
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

        public bool GetGameOver => gameOver;

        private void OnGUI() {
            Rect rect = new Rect(0, 540, 1920, 400);
            if (!gameOver) {
                GUI.Label(rect, string.Format("Score:{0}\nHP:{1}", score, PlayerStatus.Instance.GetHP), style);
            } else {
                GUI.Label(rect, "GameOver\n" + score.ToString(), style);
                if (GUI.Button(new Rect(guiX, guiY, 400, 300), "Reset"))
                    UnityEngine.SceneManagement.SceneManager.LoadScene(SceneStrings.SampleScene);

            }

        }
    }
}