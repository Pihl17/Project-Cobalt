using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreGUI : MonoBehaviour
{

	Text scoreDisplay;
	[SerializeField] Team team = Team.Blue;

	void Awake() {
		scoreDisplay = GetComponent<Text>();
	}

	void DisplayNewScore() {
		scoreDisplay.text = ScoreManager.scores[(int)team].ToString("D3");
	}

	void OnEnable() {
		ScoreManager.OnScoreChange += DisplayNewScore;
	}

	void OnDisable() {
		ScoreManager.OnScoreChange -= DisplayNewScore;
	}
}
