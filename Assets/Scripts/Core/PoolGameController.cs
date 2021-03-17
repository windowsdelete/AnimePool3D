using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PoolGameController : MonoBehaviour {
	public GameObject mmmmm;
	public GameObject mainBall;
	public GameObject otherBalls;
	public GameObject mainCamera;
	public GameObject scoreBoard;
	public GameObject scoreText;
	public GameObject winMessage;

	public float maxForce;
	public float minForce;
	public float _distanse;
	public Vector3 strikeDirection;

	public const float MIN_DISTANCE = 27.5f;
	public const float MAX_DISTANCE = 32f;
	
	public IGameObjectState currentState;
	public Image mask;

	public Player CurrentPlayer;
	public Player OtherPlayer;

	private bool currentPlayerContinuesToPlay = false;
	private ScoreController _scorecont;

	static public PoolGameController GameInstance {
		get;
		private set;
	}

	void Start() {
		strikeDirection = Vector3.forward;
		CurrentPlayer = new Player("Ариса");
		OtherPlayer = new Player("Мегуми");
		GameInstance = this;
		winMessage.GetComponent<Canvas>().enabled = false;
		currentState = new GameStates.WaitingForStrikeState(this);
		_scorecont = scoreText.GetComponent<ScoreController>();
	}
	
	void Update() {
		currentState.Update();
		FillPrBar();
	}

	void FixedUpdate() {
		currentState.FixedUpdate();
	}

	void LateUpdate() {
		currentState.LateUpdate();
	}

	void FillPrBar() {
		float currentOffset = _distanse - MIN_DISTANCE;
		float maximumOffset = MAX_DISTANCE - MIN_DISTANCE;
		float fillAmount = currentOffset / maximumOffset;
		mask.fillAmount = fillAmount;
	}

	public void BallPocketed(int ballNumber) {
		currentPlayerContinuesToPlay = true;
		CurrentPlayer.Collect(ballNumber);
		_scorecont.ballUpdate(ballNumber);
	}

	public void NextPlayer() {
		_distanse = 0;
		if (currentPlayerContinuesToPlay) {
			currentPlayerContinuesToPlay = false;
			Debug.Log("Ход: " + CurrentPlayer.Name);
			return;
		}

		Debug.Log("Ход: " +  OtherPlayer.Name);
		var aux = CurrentPlayer;
		CurrentPlayer = OtherPlayer;
		OtherPlayer = aux;
	}

	public void EndMatch() {
		Player winner = null;
		if (CurrentPlayer.Points > OtherPlayer.Points)
			winner = CurrentPlayer; 
		else if (CurrentPlayer.Points < OtherPlayer.Points)
			winner = OtherPlayer;

		var msg = "Игра завершена\n";

		if (winner != null)
			msg += string.Format("Победила {0}", winner.Name);
		else
			msg += "Ничья, потому что.";

		var text = winMessage.GetComponentInChildren<UnityEngine.UI.Text>();
		text.text = msg;
		winMessage.GetComponent<Canvas>().enabled = true;
	}
}
