using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float _gravityMultiplier = 3.0f;
    private bool _isGrounded;
    private Rigidbody2D _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();

        if (_rigidBody == null)

        {
            Debug.Log("No RigidBody Attached to GameObject");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Apply gravity if object isn't on ground
        if (!_isGrounded)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _gravityMultiplier * -1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
            _isGrounded = true;
            Debug.Log("hit floor");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
            _isGrounded = false;
        }
    }
}
