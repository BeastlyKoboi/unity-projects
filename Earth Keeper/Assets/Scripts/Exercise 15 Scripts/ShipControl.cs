using UnityEngine;

public class ShipControl : MonoBehaviour
{
	/*
	public GameManager gameManager;
	public GameObject antiRock;
	public float speed = 10f;
	public float yLimit = 6;

	public void purSeek(float theta)
    {
        Vector3 spawnPos = transform.position;
        spawnPos += new Vector3(0, 0, 0);
		theta *= Mathf.Rad2Deg;
        gameManager.AddAntiRockToList(Instantiate(antiRock, spawnPos, Quaternion.Euler(0,0,theta)));
    }

	//Exercise 15 requires that you add the code to Update() to have the "vertical axis" keys (Up/Down arrow, W/S key) translate ship in +/- y direction
	//See Exercise 9 Rocket + AntiRocks - Rocsk project for code that handles the "horizontal axis" keys translation in +/- x direction

	void Update()
	{
		// Move the player left and right
		float yInput = Input.GetAxis("Vertical");
		transform.Translate(-yInput * speed * Time.deltaTime, 0f, 0f);

		//clamp the ship's x position
		Vector3 position = transform.position;
		position.y = Mathf.Clamp(position.y, -yLimit, yLimit);
		transform.position = position;

	}*/

}