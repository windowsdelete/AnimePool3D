using UnityEngine;
using System.Collections;

namespace GameStates {
	public class WaitingForStrikeState  : AbstractGameObjectState {
		private GameObject mmmmm;
		private GameObject mainBall;
		private GameObject mainCamera;

		private PoolGameController gameController;

		public WaitingForStrikeState(MonoBehaviour parent) : base(parent) { 
			gameController = (PoolGameController)parent;
			mmmmm = gameController.mmmmm;
			mainBall = gameController.mainBall;
			mainCamera = gameController.mainCamera;
			mmmmm.GetComponent<Renderer>().enabled = true;
		}

		public override void Update() {
			var x = Input.GetAxis("Horizontal");
			
			if (x != 0) {
				var angle = x * 75 * Time.deltaTime;
				gameController.strikeDirection = Quaternion.AngleAxis(angle, Vector3.up) * gameController.strikeDirection;
				mainCamera.transform.RotateAround(mainBall.transform.position, Vector3.up, angle);
				mmmmm.transform.RotateAround(mainBall.transform.position, Vector3.up, angle);
			}
			//Debug.DrawLine(mainBall.transform.position, mainBall.transform.position + gameController.strikeDirection * 10);
			if (Input.GetButtonDown("Fire1")) {
				gameController.currentState = new GameStates.StrikingState(gameController);
			}
		}
	}
}