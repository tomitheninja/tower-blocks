using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeSpawnBehaviour : MonoBehaviour {

	const int SKIP_CAMERA_MOVE_UNTIL = 3;

	public GameObject cube;
	public Button dropButton;
	public Camera cam;

	private Vector3 initialCubePosition;
	private Quaternion initialCubeRotation;
	private float cubeHeight;
	private int numCubesSpawned = 1;
	private RigidbodyConstraints InitialCubeRigidbodyConstraints;

	// Use this for initialization
	void Start () {
		cubeHeight = cube.GetComponent<Collider>().bounds.size.y;
		initialCubePosition = cube.transform.position;
		initialCubeRotation = cube.transform.rotation;
		InitialCubeRigidbodyConstraints = cube.GetComponent<Rigidbody>().constraints;

		dropButton.onClick.AddListener(onUserSpawnEvent);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) { onUserSpawnEvent(); }
	}

	/// <summary>
	/// Handler for user generated spawn event
	/// </summary>
	void onUserSpawnEvent() {

		Debug.Log("CubeSpawnBehaviour: spawning cube");
		GameObject newCube = Instantiate(cube, new Vector3(
			initialCubePosition.x,
			initialCubePosition.y + Mathf.Max(0, numCubesSpawned - SKIP_CAMERA_MOVE_UNTIL) * cubeHeight,
			initialCubePosition.z
		), initialCubeRotation);
		
		if (numCubesSpawned > SKIP_CAMERA_MOVE_UNTIL) {
			Debug.Log("CubeSpawnBehaviour: moving camera");
			cam.transform.position = new Vector3(
				cam.transform.position.x,
				cam.transform.position.y + cubeHeight,
				cam.transform.position.z
			);
		}

		Rigidbody rb = newCube.GetComponent<Rigidbody>();
		rb.constraints = InitialCubeRigidbodyConstraints;
		newCube.name = "Cube #" + numCubesSpawned;
		
		numCubesSpawned += 1;
	}
}
