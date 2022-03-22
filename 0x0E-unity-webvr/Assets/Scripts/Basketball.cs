using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    public GameObject ballPrefab;
    private Rigidbody _rb;
    private Vector3 _startPos;
    private Quaternion _zeroRot;
    private bool _cloned = false;

    void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        _startPos = transform.position;
    }

    void Update()
    {
        // Resets the ball when it hits the floor.
        //if (transform.position.y < 0.281)
        //{
        //    _rb.velocity = Vector3.zero;
        //    transform.position = _startPos;
        //}

        // Instantiates a new ball when this one hits the floor.
        if (!_cloned && transform.position.y < 0.281)
        {
            Instantiate(ballPrefab, _startPos, _zeroRot, transform.parent);
            _cloned = true;
        }
    }
}
