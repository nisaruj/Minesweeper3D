using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class time_counter : MonoBehaviour {

	public bool is_enabled = true;
	float timer = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (is_enabled) timer += Time.deltaTime;
		Text counter = GetComponent<Text>();
		counter.text = ((int)timer).ToString();
	}
}
