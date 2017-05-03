using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class flagcount : MonoBehaviour {

	main_loop mainvar;

	// Use this for initialization
	void Start () {
		mainvar = GameObject.Find("main_game").GetComponent<main_loop>();
	}
	
	// Update is called once per frame
	void Update () {
		Text count = GetComponent<Text>();
		count.text = "x" + mainvar.flag_count.ToString();
	}
}
