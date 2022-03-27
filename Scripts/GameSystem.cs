using ADX2LEDemo.Scripts.Player;
using ADX2LEDemo.Scripts.UI;
using ADX2LEDemo.Scripts.Score;
using ADX2LEDemo.Scripts.Target;
using ADX2LEDemo.Scripts.UtilClass;
using UnityEngine;

namespace ADX2LEDemo.Scripts {
    public class GameSystem : MonoBehaviour 
    {
        public static GameSystem Instance { get; private set; }
        
        [SerializeField] UIController uiController;
        
        private ScoreManager scoreManager;
        private TargetManager targetManager;
        
        private bool gameOver = false;
        public bool GetGameOver => gameOver;
        
        private void Awake() {
            if (GameSystem.Instance == null) {
                GameSystem.Instance = this;
            } else {
                Destroy(this);
                return;
            }

            ManagerInit();

        }

        private void ManagerInit()
        {
            scoreManager=new ScoreManager();
            
            uiController = GetComponent<UIController>();
            UIController.Delegate loadSampleScene=LoadSampleScene;
            uiController.Init(loadSampleScene);
            
            new PlayerGenerator().PlayerGenerate();
            
            targetManager = new TargetManager();
            TargetController.Delegate defeatedTarget=DefeatTarget;
            targetManager.Init(defeatedTarget);
        }
        
        void Update() {
            
            if (gameOver)
            {
                return;
            }

            UpdateManager();
            if (PlayerStatus.Instance.GetHP <= 0) {
                GameOver();
            }
        }

        private void UpdateManager()
        {
            targetManager.TryGenerateTarget();
            
        }

        /// <summary>
        /// 倒されたターゲットから呼び出し
        /// </summary>
        /// <param name="defeatedTargetPoint"></param>
        private void DefeatTarget(int defeatedTargetPoint)
        {
            scoreManager.AddScore(defeatedTargetPoint);
            targetManager.AddDestroyTargetNum();
            uiController.UpdateScore(scoreManager.score);
        } 
            
        private void GameOver()
        {
            GetComponent<UIController>().UpdateUiByGameOver(scoreManager.score);
            Cursor.lockState = CursorLockMode.None;
            gameOver = true;
        }

        private void LoadSampleScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneStrings.SampleScene);
        }
    }
}