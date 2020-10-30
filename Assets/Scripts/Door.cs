using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject _interactable;
    private float _interactableYOffset = 1.2f;
    private GameObject _interactReference;
    [SerializeField]
    private int _sceneToLoad;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _interactReference =  Instantiate(_interactable, new Vector2(transform.position.x, transform.position.y + _interactableYOffset), Quaternion.identity, transform);
            other.GetComponent<Player>()._canUseDoor = true;
            NS_GameManager.GameManager._doorSceneToLoad = _sceneToLoad;
            Debug.Log("Show text");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>()._canUseDoor = false;
            NS_GameManager.GameManager._doorSceneToLoad = null;
            Destroy(_interactReference);
        }
    }
}
