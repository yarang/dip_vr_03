using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Box : EventTrigger
{
	private GameManager gameManager;
	private Circular_Gauge circular_gauge;
	private float move_speed;
	private BOX_COLOR box_color;
	private bool is_on;

	public void Init (Vector3 base_pos, BOX_COLOR _color)
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		circular_gauge = GameObject.Find ("Circular_Gauge").GetComponent<Circular_Gauge> ();
		move_speed = 2.0f;
		Box_Color = _color;
		is_on = false;

		Set_Pos (base_pos);
	}

	public void Exit (int type)
	{

		//type : 0 -> 마그네틱 트리거,  1: 조준점 게이지,   2: 종료위치에서 사라짐
		switch (type) {
		case 0:
			if (Box_Color == BOX_COLOR.RED)
				gameManager.Update_Score (200);
			else
				gameManager.Update_Score (-100);
			circular_gauge.Exit_Gauge ();
			break;
		case 1:
			if (Box_Color == BOX_COLOR.BLUE)
				gameManager.Update_Score (200);
			else
				gameManager.Update_Score (-100);
			circular_gauge.Exit_Gauge ();
			break;
		case 2:
			if (Box_Color == BOX_COLOR.GREEN)
				gameManager.Update_Score (200);
			else
				gameManager.Update_Score (-100);
			break;
		}
		is_on = false;
		Destroy (gameObject);
	
	}
		
	// Update is called once per frame
	void Update ()
	{
		Add_Pos (Vector3.back * move_speed * Time.deltaTime);
		if (Get_Pos ().z < -6.0f) {
			Exit (2);
		}
		if (is_on && circular_gauge.Is_Full) {
			Exit (1);
		}
	}

	public BOX_COLOR Box_Color {
		get{ return box_color; }
		set { 
			box_color = value;
			switch (box_color) {
			case BOX_COLOR.RED:
				GetComponent<Renderer> ().material.color = Color.red;
				break;
			case BOX_COLOR.BLUE:
				GetComponent<Renderer> ().material.color = Color.blue;
				break;
			case BOX_COLOR.GREEN:
				GetComponent<Renderer> ().material.color = Color.green;
				break;
			}
			;
		}
	}

	public override void OnPointerEnter (PointerEventData eventData)
	{
		is_on = true;
		gameManager.Target = this;
		circular_gauge.Enter_Gauge ();
		Debug.Log ("OnPointerEnter..." + this.name + "(" + Box_Color + ")");
	}

	public override void OnPointerExit (PointerEventData eventData)
	{
		is_on = false;
		if (gameManager.Target == this) {
			gameManager.Target = null;
		}
		circular_gauge.Exit_Gauge ();
		Debug.Log ("onPonterExit...");
	}

	public void Add_Pos (Vector3 p)
	{
		transform.position += p;
	}

	public void Set_Pos (Vector3 p)
	{
		transform.position = p;
	}

	public Vector3 Get_Pos ()
	{
		return transform.position;
	}
}