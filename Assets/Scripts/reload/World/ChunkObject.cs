using UnityEngine;
using System.Collections;

public class ChunkObject : MonoBehaviour {

	private World world;
	private Chunk chunk;
	private MeshFilter meshFilter;
	private MeshCollider meshCollider;
	private bool dirty;
	private float offset;
	private bool add=true;
	private int lastLevelY;

	public static ChunkObject Instance(World world, Chunk chunk){
		GameObject gameObject = new GameObject(
			"Chunk @ (" + chunk.WorldX + "," + chunk.WorldY + "," + chunk.WorldZ + ")"
		);
		gameObject.transform.parent = world.transform;
		gameObject.transform.position = new Vector3(chunk.WorldX, chunk.WorldY, chunk.WorldZ);
		gameObject.transform.rotation = Quaternion.identity;
		gameObject.AddComponent<MeshRenderer> ().sharedMaterials = world.materials;
		ChunkObject chunkObject = gameObject.AddComponent<ChunkObject>;
		chunkObject.Initialize(world, chunk, gameObject.AddComponent<MeshFilter>, gameObject.AddComponent<MeshCollider>());
		return chunkObject;
	}

	public void Initialize(World world, Chunk chunk, MeshFilter meshFilter, MeshCollider meshCollider){
		this.world = world;
		this.chunk = chunk;
		this.meshFilter = meshFilter;
		this.meshCollider = meshCollider;
		//Initialize 主要就是设置本地的一些变量。
	}

	// Update is called once per frame
	void Update () {
		if(chunk.NeighboursReady() && this.dirty){
			meshFilter.sharedMesh = RenderMesh();
			meshCollider.sharedMesh = null;
			meshCollider.sharedMesh = meshFilter.sharedMesh;
			this.dirty = false;
		}
	}

	private Mesh RenderMesh(){
		world.renderer.Render (world, chunk);
		return world.renderer.ToMesh (meshFilter.sharedMesh);
	}

	public void MakeDirty(){
		this.dirty =  true;
	}
}
