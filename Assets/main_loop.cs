﻿using UnityEngine;
using System.Collections;

public class main_loop : MonoBehaviour {

	public Vector3 spawn;
	public Vector3 start_pos;
	public Vector3 size;
	public GameObject cube;
	public GameObject Player;
	public int board_width=30,board_height=30;
	public int bombs=15;

	int[,] main_board = new int[100,100];
	int[,] dir = {{-1,-1} , {0,-1} , {1,-1} , {-1,0} , {1,0} , {-1,1} , {0,1} , {1,1}};
	bool[,] is_bomb = new bool[100,100];
	public bool[,] is_opened = new bool[100,100];
	GameObject newcube;
	
	void board_gen () {
		//Generate bombs
		int count = 0,xpos,ypos;
		for (int i=0;i<board_height;i++) {
			for(int j=0;j<board_width;j++) {
				is_bomb[i,j] = false;
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
		size = cube.transform.localScale;
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

	void spawn_player() {
		spawn = new Vector3((start_pos.x+board_width*size.x)/2,start_pos.y+10,(start_pos.z+board_height*size.z)/2);
		Instantiate(Player,spawn,Quaternion.identity);
	}

	public void open_dfs(int x,int y,bool pass) {
		if (x < 0 || x >= board_height || y < 0 || y >= board_width) return;
		if (is_opened[x,y] || pass) return;
		if (main_board[x,y] > 0) pass = true;
		is_opened[x,y] = true;
		open_dfs(x+1,y,pass);
		open_dfs(x-1,y,pass);
		open_dfs(x,y+1,pass);
		open_dfs(x,y-1,pass);
	}

	// Use this for initialization
	void Start () {
		board_gen();
		cube_gen();
		spawn_player();
	}
	
	// Update is called once per frame
	void Update () {

	}
}

