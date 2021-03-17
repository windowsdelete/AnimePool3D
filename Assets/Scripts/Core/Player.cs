using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player {
	private IList<Int32> ballsCount = new List<Int32>();

	public Player(string name) {
		Name = name;
	}

	public string Name {
		get;
		private set;
	}

	public int Points {
		get { return ballsCount.Count; }
	}

	public void Collect(int ballNumber) {
		Debug.Log(Name + " забила шар номер " + ballNumber);
		ballsCount.Add(ballNumber);
	}
}
