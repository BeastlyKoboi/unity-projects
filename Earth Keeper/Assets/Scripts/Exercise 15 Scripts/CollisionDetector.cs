using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Collisions.
/// Component of the Game Manager game object.
/// Determines collisions between 2 game objects.
/// </summary>
public class CollisionDetector: MonoBehaviour 
{

	
    public bool AABBTest(GameObject a, GameObject b)
	{  
    		
		// get horizontal extents of sprite A
		float aMinX = a.transform.position.x + a.GetComponentInChildren<SpriteInfo>().lowLeft.x;
		float aMaxX = a.transform.position.x + a.GetComponentInChildren<SpriteInfo>().upRight.x;
		
		// get vertical extents of sprite A
		float aMinY = a.transform.position.y + a.GetComponentInChildren<SpriteInfo>().lowLeft.y;
		float aMaxY = a.transform.position.y + a.GetComponentInChildren<SpriteInfo>().upRight.y;
		 
        // get horizontal extents of sprite B
		float bMinX = b.transform.position.x + b.GetComponentInChildren<SpriteInfo>().lowLeft.x;
		float bMaxX = b.transform.position.x + b.GetComponentInChildren<SpriteInfo>().upRight.x;
		
		// get vertical extents of sprite B
		float bMinY = b.transform.position.y + b.GetComponentInChildren<SpriteInfo>().lowLeft.y;
		float bMaxY = b.transform.position.y + b.GetComponentInChildren<SpriteInfo>().upRight.y;

        // Check for a collision using the concept of a separating plane, which if it exists between sprite A and sprite B, means they are not colliding
        if (aMaxX < bMinX) //sprite A is completely to the left of sprite B
            return false;
        if (bMaxX < aMinX) //sprite B is completely to the left of sprite A
            return false;
        if (aMaxY < bMinY) //sprite A is completely below sprite B
            return false;
        if (bMaxY < aMinY) //sprite B is completely below sprite A
            return false;
        return true; // the only remaining alternative is that sprite A and B are colliding
	}

	/*
	public bool BoundingSphereTest(GameObject a, GameObject b)
	{

		
	}
  */
}
