using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollided : MonoBehaviour
{
    public Animator anim;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "First Person Controller")
        {
            Debug.Log("Hit Player");
            anim.SetBool("playerCol", true);
        }
        Debug.Log("collided");
    }

    
}
