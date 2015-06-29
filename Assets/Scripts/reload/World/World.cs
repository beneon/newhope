using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	public Vector3 chunkSize;
	public Grid<Chunk> grid = new Grid<Chunk> ();
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
	// Use this for initialization
	void Start () {
		int chunkPosX = Mathf.RoundToInt(chunkPos.x);
		int chunkPosY = Mathf.RoundToInt(chunkPos.y);
		int chunkPosZ = Mathf.RoundToInt(chunkPos.z);
		chunk = new Chunk(this, chunkPosX, chunkPosY, chunkPosZ);

		for(int x=0;x<ChunkX;x++){
		for(int y=0;y<ChunkY;y++){
		for(int z=0;z<ChunkZ;z++){
			chunk.SetBlock(new Dirt(),x,y,z);
		}
		}
		}
	
		for(int x=ChunkX;x<(2*ChunkX-1);x++){
		for(int y=ChunkY;y<(2*ChunkY-1);y++){
		for(int z=ChunkZ;z<(2*ChunkZ-1);z++){
			chunk.DelBlockWorldPos(x,y,z);
		}
		}
		}
		for(int x=0;x<ChunkX;x++){
			for(int y=0;y<ChunkY;y++){
				for(int z=0;z<ChunkZ;z++){
					if(chunk.GetBlockWorldPos(x,y,z)!=null)
						Debug.Log(x+","+y+","+z+"UV: "+chunk.GetBlockWorldPos(x,y,z).UV);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
