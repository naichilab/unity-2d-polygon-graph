﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MeshRenderer))]
[RequireComponent (typeof(MeshFilter))]
public class DynamicCreateMesh : MonoBehaviour
{
	// 変更箇所 : Materialを保持するようにする
	[SerializeField]
	private Material _mat;

	private void Start ()
	{
		var mesh = new Mesh ();
		mesh.vertices = new Vector3[] {
			new Vector3 (0, 1f),
			new Vector3 (1f, -1f),
			new Vector3 (-1f, -1f),
		};
		mesh.triangles = new int[] {
			0, 1, 2 
		};

		var filter = GetComponent<MeshFilter> ();
		filter.sharedMesh = mesh;

		// 変更箇所 : MeshRendererからMaterialにアクセスし、Materialをセットするようにする
		var renderer = GetComponent<MeshRenderer> ();
		renderer.material = _mat;
	}
}