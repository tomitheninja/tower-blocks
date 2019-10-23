using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovementOnCollision : MonoBehaviour {

	public float delayInSecond = 3;

	private bool noCollisionUntilNow = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnCollisionEnter(Collision other) {
		DisableMovementOnCollision otherObj = other.gameObject.GetComponent<DisableMovementOnCollision>();
		if (noCollisionUntilNow) {
			noCollisionUntilNow = false;

			// self
			StartCoroutine(disableMovementAfterDelay(delayInSecond));

			// other
			if (otherObj == null) { // Hit the ground

			} else {
				otherObj.disableMovement();
			}

		}
	}

	private IEnumerator disableMovementAfterDelay(float delay) {
		yield return new WaitForSeconds(delayInSecond);
		disableMovement();
	}

	public void disableMovement() {
		Debug.Log("DisableMovementOnCollision: Disable movement for " + this.name);
		this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}
}
