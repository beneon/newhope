using UnityEngine;
using System.Collections;

public class OverviewCamera : MonoBehaviour 
{
	public bool mouseScroll;
	public bool keyboardScroll;
	private int scrollDistance = 5;
	private float scrollSpeed = 20;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float mousePosX = Input.mousePosition.x;
		float mousePosY = Input.mousePosition.y;
		float vertical = Input.GetAxis ("Vertical")*scrollSpeed;
		float horizontal = Input.GetAxis ("Horizontal") * scrollSpeed;

		if(keyboardScroll){
			Vector3 speed = new Vector3 (horizontal,0f,vertical);
			transform.position=transform.position + (speed * Time.deltaTime);
		}
		if(mouseScroll){
			if(mousePosX < scrollDistance) {
				transform.Translate (Vector3.left * scrollSpeed * Time.deltaTime);
			}
			if(mousePosX > (Screen.width - scrollDistance) && mousePosX >= scrollDistance){
				transform.Translate (Vector3.right * scrollSpeed * Time.deltaTime);
			}
			if(mousePosY < scrollDistance){
				transform.Translate (Vector3.down * scrollSpeed * Time.deltaTime);

			}
			if(mousePosY > (Screen.height - scrollDistance) && mousePosY >= scrollDistance){
				transform.Translate (Vector3.up * scrollSpeed * Time.deltaTime);
			}
		}
	}
}
//overview camera unit testing done 0528
//这里与原来的文件相比，把mousescroll的transform.back修改为Vector3.down and up
//原来的代码应该是弄错了
