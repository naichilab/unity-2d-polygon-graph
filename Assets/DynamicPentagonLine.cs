using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(LineRenderer))]
public class DynamicPentagonLine : MonoBehaviour
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

	[SerializeField]
	private float Width = 0.05f;

	private LineRenderer rend;

	private void Awake ()
	{
		this.rend = GetComponent<LineRenderer> ();
	}

	private void Start ()
	{
	}

	private void FixedUpdate ()
	{
		rend.material = this.Material;
		rend.startWidth = this.Width;
		rend.endWidth = this.Width;
		rend.useWorldSpace = false;
		rend.numPositions = VerticesCount + 2;

		//各頂点座標
		for (int i = 0; i <= VerticesCount + 1; i++) {
			float rad = (90f - (360f / (float)VerticesCount) * (i - 1)) * Mathf.Deg2Rad;
			float x = Mathf.Cos (rad) * this.Radius * this.Volume;
			float y = Mathf.Sin (rad) * this.Radius * this.Volume;

			rend.SetPosition (i, new Vector3 (x, y, 0));
		}
	}

}