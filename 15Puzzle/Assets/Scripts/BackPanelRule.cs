using UnityEngine;
using System.Collections;

public class BackPanelRule : MonoBehaviour {

	public bool flag;

	// Use this for initialization
	void Start ()
	{
		flag = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D()
	{
		flag = true;
//		this.GetComponent<BoxCollider2D> ().enabled = false;
	}

	void OnCollisionStay2D()
	{
		Debug.Log ("無効！");
	}
	
}
