using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestRenderer:IRenderer{
  private World world;
  private Chunk chunk;
  private List<Vector3> vertices;
  private List<int> triangles;
  private List<Vector2> uvs;
  private Vector3[] positions;
  private bool[] corners;

  public void Initialize()
  {
    this.vertices = new List<Vector3> ();
    this.triangles = new List<int> ();
    this.uvs = new List<Vector2> ();
    this.corners = new bool[8];
    this.positions = new Vector3[8];
  }

  public Mesh ToMesh(Mesh mesh)
  {
    if(mesh == null){
      mesh = new Mesh();
    }
    mesh.Clear();
    mesh.vertices = vertices.ToArray();
    mesh.triangles = triangles.ToArray();
    mesh.uv = uvs.ToArray();
    mesh.RecalculateNormals();
    mesh.RecalculateBounds();
    return mesh;
  }

  public void Render (World world, Chunk chunk)
  {
    this.world = world;
    this.chunk = chunk;
  }
}
