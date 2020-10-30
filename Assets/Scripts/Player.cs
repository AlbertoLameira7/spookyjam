using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.2f;
    [SerializeField]
    private float _gravityMultiplier = 3.0f;
    private bool _isGrounded;
    private Vector2 _direction;
    private Rigidbody2D _rigidBody;
    private Animator _anim;
    private SpriteRenderer _renderer;
    private bool _isFacingLeft;
    private GameObject _flashlight;
    [HideInInspector]
    public bool _canUseDoor;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _flashlight = gameObject.transform.GetChild(0).gameObject;

        if (_rigidBody == null)

        {
            Debug.Log("No RigidBody Attached to Player");
        }

        if (_anim == null)
        {
            Debug.Log("No Animator Attached to Player");
        }

        if (_renderer == null)
        {
            Debug.Log("No SpriteRenderer Attached to Player");
        }

        if (_flashlight == null)
        {
            Debug.Log("No Flashlight as Child of Player");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // get Direction from input
        _direction = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (_canUseDoor && Input.GetKeyDown(KeyCode.E))
        {
            NS_GameManager.GameManager.LoadSceneFromDoor();
        }

        MoveCharacter();
    }

    void MoveCharacter()
    {
        // Apply gravity if player isn't on ground
        if (!_isGrounded)
        {
            _direction.y = -0.7f * _gravityMultiplier;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            // Rotate Character Left
            if (Input.GetAxis("Horizontal") < 0 && !_isFacingLeft)
            {
                transform.Rotate(0, 180, 0);
                _isFacingLeft = true;
            }
            // Rotate Character Right
            else if (Input.GetAxis("Horizontal") > 0 && _isFacingLeft)
            {
                transform.Rotate(0, -180, 0);
                _isFacingLeft = false;
            }

            _anim.SetBool("isWalking", true);
        }
        else
        {
            _anim.SetBool("isWalking", false);
        }

        // Move Character
        _rigidBody.velocity = _direction * _speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
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
