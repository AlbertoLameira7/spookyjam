using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NS_GameManager;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    private GameObject _playerRef;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _playerRef = Instantiate(_playerPrefab, transform.position, Quaternion.identity);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _gameManager.SetPlayerRef(_playerRef);
        _gameManager.AttachPlayerToCamera();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
