using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {
	public float movementSpeed = 4.5f;
	public float mouseSensitivity = 5.0f;
	public float jumpSpeed = 8.0f;
	public float upDownRange = 45.0f;
	protected float rotLeftRight = 0;
	protected float verticalRotation = 0;
	protected float verticalVelocity = 0;
	protected CharacterController characterController;
	protected bool gravity = false;
	protected bool running = false;
	protected Vector3 speed;
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController> ();

	}
	void OnGUI () {
		GUILayout.Label("mouse x: "+ Input.GetAxis("Mouse X")+"rotLeftRight: "+rotLeftRight+" this.rotation "+transform.rotation+"this.speed: "+speed);
	}
	// Update is called once per frame
	void Update () {
		rotLeftRight += Input.GetAxis ("Mouse X") * mouseSensitivity;
		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		transform.rotation=Quaternion.Euler(0,rotLeftRight,0);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation,0,0);
		//旋转果然还是用Quaternion.Euler比较好

		if (running){
			forwardSpeed = forwardSpeed * 2f;
		}

		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

		if (Input.GetKey (KeyCode.LeftShift)){
			running = true;
		}else{
			running = false;
		}


		if (gravity){
			verticalVelocity += Physics.gravity.y * Time.deltaTime * movementSpeed /4;
		}

		if(characterController.isGrounded && Input.GetButton ("Jump")){
			verticalVelocity = jumpSpeed;
		}

		speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);

		speed = transform.rotation * speed;
		//理解成fps里面边跑边转就是绕一个点跑？
		//这一行代码倒是很重要，如果去掉的话那么就会没有局部指向了（向前始终是向着world坐标系的前）

		characterController.Move (speed * Time.deltaTime);
	}
}
