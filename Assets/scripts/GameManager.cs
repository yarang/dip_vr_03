using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	[SerializeField] private Text UI_Score;
	[SerializeField] private Text UI_TargetData;
	[SerializeField] private Text UI_PlayTime;

	private int game_score;
	private float play_time;

	public bool IsOn_Magnet {		set;		get;	}

	private Box target;
	public Box Target{
		set{ target = value as Box;}
		get{ return target;}
	}
	void Awake() {
		game_score = 0;
		play_time = 300.0f;
		IsOn_Magnet = false;
		Target = null;
		Update_Score (0);
	}

	// Update is called once per frame
	void Update () {
		Update_TargetData ();
		if (IsOn_Magnet) {
			if (target != null)
				target.Exit (0);
			IsOn_Magnet = false;
		}
	}

	public void Update_Score(int _score){
		game_score += _score;
		if (game_score < 0) {
			game_score = 0;
		}
		UI_Score.text = "Score: " + game_score;
	}

	public void Update_TargetData(){
		if (target != null)
			UI_TargetData.text = target.name + "(" + target.Box_Color + ")";
		else
			UI_TargetData.text = "Target is null";
	}

	public void Update_PlayTime(){
		if (play_time > .0f) {
			play_time -= Time.deltaTime;
			UI_PlayTime.text = string.Format ("{0:D2}{1}{2:D2}", (int)play_time / 60, ":", (int)play_time % 60);
		} else {
			UI_PlayTime.text = "Game Over";
		}
	}
}
