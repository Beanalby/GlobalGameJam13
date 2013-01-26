using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    private float moveSpeed = 5f;

	void FixedUpdate() {
        Vector3 pos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        pos *= Time.deltaTime * moveSpeed;
        rigidbody.MovePosition(rigidbody.position + pos);
	}
}
