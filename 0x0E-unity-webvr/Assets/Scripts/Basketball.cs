using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    public GameObject ballPrefab;
    public bool canScore = false;
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
        // Instantiates a new ball when this one hits the floor.
        if (!_cloned && transform.position.y < 0.281)
        {
            canScore = false;
            Instantiate(ballPrefab, _startPos, _zeroRot, transform.parent);
            _cloned = true;
        }
    }

    void FixedUpdate()
    {
        // Cuts the force of gravity on the ball in half.
        _rb.AddForce(Physics.gravity * -0.5f, ForceMode.Acceleration);
    }
}
