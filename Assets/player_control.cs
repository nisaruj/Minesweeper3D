using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;

public class player_control : MonoBehaviour {

	Camera main_cam;
	main_loop mainvar;
	RaycastHit hit;
	Ray ray;
	Grayscale grayscale;

	public void player_dead() {
		grayscale.enabled = true;
		GameObject player = GameObject.FindWithTag("Player");
		FirstPersonController fpscon = player.GetComponent<FirstPersonController>();
		fpscon.enabled = false;
	}

	// Use this for initialization
	void Start () {
		grayscale = GetComponent<Grayscale>();
	 	main_cam = GetComponent<Camera>();
		mainvar = GameObject.Find("main_game").GetComponent<main_loop>();
		grayscale.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		ray = main_cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast(ray,out hit,5f)) {
				if (hit.collider.gameObject.tag == "cube") {
					GameObject selected_cube = hit.collider.gameObject;
					cube_property cp = selected_cube.GetComponent<cube_property>();
					if (!mainvar.is_flag[cp.xpos,cp.ypos]) mainvar.open_dfs(cp.xpos,cp.ypos,false);
					Debug.DrawLine(ray.origin, hit.point);
					//Debug.Log("HIT!");
				}
			}
		}
		if (Input.GetMouseButtonDown(1)) {
			if (Physics.Raycast(ray,out hit,5f)) {
				if (hit.collider.gameObject.tag == "cube") {
					GameObject player = GameObject.FindWithTag("Player");
					GameObject selected_cube = hit.collider.gameObject;
					cube_property cp = selected_cube.GetComponent<cube_property>();
					mainvar.set_flag(cp.xpos,cp.ypos,hit.point,(float)player.transform.localRotation.eulerAngles.y);
					Debug.DrawLine(ray.origin, hit.point);
				}
			}
		}
	}
}
