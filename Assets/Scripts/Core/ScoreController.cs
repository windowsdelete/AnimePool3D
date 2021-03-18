using UnityEngine;
using System;
using System.Collections;

public class ScoreController : MonoBehaviour {

	int ballNumber;
	bool goal = false;

	public GameObject scboard;
	public GameObject currPl;
	public GameObject ballPn;
	public GameObject ballTx;
	public AudioClip aClip;
    public AudioSource aSour;

	void Update () {
		if(!goal)
		{
			var text = scboard.GetComponent<UnityEngine.UI.Text>();
			var text2 = currPl.GetComponent<UnityEngine.UI.Text>();
			var currentPlayer = PoolGameController.GameInstance.CurrentPlayer;
			var otherPlayer = PoolGameController.GameInstance.OtherPlayer;
			text.text = String.Format("Хiд: {0} - {1}", currentPlayer.Name, currentPlayer.Points);
			text2.text = String.Format("{0} - {1}", otherPlayer.Name, otherPlayer.Points);
		}
		else
		{
			var text = ballTx.GetComponent<UnityEngine.UI.Text>();
			var currentPlayer = PoolGameController.GameInstance.CurrentPlayer;
			text.text = String.Format("{0} забила куля номер {1}", currentPlayer.Name, ballNumber);
		}
	}

    IEnumerator ballGoal()
    {
    	ballPn.SetActive(true);
    	aSour.PlayOneShot(aClip);
    	yield return new WaitForSeconds(3f);
		ballPn.SetActive(false);
    	goal = false;
    }

    public void ballUpdate (int _ballNum)
    {
    	ballNumber = _ballNum;
    	goal = true;
    	StartCoroutine(ballGoal());
    }
}
