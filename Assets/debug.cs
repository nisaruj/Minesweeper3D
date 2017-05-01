using UnityEngine;
using System.Collections;

public class debug : MonoBehaviour {

	public int[] open_listx;
	public int[] open_listy;

	main_loop mainvar;

	void setopen() {
		for(int i=0;i<open_listx.Length;i++) mainvar.is_opened[open_listx[i],open_listy[i]] = true;
	}

	// Use this for initialization
	void Start () {
		mainvar = GameObject.Find("main_game").GetComponent<main_loop>();
	}
	
	// Update is called once per frame
	void Update () {
		setopen();
	}
}
