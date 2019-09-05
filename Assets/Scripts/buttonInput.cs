using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonInput : MonoBehaviour
{

	public bool left;

	public delegate void ButtonPressed();
	public static event ButtonPressed OnLeft;
	public static event ButtonPressed OnRight;

	private void OnMouseDown()
	{

		if (OnLeft != null && left)
		{
			OnLeft(); // triggar vi OnLeft Eventet

		}
		else if (OnRight != null)
		{
		 OnRight(); // triggar vi Onright eventet

		}

	}
}
