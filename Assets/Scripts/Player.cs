using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NS_Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 3.2f;
        private Vector2 _direction;
        private Rigidbody2D _rigidBody;
        private Animator _anim;
        private SpriteRenderer _renderer;
        private bool _isFacingLeft;
        [HideInInspector]
        public bool _canUseDoor;
        private bool _isAiming = false;
        private bool _flashlightOn = false;
        private GameObject _flashlight;
        private GameObject _pistol;
        [SerializeField]
        public GameObject _bulletRef;
        private int _ammoTotal = 20;
        private int _ammoLoaded = 7;
        private int _magSize = 7;

        // Start is called before the first frame update
        void Start()
        {
            _rigidBody = gameObject.GetComponent<Rigidbody2D>();
            _anim = gameObject.GetComponent<Animator>();
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _flashlight = gameObject.transform.GetChild(0).gameObject;
            _flashlight.transform.parent = this.gameObject.transform;
            _pistol = gameObject.transform.GetChild(1).gameObject;

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
            _direction = new Vector2(Input.GetAxis("Horizontal"), _rigidBody.velocity.y);

            if (_canUseDoor && Input.GetKeyDown(KeyCode.E))
            {
                NS_GameManager.GameManager.LoadSceneFromDoor();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                _isAiming = true;
                NS_GameManager.GameManager.Aim();
                ToggleAim();
            }

            if (Input.GetButtonUp("Fire2"))
            {
                _isAiming = false;
                NS_GameManager.GameManager.NoAim();
                ToggleAim();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleFlashLight();
            }

            if (_isAiming && Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }

            // TODO: Implement dynamic arm system, points to where cursor is pointing.
            /*var mouse = Input.mousePosition;
            var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);*/

            MoveCharacter();
        }

        void Reload()
        {
            int magDiff = _magSize - _ammoLoaded; // amount of bullets that need to be reloaded

            if (magDiff > 0)
            {
                if ((_ammoTotal - magDiff) >= 0)
                {
                    _ammoLoaded += magDiff;
                    _ammoTotal -= magDiff;
                }
                else
                {
                    _ammoLoaded += _ammoTotal;
                    _ammoTotal = 0;
                }

                // TODO: Player script should not tell UI to update, UI should update on its own. Need to refactor later
                NS_GameManager.GameManager.UIUpdateAmmoLoaded(_ammoLoaded);
                NS_GameManager.GameManager.UIUpdateAmmoTotal(_ammoTotal);
            }
        }

        public int GetAmmoLoaded()
        {
            return _ammoLoaded;
        }

        public int GetAmmoTotal()
        {
            return _ammoTotal;
        }

        void Shoot()
        {
            // TODO: Use "Pistol_Barrel" as reference point of Instantiate instead of pistol, avoids hardcoded offset values,
            // good for more weapon implementation and fine tuning bullet spawn point
            if (_ammoLoaded > 0)
            {
                GameObject bullet = Instantiate(_bulletRef, _pistol.transform.position + new Vector3(0.18f, 0.06f, 0), _pistol.transform.rotation, _pistol.transform);
                bullet.transform.localScale = new Vector3(0.3f, 0.3f, 0);
                _ammoLoaded -= 1;
                NS_GameManager.GameManager.UIUpdateAmmoLoaded(_ammoLoaded);
            }
            else
            {
                Reload();
            }
        }

        void ToggleAim()
        {
            if (_isAiming)
            {
                // run aim animation
                _anim.SetBool("isAiming", true);
                _pistol.SetActive(true);
            }
            else
            {
                // run no aim animation
                _anim.SetBool("isAiming", false);
                _pistol.SetActive(false);
            }
        }

        void ToggleFlashLight()
        {
            if (_flashlightOn)
            {
                _flashlightOn = false;
                _flashlight.SetActive(false);
            }
            else
            {
                _flashlightOn = true;
                _flashlight.SetActive(true);
            }
        }

        void MoveCharacter()
        {
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
    }
}

