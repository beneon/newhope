using UnityEngine;
using System.Collections;

public class SelectionTest : MonoBehaviour {
	public GameObject selection;
	private Vector3? start = null;
	private GameObject gameObject = null;

	private Vector3? MouseToWorld (Vector3 v)
	{
		Vector3? s = null;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (v);
		if(Physics.Raycast(ray, out hit, 99f)){
			s = hit.point - hit.normal / 2;
		}
		return s;
	}

	private struct SelectionArea
	{
		public int startX, startZ, endX, endZ;
		public SelectionArea (float sx, float sz, float ex, float ez)
		{
			if(sx > ex){
				endX = Mathf.FloorToInt (sx);
				startX = Mathf.FloorToInt (ex);
			}else{
				endX = Mathf.FloorToInt (ex);
				startX = Mathf.FloorToInt (sx);
			}
			if(sz>ez){
				endZ = Mathf.CeilToInt(sz);
				startZ = Mathf.CeilToInt(ez);
			}else{
				endZ = Mathf.CeilToInt(ez);
				startZ=Mathf.CeilToInt(sz);
			}
		}
	}
	

	void Update()
	{
		if(gameObject != null)
			Destroy(gameObject);
		if(Input.GetMouseButtonDown(0)){
			//GetMouseButtonDown(0) means the left key was pressed
			start = MouseToWorld (Input.mousePosition);
			//Input.mousePosition回复的是一个vector3
		}
		if(Input.GetMouseButton(0)){
			//GetMouseButton是按下不放，GetMouseButtonDown是仅仅在按下的那一桢才true，
			//除非松开再按，否则一直还是false
			gameObject = new GameObject (
				"Selection @" + Mathf.RoundToInt (start.Value.x) 
				+ ", " + Mathf.RoundToInt (start.Value.z)
				);
			if(start.HasValue){
				Vector3? end = MouseToWorld(Input.mousePosition);
				if(end.HasValue){
					SelectionArea selectionArea = new SelectionArea (
						start.Value.x, start.Value.z, end.Value.x, end.Value.z
						);
					for (int x=selectionArea.startX; x<= selectionArea.endX; x++){
						for(int z = selectionArea.startZ; z<= selectionArea.endZ;z++){
							GameObject o = (GameObject)Instantiate (
								selection, 
								new Vector3 (x, Mathf.Floor (start.Value.y),z),
								Quaternion.identity
								);
							o.transform.parent = gameObject.transform;
						}
					}
				}
			}
		}

	}

}
//疑问1：为什么要在update里面不断删除建立的gameobject？是否是为了防止生成过多？
//疑问2：鼠标mousetoworld这个function基础就是ScreenPointToRay,不过如果鼠标跑到外面去了怎么办？
//疑问3：鼠标只要没有点到物体上面那么呼叫mousetoworld肯定就会返回null，然后接下来也会陆续出null，然后就会报错。不知道会不会有影响
//这个script不用也罢，但是要知道有这些问题。