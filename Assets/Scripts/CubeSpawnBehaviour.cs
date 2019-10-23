using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeSpawnBehaviour : MonoBehaviour {

	const int SKIP_CAMERA_MOVE_UNTIL = 2;

	public GameObject cube;
	public Button dropButton;
	public Camera cam;

	private GameObject latestCube;
	private bool initialUseGravity;
	private Vector3 initialCubePosition;
	private Quaternion initialCubeRotation;
	private float cubeHeight;
	private int numCubesSpawned = 1;
	private RigidbodyConstraints initialCubeRigidbodyConstraints;

	// Use this for initialization
	void Start () {
		Rigidbody rb = cube.GetComponent<Rigidbody>();
		Collider coll = cube.GetComponent<Collider>();

		latestCube = cube;
		cubeHeight = coll.bounds.size.y;
		initialCubePosition = cube.transform.position;
		initialCubeRotation = cube.transform.rotation;
		initialUseGravity = rb.useGravity;
		initialCubeRigidbodyConstraints = rb.constraints;

		dropButton.onClick.AddListener(onDropEvent);

		rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) { onDropEvent(); }
	}

	/// <summary>
	/// Handler for user generated spawn event
	/// </summary>
	void onDropEvent() {
		Debug.Log("CubeSpawnBehaviour: Dropping old cube");
		latestCube.GetComponent<Rigidbody>().useGravity = initialUseGravity;
		latestCube.GetComponent<FloatSideways>().enabled = false;

		StartCoroutine(createNewCubeAfterDelay(3.5f));

	}

	IEnumerator createNewCubeAfterDelay(float delay) {
		yield return new WaitForSeconds(delay);
		createNewCube();
	}

	void createNewCube() {
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
		rb.constraints = initialCubeRigidbodyConstraints;
		newCube.name = "Cube #" + numCubesSpawned;
		
		rb.useGravity = false;
		rb.GetComponent<FloatSideways>().enabled = true;

		numCubesSpawned += 1;
		latestCube = newCube;
	}
}
