using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class DynamicPentagon : MonoBehaviour
{
	//頂点数
	private const int VerticesCount = 5;

	[SerializeField]
	private Material Material;


	//半径
	[SerializeField]
	private float Radius = 1f;

	[SerializeField]
	private List<float> Volumes;



	private MeshFilter filter;
	private MeshRenderer rend;
	List<Vector3> vertices = new List<Vector3> ();
	List<int> triangles = new List<int> ();
	Mesh mesh;

	private void Awake ()
	{
		this.filter = GetComponent<MeshFilter> ();
		this.rend = GetComponent<MeshRenderer> ();
		this.mesh = new Mesh ();
	}

	private void Start ()
	{
	}

	private void FixedUpdate ()
	{
		this.vertices.Clear ();
		this.triangles.Clear ();

		//原点座標
		vertices.Add (Vector3.zero);

		//各頂点座標
		for (int i = 1; i <= VerticesCount; i++) {
			float rad = (90f - (360f / (float)VerticesCount) * (i - 1)) * Mathf.Deg2Rad;
			float x = Mathf.Cos (rad) * this.Radius * this.Volumes [i - 1];
			float y = Mathf.Sin (rad) * this.Radius * this.Volumes [i - 1];
			vertices.Add (new Vector3 (x, y, 0));
			triangles.Add (0);
			triangles.Add (i);
			triangles.Add (i == VerticesCount ? 1 : i + 1);
		}

		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();

		filter.sharedMesh = mesh;
		rend.material = this.Material;
	}

}