using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour
{
    private NS_EyeSense.EyeSense _eyes;
    private Vector2 _target;

    // Start is called before the first frame update
    void Start()
    {
        _eyes = gameObject.transform.GetChild(0).gameObject.GetComponent<NS_EyeSense.EyeSense>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_eyes.GetSeePlayer())
        {
            // get player pos
            _target = _eyes.GetPlayerPos();
            // move towards player
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(_target.x, transform.position.y), 0.005f); // Moves only on X axis, maintains Y
        }
    }
}
