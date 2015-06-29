using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	public Vector3 chunkSize;
	public GameObject mainCamera;
	public int sightRadius;
	private Grid<Chunk> grid = new Grid<Chunk> ();
	//for testing purpose
	private Chunk chunk;
	public Vector3 chunkPos;


	public int ChunkX{
		get{return Mathf.RoundToInt(chunkSize.x);}
	}

	public int ChunkY{
		get{return Mathf.RoundToInt(chunkSize.y);}
	}

	public int ChunkZ{
		get{return Mathf.RoundToInt(chunkSize.z);}
	}

	public Block GetBlock(int x, int y,int z){
		Chunk chunk = GetChunkWorldPos(x,y,z);
		if(chunk.generated){
			return chunk.GetBlockWorldPos(x,y,z);
		}
		return null;
	}

	public void SetBlock(Block block, int x, int y, int z){
		Chunk chunk = GetChunkWorldPos(x,y,z);
		if(chunk.generated){
			chunk.SetBlockWorldPos(block, x, y, z);
		}
	}

	public void DelBlock(int x, int y, int z){
		Chunk chunk = GetChunkWorldPos(x,y,z);
		if(chunk.generated){
			chunk.DelBlockWorldPos(x,y,z);
		}
	}

	public Chunk GetChunkWorldPos(int x, int y, int z){
		//这个func用的很多，要测试好
		if(x<0){
			x=(x+1)/ChunkX-1;
		}else{
			x=(x/ChunkX);
		}
		if(y<0){
			y=(y+1)/ChunkY-1;
		}else{
			y=(y/ChunkY);
		}
		if(z<0){
			z=(z+1)/ChunkZ-1;
		}else{
			z=(z/ChunkZ);
		}
		return GetChunk(x,y,z);
	}

	public Chunk GetChunk(int x, int y, int z){
		Chunk chunk = grid.SafeGet(x,y,z);
		if(chunk==null){
			chunk = new Chunk(this,x,y,z);
			grid.AddOrReplace(chunk,x,y,z);
		}
		return chunk;
	}

	private Chunk NearestEmptyChunk ()
	{
		Vector3 center = mainCamera.transform.position;
		Vector3? near = null;
		for(int x=(int)center.x-sightRadius;x<(int)center.x+sightRadius;x++){
			for(int y=(int)center.y-sightRadius;y<(int)center.y+sightRadius;y++){
			for(int z=(int)center.z-sightRadius;z<(int)center.z+sightRadius;z++){


				Chunk chunkT = GetChunkWorldPos(x,y,z);
				chunkT.Description();
				grid.Description();
				if(chunkT.generated)
					continue;
				Vector3 current = new Vector3 (x,y,z);
				float distance = (current-center).sqrMagnitude;
				if(distance > sightRadius * sightRadius)
					continue;
				if(!near.HasValue){
					near=current;
				}else{
					float _distance = (near.Value-center).sqrMagnitude;
					if(distance<_distance)
						near=current;
				}
		}}}
		if(near.HasValue){
			return GetChunkWorldPos(
			(int)near.Value.x,(int)near.Value.y,(int)near.Value.z
			);
		}else{
			return null;
		}

	}

	// Use this for initialization
	void Start () {
		int chunkPosX = Mathf.RoundToInt(chunkPos.x);
		int chunkPosY = Mathf.RoundToInt(chunkPos.y);
		int chunkPosZ = Mathf.RoundToInt(chunkPos.z);
		for(int x=-5;x<10;x++){
			GetChunk(x,0,0);
		}
				
		//chunk = new Chunk(this, chunkPosX,chunkPosY,chunkPosZ);
		//NearestEmptyChunk();
		//chunk.NeighboursReady();
	}

	// Update is called once per frame
	void Update () {

	}
}
