using Assets.Scripts;
using UnityEngine;
using System.Collections.Generic;
using System;

public class NodeFire : MonoBehaviour
{
    public GameObject Node;
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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
        foreach (var node in ExistingNodes)
        {
            if (node.transform.position.z <= Constants.TerminationDepth)
            {
                Destroy(node);
                ExistingNodes.Remove(node);
            }
            else
            {
                node.transform.Translate(0, 0, Constants.MovementSpeed);
            }
        }
    }
}
