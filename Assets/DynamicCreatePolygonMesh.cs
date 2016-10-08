using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(MeshRenderer))]
[RequireComponent (typeof(MeshFilter))]
public class DynamicCreatePolygonMesh : MonoBehaviour
{
	[SerializeField]
	private Material _PolygonMaterial;

	[SerializeField]
	private Material _LineMaterial;

	//頂点数
	[SerializeField]
	private int VerticesCount = 5;

	//半径
	[SerializeField]
	private float Radius = 1f;

	private void Start ()
	{
		if (VerticesCount < 3) {
			Debug.LogError ("頂点数は３以上を指定してください。");
			return;
		}


		CreatePolygon ();
		CreateLine ();
	}

	void CreatePolygon ()
	{
		List<Vector3> vertices = new List<Vector3> ();
		List<int> triangles = new List<int> ();

		//原点座標
		vertices.Add (Vector3.zero);

		//各頂点座標
		for (int i = 1; i <= this.VerticesCount; i++) {
			float rad = (90f - (360f / (float)this.VerticesCount) * (i - 1)) * Mathf.Deg2Rad;
			float x = Mathf.Cos (rad);
			float y = Mathf.Sin (rad);
			vertices.Add (new Vector3 (x, y, 0));
			triangles.Add (0);
			triangles.Add (i);
			triangles.Add (i == this.VerticesCount ? 1 : i + 1);
		}

		var mesh = new Mesh ();
		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();

		var filter = GetComponent<MeshFilter> ();
		filter.sharedMesh = mesh;

		var renderer = GetComponent<MeshRenderer> ();
		renderer.material = _PolygonMaterial;
	}

	void CreateLine ()
	{
		List<Vector3> vertices = new List<Vector3> ();

		var renderer = GetComponent<LineRenderer> ();
		renderer.material = _LineMaterial;
		renderer.startWidth = 0.1f;
		renderer.endWidth = 0.1f;
		renderer.useWorldSpace = false;

		renderer.numPositions = this.VerticesCount + 1;

		//各頂点座標
		for (int i = 0; i <= this.VerticesCount; i++) {
			float rad = (90f - (360f / (float)this.VerticesCount) * (i - 1)) * Mathf.Deg2Rad;
			float x = Mathf.Cos (rad);
			float y = Mathf.Sin (rad);

			renderer.SetPosition (i, new Vector3 (x, y, 0));
		}
	}

}