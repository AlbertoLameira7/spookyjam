using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(20.0f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            // run impact animation, then destroy
            Destroy(gameObject);
        }
    }
}
