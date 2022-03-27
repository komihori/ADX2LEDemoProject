using ADX2LEDemo.Scripts.Player;
using UnityEngine;

namespace ADX2LEDemo.Scripts.UI
{
    public class UIController:MonoBehaviour
    {
        public delegate void Delegate();
        
        private GUIStyle style;
        private string uiText;
        private Delegate loadSampleScene;
        private const float GuiX = 10;
        private const float GuiY = 10;
        
        private readonly Rect labelRect =new Rect(0, 540, 1920, 400);
        public void Init(Delegate loadSampleScene) {
            style = new GUIStyle();
            style.fontSize = 32;

            this.loadSampleScene = loadSampleScene;
        }
        
        private void OnGUI() {
            GUI.Label(labelRect, uiText, style);
            
            if (GUI.Button(new Rect(GuiX, GuiY, 400, 300), "Reset"))
                loadSampleScene();
        }

        public void UpdateScore(int score)
        {
            uiText = $"Score:{score}\nHP:{PlayerStatus.Instance.GetHP}";
        }
        
        public void UpdateUiByGameOver(int score) {
            style.fontSize = 72;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;
            uiText = "GameOver\n" + score;
        }
    }
}