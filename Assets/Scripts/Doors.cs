using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

	public GameObject toggleObject;



	private bool doTheThing;

	void Start()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			toggleObject.SetActive(false);
		}
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
			toggleObject.SetActive(true);
        }
    }
}
