using Assets.Scripts;
using UnityEngine;
using System.Collections.Generic;
using System;

public class NodeFire : MonoBehaviour
{
    public GameObject Node;
    public GameObject CurrNode;
    private bool perfect = false;
    public float MovementSpeed = Constants.MovementSpeed;
    public readonly IList<NodeStartPoint> StartingPoints = new List<NodeStartPoint>
    {
        new NodeStartPoint(
            name: "NE",
            posX: Constants.InterCardinalRadius,
            posY: Constants.InterCardinalRadius,
            posZ: Constants.OriginDepth,
            rotX: 0,
            rotY: 0,
            rotZ: -Constants.InterCardinalRotation
            ),
        new NodeStartPoint(
            name: "SE",
            posX: Constants.InterCardinalRadius,
            posY: -Constants.InterCardinalRadius,
            posZ: Constants.OriginDepth,
            rotX: 0,
            rotY: 0,
            rotZ: Constants.InterCardinalRotation
            ),
        new NodeStartPoint(
            name: "SW",
            posX: -Constants.InterCardinalRadius,
            posY: -Constants.InterCardinalRadius,
            posZ: Constants.OriginDepth,
            rotX: 0,
            rotY: 0,
            rotZ: -Constants.InterCardinalRotation
            ),
        new NodeStartPoint(
            name: "NW",
            posX: -Constants.InterCardinalRadius,
            posY: Constants.InterCardinalRadius,
            posZ: Constants.OriginDepth,
            rotX: 0,
            rotY: 0,
            rotZ: Constants.InterCardinalRotation
            ),
    };
    readonly List<GameObject> ExistingNodes = new List<GameObject>();
    int PointToCreateFrom = 0;
    float NextTime = 0;

    // Update is called once per frame
    void Update()
    {

        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null && perfect)
                {
                    Debug.Log("Perfect Hit");
                    Destroy(CurrNode);
                    ExistingNodes.Remove(CurrNode);
                    perfect = false;
                }
                else
                {
                    Debug.Log("Too early or Late!");
                }
            }

        }


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
                    Destroy(CurrNode);
                    ExistingNodes.Remove(CurrNode);
                    perfect = false;
                }
                else
                {
                    Debug.Log("Too early or Late!");
                }
            }
        }

        if (Time.time >= NextTime)
        {
            ExistingNodes.Add(Instantiate(Node, StartingPoints[PointToCreateFrom].Transform, StartingPoints[PointToCreateFrom].Rotation));
            NextTime += Constants.Interval;
            PointToCreateFrom++;
            if (PointToCreateFrom > StartingPoints.Count - 1)
            {
                PointToCreateFrom = 0;
            }
        }

        // Found that the warnings were coming from the foreach() statement (some stuff online I didnt understand) 
        // So I replaced with a normal for loop and they are now gone
        for (int i = 0; i < ExistingNodes.Count; i++ ) 
        {

            if (ExistingNodes[i].transform.position.z <= Constants.TerminationDepth)
            {
                Destroy(ExistingNodes[i]);
                ExistingNodes.Remove(ExistingNodes[i]);
            }
            else
            {
                ExistingNodes[i].transform.Translate(0, 0, MovementSpeed);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            perfectTrue();
            CurrNode = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            perfectFalse();
            if (CurrNode)
            {
                Destroy(CurrNode);
                ExistingNodes.Remove(CurrNode);
            }
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
