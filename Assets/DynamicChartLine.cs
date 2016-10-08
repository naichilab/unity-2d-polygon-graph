using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class DynamicChartLine : MonoBehaviour
{
	//頂点数
	[SerializeField]
	private int VerticesCount = 5;

	[SerializeField]
	private Material Material;

	//半径
	[SerializeField]
	private float Radius = 1f;

	[SerializeField]
	private int SeparateCount = 5;

	[SerializeField]
	private float LineWidth = 0.01f;

	private MeshFilter filter;
	private MeshRenderer rend;
	List<Vector3> vertices = new List<Vector3> ();
	List<int> triangles = new List<int> ();
	List<Color> colors = new List<Color> ();
	Mesh mesh;

	[SerializeField]
	GameObject sphere;

	[SerializeField]
	bool Debug = false;

	private void Awake ()
	{
		this.filter = GetComponent<MeshFilter> ();
		this.rend = GetComponent<MeshRenderer> ();
		this.mesh = new Mesh ();
	}

	private void Start ()
	{
		this.vertices.Clear ();
		this.triangles.Clear ();
		this.colors.Clear ();

		int currentVerticesCount = this.vertices.Count;

		//円周
		for (int n = 1; n <= SeparateCount; n++) {
			
			currentVerticesCount = this.vertices.Count;

			//各頂点座標
			for (int i = 0; i < VerticesCount; i++) {
				float deg = (360f / VerticesCount) * 0.5f;
				float offset = (this.LineWidth / Mathf.Cos (deg * Mathf.Deg2Rad)) / 2f;

				float rad = (90f - (360f / (float)VerticesCount) * i) * Mathf.Deg2Rad;
				float x1 = Mathf.Cos (rad) * (this.Radius * n * (1f / SeparateCount) - offset);
				float y1 = Mathf.Sin (rad) * (this.Radius * n * (1f / SeparateCount) - offset);
				float x2 = Mathf.Cos (rad) * (this.Radius * n * (1f / SeparateCount) + offset);
				float y2 = Mathf.Sin (rad) * (this.Radius * n * (1f / SeparateCount) + offset);
				vertices.Add (new Vector3 (x1, y1, 0));
				vertices.Add (new Vector3 (x2, y2, 0));
				if (this.Debug) {
					Instantiate (this.sphere, new Vector3 (x1, y1, 0), Quaternion.identity);
					Instantiate (this.sphere, new Vector3 (x2, y2, 0), Quaternion.identity);
				}

				float c = 1f;

				colors.Add (new Color (c, c, c));
				colors.Add (new Color (c, c, c));

				triangles.Add ((((i + 0) * 2) + 0) % (VerticesCount * 2) + currentVerticesCount);	//0
				triangles.Add ((((i + 0) * 2) + 1) % (VerticesCount * 2) + currentVerticesCount);	//1
				triangles.Add ((((i + 1) * 2) + 0) % (VerticesCount * 2) + currentVerticesCount);	//2

				triangles.Add ((((i + 1) * 2) + 0) % (VerticesCount * 2) + currentVerticesCount);	//2
				triangles.Add ((((i + 0) * 2) + 1) % (VerticesCount * 2) + currentVerticesCount);	//1
				triangles.Add ((((i + 1) * 2) + 1) % (VerticesCount * 2) + currentVerticesCount);	//3
			}
		}

		currentVerticesCount = this.vertices.Count;

		//軸
		for (int i = 0; i < VerticesCount; i++) {
			float halfWidthDeg = 90 * this.LineWidth / (Mathf.PI * this.Radius);

			float rad1 = (90f - halfWidthDeg - (360f / (float)VerticesCount) * i) * Mathf.Deg2Rad;
			float rad2 = (90f + halfWidthDeg - (360f / (float)VerticesCount) * i) * Mathf.Deg2Rad;
			float x1 = Mathf.Cos (rad1) * this.Radius;
			float y1 = Mathf.Sin (rad1) * this.Radius;
			float x2 = Mathf.Cos (rad2) * this.Radius;
			float y2 = Mathf.Sin (rad2) * this.Radius;
			vertices.Add (new Vector3 ((x1 - x2) / 2f, (y1 - y2) / 2f, 0));
			vertices.Add (new Vector3 ((x2 - x1) / 2f, (y2 - y1) / 2f, 0));
			vertices.Add (new Vector3 (x1, y1, 0));
			vertices.Add (new Vector3 (x2, y2, 0));
			if (this.Debug) {
				Instantiate (this.sphere, new Vector3 ((x1 - x2) / 2f, (y1 - y2) / 2f, 0), Quaternion.identity);
				Instantiate (this.sphere, new Vector3 ((x2 - x1) / 2f, (y2 - y1) / 2f, 0), Quaternion.identity);
				Instantiate (this.sphere, new Vector3 (x1, y1, 0), Quaternion.identity);
				Instantiate (this.sphere, new Vector3 (x2, y2, 0), Quaternion.identity);
			}

			colors.Add (new Color (1f, 1f, 1f));
			colors.Add (new Color (1f, 1f, 1f));
			colors.Add (new Color (1f, 1f, 1f));
			colors.Add (new Color (1f, 1f, 1f));

			triangles.Add (((i * 4) + 0) + currentVerticesCount);	//0
			triangles.Add (((i * 4) + 3) + currentVerticesCount);	//3
			triangles.Add (((i * 4) + 2) + currentVerticesCount);	//2
			triangles.Add (((i * 4) + 0) + currentVerticesCount);	//0
			triangles.Add (((i * 4) + 1) + currentVerticesCount);	//1
			triangles.Add (((i * 4) + 3) + currentVerticesCount);	//3
		}


		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();
		mesh.colors = colors.ToArray ();

		filter.sharedMesh = mesh;
		rend.material = this.Material;
	}

	private void FixedUpdate ()
	{

	}
}