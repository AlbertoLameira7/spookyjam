using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _health = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Damage()
    {
        _health -= 10;

        if (_health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        //run death animation, then destroy
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("hello");
            // run hurt animation, then apply damage
            Damage();
        }
    }
}
