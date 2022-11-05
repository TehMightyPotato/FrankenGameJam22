using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Extensions
{
    public static class CameraExtensions
    {
        /// <summary>
        /// Convert a Mouse Position to a World Position using a Camera
        /// </summary>
        /// <param name="camera">the camera</param>
        /// <param name="mousePosition">the mouse position</param>
        /// <param name="invertZAxis">if the Z-Axis should </param>
        /// <returns>A Vector3 in World Position</returns>
        public static Vector3 MousePositionToWorldPosition(this Camera camera, Vector3 mousePosition,
            bool invertZAxis = true)
        {
            if (invertZAxis)
            {
                mousePosition.z = camera.transform.position.z * -1;
            }
            return camera.ScreenToWorldPoint(mousePosition);
        }

        /// <summary>
        /// Gets the Main Camera for the active Scene
        /// </summary>
        /// <returns>Main Camera</returns>
        public static Camera GetMainCamForActiveScene()
        {
            var cams = Camera.allCameras;
            var scene = SceneManager.GetActiveScene();
            return cams.FirstOrDefault(cam => cam.gameObject.scene == scene);
        }
    }
}