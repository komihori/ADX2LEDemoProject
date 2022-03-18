using UnityEngine;

namespace ADX2LEDemo {
    public class PlayerRotation : MonoBehaviour {

        [SerializeField] float speed = 1.0f;

        bool cursorLock = true;

        void Update() {
            if (GameSystem.Instance.GetGameOver())
                return;

            float y = Input.GetAxis("Mouse X") * speed + transform.rotation.eulerAngles.y;
            float z = Input.GetAxis("Mouse Y") * speed + transform.rotation.eulerAngles.z;

            transform.rotation = Quaternion.Euler(0, y, z);

            UpdateCursorLock();
        }

        public void UpdateCursorLock() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                cursorLock = false;
            } else if (Input.GetMouseButton(0)) {
                cursorLock = true;
            }


            if (cursorLock) {
                Cursor.lockState = CursorLockMode.Locked;
            } else if (!cursorLock) {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}