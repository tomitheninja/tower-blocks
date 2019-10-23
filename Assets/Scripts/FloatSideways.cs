using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatSideways : MonoBehaviour {
	public float floatAmount = 5;
	public float floatSpeed = 0.05f;

	enum Direction { Left, Right }

	private Direction currentDirection = Direction.Left;
	private float initialPosition;

	// Use this for initialization
	void Start () {
		initialPosition = this.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(initialPosition - this.transform.position.x) >= floatAmount) {
			toggleDirection();
		}
		this.transform.position = new Vector3(
			this.transform.position.x + (currentDirection == Direction.Right ? floatSpeed : -floatSpeed),
			this.transform.position.y,
			this.transform.position.z
		);
	}

	void toggleDirection() {
		currentDirection = currentDirection == Direction.Left ? Direction.Right : Direction.Left;
	}
}
