using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class Circular_Gauge : MonoBehaviour
{
	private	Vector3[]	vertices;
	private	Vector2[]	uv;
	private	int[]		indices;

	private	Vector3[]	vertices2;
	private	Vector2[]	uv2;
	private	int[]		indices2;

	[Range(0,1)]
	public	float		fill = 0;
	private	float		prev_fill;

	private	bool		fill_on;
	private	bool		is_full;
	public bool Is_Full { get { return is_full; } }

	void Awake()
	{
		Exit_Gauge();
	}
	void Update()
	{
		if ( !fill_on ) return;

		if ( fill < 1.0f )	fill += .01f;
		else				is_full = true;

		if ( fill != prev_fill ) Rebuild_Mesh();
	}
	public void Enter_Gauge()
	{
		fill_on = true;
	}
	public void Exit_Gauge()
	{
		fill_on = false;
		is_full	= false;
		fill	= .0f;

		Rebuild_Mesh();
	}
	private void Rebuild_Mesh()
	{
		Mesh		mesh		= Build_QuadMesh();
		MeshFilter	meshFilter	= GetComponent<MeshFilter>();

		if ( meshFilter != null )
			meshFilter.mesh = mesh;

		prev_fill = fill;
	}
	private Mesh Build_QuadMesh()
	{
		vertices2	= new Vector3[10];
		uv2			= new Vector2[10];

		vertices = new Vector3[10]{new Vector3(.0f, .0f, .0f),
			new Vector3(.0f, .0f, .5f), new Vector3(.5f, .0f, .5f), new Vector3(.5f, .0f, .0f),
			new Vector3(.5f, .0f, -.5f), new Vector3(.0f, .0f, -.5f), new Vector3(-.5f, .0f, -.5f),
			new Vector3(-.5f, .0f, .0f), new Vector3(-.5f, .0f, .5f), new Vector3(.0f, .0f, .5f)};
		
		uv = new Vector2[10]{new Vector2(.5f, .5f),
			new Vector2(.5f, 1.0f), new Vector2(1.0f, 1.0f), new Vector2(1.0f, .5f),
			new Vector2(1.0f, .0f), new Vector2(.5f, .0f), new Vector2(.0f, .0f),
			new Vector2(.0f, .5f), new Vector2(.0f, 1.0f), new Vector2(.5f, 1.0f)};

		indices = new int[24]{0,1,2,0,2,3,0,3,4,0,4,5,0,5,6,0,6,7,0,7,8,0,8,9};

		Fill_QuadData();

		Mesh mesh = new Mesh();
		mesh.name = "Generated Mesh";

		mesh.vertices	= vertices2;
		mesh.triangles	= indices2;
		mesh.uv			= uv2;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		mesh.Optimize();

		return mesh;
	}
	private void Fill_QuadData()
	{
		Array.Copy(vertices, vertices2, 10);
		Array.Copy(uv, uv2, 10);

		fill = Mathf.Clamp01(fill);

		int tri_count = (int)(fill * 8.0f);

		// 잔여분이 있다면 버텍스 정보 수정
		if ( tri_count < fill * 8.0f )
		{
			float adp_fill = -fill + .25f;
			tri_count += 1;
			float nz = Mathf.Sin(adp_fill * Mathf.PI * 2) * .5f;
			float nx = Mathf.Cos(adp_fill * Mathf.PI * 2) * .5f;

			if ( Mathf.Abs(nz) > Mathf.Abs(nx) )
			{
				float nz_temp = Mathf.Sign(nz) * .5f;
				nx = nz_temp * nx / nz;
				nz = nz_temp;
			}
			else
			{
				float nx_temp = Mathf.Sign(nx) * .5f;
				nz = nx_temp * nz / nx;
				nx = nx_temp;
			}

			vertices2[tri_count+1].z = nz;
			vertices2[tri_count+1].x = nx;
			uv2[tri_count+1].x		 = nx + .5f;
			uv2[tri_count+1].y		 = nz + .5f;
		}

		indices2 = new int[3 * tri_count];
		Array.Copy(indices, indices2, 3 * tri_count);
	}
}

