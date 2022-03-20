using ADX2LEDemo.Scripts;
using UnityEngine;

namespace ADX2LEDemoAssets.Scripts.Player
{
    public class PlayerGenerator
    {
        private GameObject player;
        private GameObject cube;
        private GameObject shotPos;
        
        public void PlayerGenerate() {
            player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            shotPos = new GameObject("ShotPos");
            SetPlayerObjStatus();
            SetCubeObjStatus();
            SetShotPosObjStatus();
            SetCameraObjStatus();
        }

        private void SetPlayerObjStatus()
        {
            player.tag = "Player";
            player.name = "Player";
            player.transform.position = new Vector3(70, 1, 70);
            player.GetComponent<CapsuleCollider>().isTrigger = true;
            Rigidbody rb = player.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            
            player.AddComponent<PlayerRotation>();
            player.AddComponent<PlayerStatus>();
            player.AddComponent<Shot>().SetParams(shotPos.transform);
            
        }
        
        //@NOTE :Cubeには具体的な名前がほしい 
        private void SetCubeObjStatus()
        {
            cube.transform.SetParent(player.transform);
            cube.transform.localPosition = new Vector3(0.7f, 0.8f, -0.54f);
            cube.transform.localRotation = Quaternion.Euler(new Vector3(0f, -8.31f, 0f));
            cube.transform.localScale = new Vector3(1.6f, 0.4f, 0.4f);
        }
        
        private void SetShotPosObjStatus()
        {
            shotPos.transform.SetParent(cube.transform);
            shotPos.transform.localPosition = new Vector3(0.5f, 0f, 0f);
        }
        
        private void SetCameraObjStatus()
        {
            Camera.main.transform.SetParent(player.transform);
            Camera.main.transform.localPosition = new Vector3(0f, 1.3f, 0f);
            Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
    }
}