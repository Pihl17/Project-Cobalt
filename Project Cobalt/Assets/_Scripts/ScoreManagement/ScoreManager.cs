using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public const int maxTeamAmount = 2;
	public static int[] scores = new int[maxTeamAmount];
	public const int startScore = 10;

	public delegate void ScoreChangeEvent();
	public static event ScoreChangeEvent OnScoreChange;

	// Start is called before the first frame update
	void Start() {
		ResetScores();
		
	}

	public static void ResetScores() {
		scores = new int[maxTeamAmount];
		for (int i = 0; i < scores.Length; i++)
			ChangeScore((Team)i, startScore);
	}

	public static void ChangeScore(Team team, int amount) {
		if ((int)team >= 0 && (int)team < maxTeamAmount) {
			scores[(int)team] = Mathf.Max(scores[(int)team] + amount, 0);
			if (OnScoreChange != null)
				OnScoreChange.Invoke();
		}
	}
}
