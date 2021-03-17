using UnityEngine;
using System.Collections;

namespace GameStates {
	public class StrikingState : AbstractGameObjectState {
		private PoolGameController gameController;

		private GameObject mmmmm;
		private GameObject mainBall;

		private float cueDirection = -1;
		private float speed = 7;

		public StrikingState(MonoBehaviour parent) : base(parent) { 
			gameController = (PoolGameController)parent;
			mmmmm = gameController.mmmmm;
			mainBall = gameController.mainBall;
		}

		public override void Update() {
			if (Input.GetButtonUp("Fire1")) {
				gameController.currentState = new GameStates.StrikeState(gameController);
			}
		}

		public override void FixedUpdate () {
			var distance = Vector3.Distance(mmmmm.transform.position, mainBall.transform.position);
			if (distance < PoolGameController.MIN_DISTANCE || distance > PoolGameController.MAX_DISTANCE)
				cueDirection *= -1;
			mmmmm.transform.Translate(Vector3.down * speed * cueDirection * Time.fixedDeltaTime);
			// Debug.Log(distance);
			gameController._distanse = distance;
		}
	}
}