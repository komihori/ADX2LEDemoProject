using UnityEngine;

namespace ADX2LEDemo.Scripts.Player
{
    public class PlayerInitializer
    {
        private readonly GameObject player;
        private GameObject cube;
        private GameObject shotPos;

        public PlayerInitializer(GameObject player)
        {
            this.player = player;
        }
        
        public void InitPlayerObj()
        {
            player.tag = "Player";
            player.name = "Player";
            
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            InitCube();
            
            shotPos = new GameObject("ShotPos");
            InitShotPosObj();
            
            InitCameraObj();
            
            InitPlayerComponent();
        }

        private void InitPlayerComponent()
        {
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
        private void InitCube()
        {
            cube.transform.SetParent(player.transform);
            cube.transform.localPosition = new Vector3(0.7f, 0.8f, -0.54f);
            cube.transform.localRotation = Quaternion.Euler(new Vector3(0f, -8.31f, 0f));
            cube.transform.localScale = new Vector3(1.6f, 0.4f, 0.4f);
        }
        
        private void InitShotPosObj()
        {
            shotPos.transform.SetParent(cube.transform);
            shotPos.transform.localPosition = new Vector3(0.5f, 0f, 0f);
        }
        
        private void InitCameraObj()
        {
            Camera.main.transform.SetParent(player.transform);
            Camera.main.transform.localPosition = new Vector3(0f, 1.3f, 0f);
            Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
    }
}