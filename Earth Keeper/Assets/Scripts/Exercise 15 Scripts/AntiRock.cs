using UnityEngine;

public class AntiRock : MonoBehaviour
{ 
    public float speed = 10f;

    Vector3 pos, vel;

    void Start()
    {
        vel = Vector3.zero;
    }

    void Update()
    {
        //Note that the initial rotation applied to this AntiRock, using 
        //Quaternion.Euler(0,0,theta))
        //changes the direction of its right vector
        pos = transform.position;
        vel = speed * transform.right;
        pos += Time.deltaTime * vel;
        transform.position = pos; 
    }
}