using UnityEngine;
using System.Collections;


public enum BOX_COLOR {RED=0, BLUE, GREEN}

public class BoxManager : MonoBehaviour {

	[SerializeField] private GameObject box_prefab;
	private float create_time;
	private int count; 

	void Awake () {
		create_time = 1.0f;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (create_time > .0f) {
			create_time -= Time.deltaTime;
			Debug.Log ("deltaTime : "+ Time.deltaTime);
		} else {
			float fx = (float)Random.Range (-3,4);
			float fy = (float)Random.Range (-3,4);
			Create_Box (new Vector3(fx, fy, 20.0f));
			create_time = 1.0f;
		}
	}

	private void Create_Box(Vector3 base_pos){
		GameObject clone = Instantiate (box_prefab) as GameObject;
		clone.transform.name = string.Format ("{0}{1:D3}", "Box_", count);
		count++;
		BOX_COLOR _color = (BOX_COLOR)Random.Range (0,3);
		Box box = clone.GetComponent<Box> ();
		box.Init (base_pos, _color);
	}
}
