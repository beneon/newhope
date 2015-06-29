using UnityEngine;
using System.Collections;

public class ProfilerTesting : MonoBehaviour {
	private Profiler pftest=new Profiler();

	// Use this for initialization
	void Start () {
		pftest.Start("ProfilerTesting");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Time.deltaTime);
		if(Time.fixedTime > 4f)
		{
			//pftest.Stop();
		}
	}

	void FixedUpdate ()
	{
		Debug.Log(Time.deltaTime);
	}

}
//测试profiler用的