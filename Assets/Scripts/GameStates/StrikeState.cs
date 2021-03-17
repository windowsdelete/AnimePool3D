using UnityEngine;
using System;

namespace GameStates {
	public class StrikeState : AbstractGameObjectState {
		private PoolGameController gameController;
		
		private GameObject mmmmm;
		private GameObject mainBall;

		private float speed = 30f;
		private float force = 0f;
		
		public StrikeState(MonoBehaviour parent) : base(parent) { 
			gameController = (PoolGameController)parent;
			mmmmm = gameController.mmmmm;
			mainBall = gameController.mainBall;

			var forceAmplitude = gameController.maxForce - gameController.minForce;
			var relativeDistance = (Vector3.Distance(mmmmm.transform.position, mainBall.transform.position) - PoolGameController.MIN_DISTANCE) / (PoolGameController.MAX_DISTANCE - PoolGameController.MIN_DISTANCE);
			force = forceAmplitude * relativeDistance + gameController.minForce;
		}

		public override void FixedUpdate () {
			var distance = Vector3.Distance(mmmmm.transform.position, mainBall.transform.position);
			if (distance < PoolGameController.MIN_DISTANCE) {
				mainBall.GetComponent<Rigidbody>().AddForce(gameController.strikeDirection * force);
				mmmmm.GetComponent<Renderer>().enabled = false;
				mmmmm.transform.Translate(Vector3.down * speed * Time.fixedDeltaTime);
				gameController.currentState = new GameStates.WaitingForNextTurnState(gameController);
			} else {
				mmmmm.transform.Translate(Vector3.down * speed * -1 * Time.fixedDeltaTime);
			}
		}
	}
}