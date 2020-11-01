using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro.EditorUtilities;

namespace NS_GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playRef;
        [SerializeField]
        private GameObject _cameraRef;
        static private CinemachineVirtualCamera _cameraComponent;
        public static GameManager _gameManagerStatic;
        public static int? _doorSceneToLoad; // nullable int to avoid having 0 as default, because 0 is also a scene, so to avoid a possible wrong scene load, use null as default instead.

        // Start is called before the first frame update
        void Start()
        {
            // TODO: Load default Camera settings from file
            _gameManagerStatic = gameObject.GetComponent<GameManager>();
        }

        public void SetPlayerRef(GameObject playerRef)
        {
            _playRef = playerRef;
        }

        public void SetCameraRef(GameObject camerRef)
        {
            _cameraRef = camerRef;
        }

        public void AttachPlayerToCamera()
        {
            _cameraComponent = _cameraRef.GetComponent<CinemachineVirtualCamera>();
            _cameraComponent.m_Follow = _playRef.transform;
        }

        public void LoadScene(int sceneReference)
        {
            SceneManager.LoadScene(sceneReference);
        }

        static public void LoadSceneFromDoor()
        {
            if (_doorSceneToLoad != null)
            {
                SceneManager.LoadScene(_doorSceneToLoad ?? default(int));
            }
        }

        static public void Aim()
        {
            Debug.Log("Aim from GM");
            _cameraComponent.m_Lens.OrthographicSize = 2.5f;
            // TODO: Try to implement transposer changes with MutateCameraState
            // (https://docs.unity3d.com/Packages/com.unity.cinemachine@2.1/api/Cinemachine.CinemachineFramingTransposer.html)
            // instead of using hardcoded values, these values should come from a separate file with all camera settings
            // avoids possible bugs from manually setting the camera on each scene.
            // _cameraComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.3f;
            _cameraComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.8f;
            _cameraComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0.01f;
            _cameraComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0.1f;
        }

        static public void NoAim()
        {
            // TODO: to default the camera values when not aiming, should use default values from different file.
            _cameraComponent.m_Lens.OrthographicSize = 2.7f;
            // _cameraComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.5f;
            _cameraComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.72f;
            _cameraComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0.05f;
            _cameraComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0.5f;

        }

    }
}
