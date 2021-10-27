using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseEnter() 
    {
        GetComponent<Renderer>().material.color = Color.maroon;
    }

    void OnMouseExit() 
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
