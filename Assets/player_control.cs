using UnityEngine;
using System.Collections;

public class player_control : MonoBehaviour {

	Camera main_cam;
	main_loop mainvar;
	RaycastHit hit;
	Ray ray;

	// Use this for initialization
	void Start () {
	 	main_cam = GetComponent<Camera>();
		mainvar = GameObject.Find("main_game").GetComponent<main_loop>();
	}
	
	// Update is called once per frame
	void Update () {
		
		ray = main_cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast(ray,out hit,100f)) {
				if (hit.collider.gameObject.tag == "cube") {
					GameObject selected_cube = hit.collider.gameObject;
					cube_property cp = selected_cube.GetComponent<cube_property>();
					mainvar.open_dfs(cp.xpos,cp.ypos,false);
					Debug.DrawLine(ray.origin, hit.point);
					//Debug.Log("HIT!");
				}
			}
		}

	}
}
