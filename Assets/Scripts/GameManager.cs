using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

namespace NS_GameManager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playRef;
        [SerializeField]
        private GameObject _cameraRef;
        private CinemachineVirtualCamera _cameraComponent;
        public static GameManager _gameManagerStatic;
        public static int? _doorSceneToLoad; // nullable int to avoid having 0 as default, because 0 is also a scene, so to avoid a possible wrong scene load, use null as default instead.

        // Start is called before the first frame update
        void Start()
        {
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

    }
}
