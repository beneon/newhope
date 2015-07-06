using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	public Vector3 chunkSize;
	public GameObject mainCamera;
	public int sightRadius;
	//public IRenderer renderer = new MarchRenderer();
	public Material[] materials;
	private Grid<Chunk> grid = new Grid<Chunk> ();
	private Chunk chunk;

	// Use this for initialization
	void Start () {
		chunk = GetChunk(0,0,0);
		Chunk chunk2 = GetChunk(1,0,0);
		chunk2.generated = true;
		chunk.SetBlock(new Dirt(),3,3,3);
		Debug.Log(Corner(4,3,3));
	}
	private bool Corner(int x, int y, int z){
		if(x >= 0 && x < ChunkX && y >= 0 && y < ChunkY && z >= 0 && z < ChunkZ){
			if(chunk.GetBlock(x,y,z) != null){
				//Debug.Log("inChunk, block filled");
				return false;
			}else{
				//Debug.Log("inChunk, block is null");
				return true;
			}
		}else{
			Chunk neighbourChunk = null;
			int ix = chunk.X, iy = chunk.Y, iz = chunk.Z;
			if(x<0)
			ix--;
			if(y<0)
			iy--;
			if(z<0)
			iz--;
			if(x>=ChunkX)
			ix++;
			if(y>=ChunkY)
			iy++;
			if(z>=ChunkZ)
			iz++;
			neighbourChunk = GetChunk(ix,iy,iz);
			if(neighbourChunk.generated){

				if(neighbourChunk.GetBlockWorldPos(x,y,z)!=null){
					//Debug.Log("not in Chunk, chunk generated, block filled");
					return false;
				}else{
					//Debug.Log("not in Chunk, chunk generated, block is null");
					return true;
				}
			}else{
				//Debug.Log("not in Chunk, chunk not generated");
				return false;
			}
		}
	}
	// Update is called once per frame
	void Update () {

	}

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
				if(GetChunkWorldPos(x,y,z).generated)
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
				GetChunkWorldPos(x,y,z).Description();
				Debug.Log(near.Value);
		}}}
		if(near.HasValue){
			Debug.Log(near.Value);
			return GetChunkWorldPos(
			(int)near.Value.x,(int)near.Value.y,(int)near.Value.z
			);
		}else{
			return null;
		}

	}

	public void UpdateBlockWorldPos(int x, int y, int z){
		if(GetBlock(x+1,y,z)!=null){
			GetBlock(x+1,y,z).Update(this,x,y,z);
		}
		if(GetBlock(x,y+1,z)!=null){
			GetBlock(x,y+1,z).Update(this,x,y,z);
		}
		if(GetBlock(x,y,z+1)!=null){
			GetBlock(x,y,z+1).Update(this,x,y,z);
		}
		if(GetBlock(x-1,y,z)!=null){
			GetBlock(x-1,y,z).Update(this,x,y,z);
		}
		if(GetBlock(x,y-1,z)!=null){
			GetBlock(x,y-1,z).Update(this,x,y,z);
		}
		if(GetBlock(x,y,z-1)!=null){
			GetBlock(x,y,z-1).Update(this,x,y,z);
		}
		//这里应该是告诉周围六个相邻体素该体素出现了新的状况，应该相应进行调整了
	}

	public void RefreshChunkWorldPos(int x, int y, int z){
		// 这一块等到chunk的object弄好了再说
	}


}
