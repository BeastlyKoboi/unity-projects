using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMove : MonoBehaviour
{
    public GameManager gameManager;
    Animator anim;
    public float speed = 2f;
    public bool triggered;
    Vector3 pos;
    Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        vel = Vector3.zero;
        anim = GetComponent<Animator>();
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        vel = speed * transform.right;
        pos += Time.deltaTime * vel;
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile") && !triggered)
        {
            gameManager.RemoveMissile(collision.gameObject);
            gameManager.AsteroidsHit++;
            speed = 0;
            anim.SetTrigger("rocketHit");
            triggered = true;
            gameManager.RemoveAsteroid(this.gameObject);
            Destroy(this.gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
            Debug.Log("Hit");
        }
    }
}
