using UnityEngine;
using System.Collections;

public class Grid<T>
{
	private T[,,] grid;
	private int minX,minY,minZ;
	private int maxX,maxY,maxZ;

	public Grid()
	{
		grid = new T[0,0,0];
	}

	public void Description(){
		Debug.Log("the grid now is ranged in X "+minX+","+maxX
							+"in Y "+minY+","+maxY
							+"in Z "+minZ+","+maxZ);
	}

	public void Set (T obj, int x, int y,int z)
	{
		grid[z-minZ,y-minY,x-minX]=obj;
	}

	public T Get (int x,int y,int z)
	{
		return grid[z-minZ,y-minY,x-minX];
	}

	public T SafeGet(int x,int y,int z)
	{
		if(!IsCorrectIndex(x,y,z))
		{
			Debug.Log("index not right! the index is "+x+","+y+","+z);
			return default(T);
		}
		return grid[z-minZ,y-minY,x-minX];
	}

	public void AddOrReplace (T obj, int x,int y,int z)
	{
		Debug.Log(x+","+y+","+z);
		int dMinX=0,dMinY=0,dMinZ=0;
		int dMaxX=0,dMaxY=0,dMaxZ=0;
		if(x<minX)
		{
			dMinX=x-minX;
		}
		if(y<minY)
		{
			dMinY=y-minY;
		}
		if(z<minZ)
		{
			dMinZ=z-minZ;
		}
		if(x>maxX)
		{
			dMaxX=x-maxX+1;
		}else if(x==0 && maxX==0){
			dMaxX=1;
		}
		if(y>maxY)
		{
			dMaxY=y-maxY+1;
		}else if(y==0 && maxY==0){
			dMaxY=1;
		}
		if(z>maxZ)
		{
			dMaxZ=z-maxZ+1;
		}else if(z==0 && maxZ==0){
			dMaxZ=1;
		}

		if(dMinX!=0 || dMinZ!=0 || dMinZ!=0 ||
			dMaxX != 0 || dMaxY!=0|| dMaxZ != 0){
			Increase (dMinX,dMinY,dMinZ,
						dMaxX,dMaxY,dMaxZ);
		}
		grid[z-minZ,y-minY,x-minX]=obj;
	}

	private void Increase (int dMinX, int dMinY, int dMinZ,
							int dMaxX, int dMaxY, int dMaxZ)
	{
		int oldMinX = minX;
		int oldMinY = minY;
		int oldMinZ = minZ;

		int oldMaxX = maxX;
		int oldMaxY = maxY;
		int oldMaxZ = maxZ;

		T[,,] oldGrid = grid;

		minX += dMinX;
		minY += dMinY;
		minZ += dMinZ;

		maxX += dMaxX;
		maxY += dMaxY;
		maxZ += dMaxZ;

		int sizeX = maxX -minX;
		int sizeY = maxY -minY;
		int sizeZ = maxZ -minZ;

		grid = new T[sizeZ,sizeY,sizeX];

		for(int z=oldMinZ; z<oldMaxZ; z++){
			for(int y=oldMinY; y<oldMaxY; y++){
				for(int x=oldMinX; x<oldMaxX; x++){
					grid[z-minZ,y-minY,x-minX] = oldGrid[z-oldMinZ, y-oldMinY, x-oldMinX];
				}
			}
		}

	}

	public bool IsCorrectIndex (int x, int y, int z)
	{
		if(x<minX || y<minY || z<minZ)
			return false;
		if(x>maxX || y>maxY || z>maxZ)
			return false;
		return true;
	}

	public int GetMinX ()
	{
		return minX;
	}

	public int GetMinY ()
	{
		return minY;
	}

	public int GetMinZ ()
	{
		return minZ;
	}

	public int GetMaxX ()
	{
		return maxX;
	}

	public int GetMaxY ()
	{
		return maxY;
	}

	public int GetMaxZ ()
	{
		return maxZ;
	}
	public int GetSizeX()
	{
		return (maxX-minX);
	}
	public int GetSizeY()
	{
		return (maxY-minY);
	}
	public int GetSizeZ()
	{
		return (maxZ-minZ);
	}

		//min,max初始值始终都是0，如果没有特别扩充，那么就一直都会是0，注意一下这里！
		//所以扩充也最好把原点包括进去？
		//不过正常来说也都是从0开始的

}
