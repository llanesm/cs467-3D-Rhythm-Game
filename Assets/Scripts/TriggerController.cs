using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{

    private bool perfect = false;
    public GameObject Node;


    // Update is called once per frame
    void Update()
    {
        // Check for the user click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null && perfect)
                {
                    Debug.Log("Perfect Hit");
                }
                else
                {
                    Debug.Log("Too early or Late!");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            perfectTrue();
            Debug.Log("PERFECT NOW");
            Destroy(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            perfectFalse();
            Debug.Log("Out of perfect zone");
        }
    }

    public void perfectTrue()
    {
        perfect = true;
    }

    public void perfectFalse()
    {
        perfect = false;
    }
}
