using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class DynamicPentagonWithColor : MonoBehaviour
{
	//頂点数
	private const int VerticesCount = 5;

	[SerializeField]
	private Material Material;


	//半径
	[SerializeField]
	private float Radius = 1f;

	//半径を１とした作図サイズの割合
	[SerializeField]
	[Range (0f, 1f)]
	private float Volume = 0.8f;

	private MeshFilter filter;
	private MeshRenderer rend;
	List<Vector3> vertices = new List<Vector3> ();
	List<int> triangles = new List<int> ();
	List<Color> colors = new List<Color> ();
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
		this.colors.Clear ();

		//原点座標
		vertices.Add (Vector3.zero);
		colors.Add (new Color (108f / 255f, 193f / 255f, 93f / 255f));//red

		//各頂点座標
		for (int i = 1; i <= VerticesCount; i++) {
			float rad = (90f - (360f / (float)VerticesCount) * (i - 1)) * Mathf.Deg2Rad;
			float x = Mathf.Cos (rad) * this.Radius * this.Volume;
			float y = Mathf.Sin (rad) * this.Radius * this.Volume;
			vertices.Add (new Vector3 (x, y, 0));
			colors.Add (new Color (108f / 255f, 193f / 255f, 93f / 255f));//red
			triangles.Add (0);
			triangles.Add (i);
			triangles.Add (i == VerticesCount ? 1 : i + 1);
		}

		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();
		mesh.colors = colors.ToArray ();

		filter.sharedMesh = mesh;
		rend.material = this.Material;
	}

}