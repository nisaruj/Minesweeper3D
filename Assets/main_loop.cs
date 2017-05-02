using UnityEngine;
using System.Collections;

public class main_loop : MonoBehaviour {

	public Vector3 spawn;
	public Vector3 start_pos;
	public Vector3 size;
	public GameObject cube;
	public GameObject Player;
	public GameObject wall;
	public GameObject flag;
	public int board_width=30,board_height=30;
	public int bombs=15;

	int[,] main_board = new int[100,100];
	int[,] dir = {{-1,-1} , {0,-1} , {1,-1} , {-1,0} , {1,0} , {-1,1} , {0,1} , {1,1}};
	bool[,] is_bomb = new bool[100,100];
	public bool[,] is_flag = new bool[100,100];
	public bool[,] is_opened = new bool[100,100];
	GameObject newcube;
	
	void board_gen () {
		//Generate bombs
		int count = 0,xpos,ypos;
		for (int i=0;i<board_height;i++) {
			for(int j=0;j<board_width;j++) {
				is_bomb[i,j] = false;
				is_flag[i,j] = false;
				main_board[i,j] = 0;
			}
		}
		while(count < bombs) {
			xpos = Random.Range(0,board_width);
			ypos = Random.Range(0,board_height);
			if (is_bomb[xpos,ypos]) continue;
			is_bomb[xpos,ypos] = true;
			main_board[xpos,ypos] = 9;
			count++;
		}
	
		for(int i=0;i<board_height;i++) {
			for(int j=0;j<board_width;j++) {
				if (is_bomb[i,j]) {
					//Debug.Log(i.ToString() + ',' + j.ToString());
					for(int k=0;k<8;k++) {
						if (i+dir[k,0] >= 0 && i+dir[k,0] < board_height && j+dir[k,1] >= 0 && j+dir[k,1] < board_width && !is_bomb[i+dir[k,0],j+dir[k,1]]) {
							main_board[i+dir[k,0],j+dir[k,1]]++;
						}
					}
				}
			}
		}
	}

	void cube_gen() {
		for (int i=0;i<board_height;i++) {
			for(int j=0;j<board_width;j++) {
				newcube = (GameObject)Instantiate(cube,new Vector3(start_pos.x+i*size.x,start_pos.y,start_pos.z+j*size.z),Quaternion.identity);
				newcube.name = i.ToString() + ',' + j.ToString();
				cube_property prop = newcube.GetComponent<cube_property>();
				prop.num = main_board[i,j];
				prop.xpos = i;
				prop.ypos = j;
			}
		}
	}

	void wall_gen() {
		GameObject newwall;
		newwall = (GameObject)Instantiate(wall,new Vector3((float)start_pos.x+((float)board_height*size.x/2)-(float)size.x/2,start_pos.y,start_pos.z-size.z),Quaternion.identity);
		newwall.transform.localScale = new Vector3(size.x*board_height,100,size.z);
		newwall = (GameObject)Instantiate(wall,new Vector3((float)start_pos.x+((float)board_height*size.x/2)-(float)size.x/2,start_pos.y,(float)start_pos.z+(float)size.z*board_width),Quaternion.identity);
		newwall.transform.localScale = new Vector3(size.x*board_height,100,size.z);
		newwall = (GameObject)Instantiate(wall,new Vector3(start_pos.x-size.x,start_pos.y,(float)board_width*size.z/2-(float)size.z/2),Quaternion.identity);
		newwall.transform.localScale = new Vector3(size.x,100,size.z*board_width);
		newwall = (GameObject)Instantiate(wall,new Vector3((float)start_pos.x+(float)size.x*board_height,start_pos.y,(float)board_width*size.z/2-(float)size.z/2),Quaternion.identity);
		newwall.transform.localScale = new Vector3(size.x,100,size.z*board_width);
	}

	void spawn_player() {
		spawn = new Vector3((start_pos.x+board_width*size.x)/2,start_pos.y+10,(start_pos.z+board_height*size.z)/2);
		Instantiate(Player,spawn,Quaternion.Euler(new Vector3(0, 180, 0)));
	}

	public void open_dfs(int x,int y,bool pass) {
		if (x < 0 || x >= board_height || y < 0 || y >= board_width) return;
		if (is_opened[x,y] || pass) return;
		if (is_flag[x,y]) return;
		if (main_board[x,y] > 0) pass = true;
		is_opened[x,y] = true;
		open_dfs(x+1,y,pass);
		open_dfs(x-1,y,pass);
		open_dfs(x,y+1,pass);
		open_dfs(x,y-1,pass);
	}

	public void set_flag(int x,int y,Vector3 flagpos,float flagrot) {
		GameObject current_flag;
		if (!is_opened[x,y]) {
			if (!is_flag[x,y]) {
				//Debug.Log(flagrot.ToString());
				current_flag = (GameObject)Instantiate(flag,flagpos,Quaternion.identity);
				current_flag.name = "flag " + x.ToString() + ',' + y.ToString();
				current_flag.transform.rotation = Quaternion.Euler(new Vector3(0,90+flagrot,0));
			} else {
				current_flag = GameObject.Find("flag " + x.ToString() + ',' + y.ToString());
				Destroy(current_flag);
			}
			is_flag[x,y] = !is_flag[x,y];
		}
	}

	void setMainlight() {
		GameObject main_light = GameObject.Find("main_light");
		main_light.transform.position = new Vector3((float)start_pos.x+((float)board_height*size.x/2),100,(float)board_width*size.z/2-(float)size.z/2);
	}

	// Use this for initialization
	void Start () {
		size = cube.transform.localScale;
		board_gen();
		cube_gen();
		wall_gen();
		setMainlight();
		spawn_player();
	}
	
	// Update is called once per frame
	void Update () {

	}
}

