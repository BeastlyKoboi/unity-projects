using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMove : MonoBehaviour
{
    public float speed = 2f;
    Vector3 pos;
    Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        vel = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        vel = speed * transform.right;
        pos += Time.deltaTime * vel;
        transform.position = pos;
    }
}
