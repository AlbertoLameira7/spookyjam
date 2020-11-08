using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

namespace NS_EyeSense
{
    public class EyeSense : MonoBehaviour
    {

        private bool _seePlayer = false;
        private Vector2 _playerPos;

        // Start is called before the first frame update
        void Start()
        {

        }

        public bool GetSeePlayer()
        {
            return _seePlayer;
        }

        public Vector2 GetPlayerPos()
        {
            return _playerPos;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                _playerPos = other.transform.position;
                _seePlayer = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                _seePlayer = false;
            }
        }
    }
}
