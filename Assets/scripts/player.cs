using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	public float speed = 10f;

	void Update()
	{
		InputMovement();
//		Update_Distance ();
	}

//	public void Update_Distance(){
//		GameObject text = GameObject.Find ("UI_Distance");
//		text.GetComponent<Text>().text = "Distance: " + GameObject.Find ("player").transform.position;
//	}

	void InputMovement()
	{
		if (Input.GetKey(KeyCode.W))
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.forward * speed * Time.deltaTime);

		if (Input.GetKey(KeyCode.S))
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.forward * speed * Time.deltaTime);

		if (Input.GetKey(KeyCode.D))
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.right * speed * Time.deltaTime);

		if (Input.GetKey(KeyCode.A))
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position - Vector3.right * speed * Time.deltaTime);
	}
}