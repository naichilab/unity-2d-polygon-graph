using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		LineRenderer r = this.GetComponent<LineRenderer> ();
		r.SetWidth (3f, 3f);
		r.numPositions = 2;
		r.SetPosition (0, Vector3.zero);
		r.SetPosition (1, new Vector3 (1f, 1f, 0f));
	}
}
