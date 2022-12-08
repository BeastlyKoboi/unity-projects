using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Sprite info class
/// Allows other scripts easy access to the bounding circle and box dimensions of a Sprite
/// Make this a component on a Sprite
/// </summary>
public class SpriteInfo : MonoBehaviour 
{
    public float radius;
    public Vector3 center;
    public Vector3 lowLeft;
	public Vector3 upRight;
	public float xScale;
	public float yScale;
  
    // Use this for initialization
    void Start () 
	{
        radius = gameObject.GetComponent<SpriteRenderer>().bounds.extents.magnitude;
        center = gameObject.GetComponent<SpriteRenderer>().bounds.center;

        xScale = gameObject.transform.localScale.x;
		yScale = gameObject.transform.localScale.y;

		lowLeft = gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.min * xScale;
		upRight = gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.max * yScale;
    }
}
