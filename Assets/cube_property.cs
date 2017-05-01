using UnityEngine;
using System.Collections;

public class cube_property : MonoBehaviour {

	public int num = 0; //9 for bomb
	public Texture closed;
	public Texture[] cube_texture = new Texture[15];
	public int xpos,ypos;
	
	main_loop mainvar;

	// Use this for initialization
	void Start () {
		mainvar = GameObject.Find("main_game").GetComponent<main_loop>();
		GetComponent<Renderer>().material.mainTexture = closed;
	}
	
	// Update is called once per frame
	void Update () {
		if (mainvar.is_opened[xpos,ypos]) GetComponent<Renderer>().material.mainTexture = cube_texture[num];
	}
}
