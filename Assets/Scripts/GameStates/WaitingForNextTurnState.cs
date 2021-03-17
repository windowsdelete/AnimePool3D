using UnityEngine;
using System.Collections;

namespace GameStates {
	public class WaitingForNextTurnState : AbstractGameObjectState {
		private PoolGameController gameController;
		private GameObject mmmmm;
		private GameObject mainBall;
		private GameObject otherBalls;
		private GameObject mainCamera;

		private Vector3 cameraOffset;
		private Vector3 cueOffset;
		private Quaternion cameraRotation;
		private Quaternion cueRotation;

		public WaitingForNextTurnState(MonoBehaviour parent) : base(parent) {
			gameController = (PoolGameController)parent;

			mmmmm = gameController.mmmmm;
			mainBall = gameController.mainBall;
			otherBalls = gameController.otherBalls;
			mainCamera = gameController.mainCamera;
			
			cameraOffset = mainBall.transform.position - mainCamera.transform.position;
			cameraRotation = mainCamera.transform.rotation;
			cueOffset = mainBall.transform.position - mmmmm.transform.position;
			cueRotation = mmmmm.transform.rotation;
		}

		public override void FixedUpdate() {
			// Debug.Log(otherBalls.GetComponentsInChildren<Transform>().Length);
			if (otherBalls.GetComponentsInChildren<Transform>().Length == 1) {
				gameController.EndMatch();
			} else {
				var mainBallBody = mainBall.GetComponent<Rigidbody>();
				if (!(mainBallBody.IsSleeping() || mainBallBody.velocity == Vector3.zero))
					return;
				
				foreach (var rigidbody in otherBalls.GetComponentsInChildren<Rigidbody>()) {
					if (!(rigidbody.IsSleeping() || rigidbody.velocity == Vector3.zero))
						return;
				}

				gameController.NextPlayer();
				gameController.currentState = new WaitingForStrikeState(gameController);
			}
		}

		public override void LateUpdate() {
			mainCamera.transform.position = mainBall.transform.position - cameraOffset;
			mainCamera.transform.rotation = cameraRotation;
			
			mmmmm.transform.position = mainBall.transform.position - cueOffset;
			mmmmm.transform.rotation = cueRotation;
		}
	}
}