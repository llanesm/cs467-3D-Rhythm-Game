using Assets.Scripts;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    #region Properties
    public GameObject Node;
    public GameObject CurrNode;
    private bool Perfect = false;
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
    public int PointToCreateFrom = 0;
    public float NextTime = 0;
    public ScoreVariable Score;
    public HotStreakVariable HotStreak;
    public int HitInARow = 0;
    public int MissedNodesOrTapsInARow = 0;
    public bool GameOver = false;
    #endregion

    void Start()
    {
        Score.Value = 0;
        HotStreak.Multiplier = 1;
    }
    // Update is called once per frame
    void Update()
    {
        CheckForTapInput();

        CheckForClickInput();

        NewNodes();

        MoveNodes();
    }

    #region Scoring
    public void Scored()
    {
        Debug.Log("Hit!");
        Destroy(CurrNode);
        ExistingNodes.Remove(CurrNode);
        Perfect = false;
        Score.Value += Constants.AddToScore * HotStreak.Multiplier;
        HitInARow++;
        MissedNodesOrTapsInARow = 0;
        if (HitInARow >= Constants.HotStreakThreshold * HotStreak.Multiplier && HotStreak.Multiplier < 3)
        {
            HotStreak.Multiplier++;
        }
        
    }

    public void Missed()
    {
        Debug.Log("Miss!");
        HitInARow = 0;
        MissedNodesOrTapsInARow++;
        HotStreak.Multiplier = 1;
        if (MissedNodesOrTapsInARow > Constants.AmountMissedToGameOver)
        {
            GameOver = true;
        }
    }
    #endregion

    #region Input
    public void CheckForTapInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
            {
                if (hit.transform != null && Perfect)
                {
                    Scored();
                }
                else
                {
                    Missed();
                }
            }

        }
    }

    public void CheckForClickInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
            {
                if (hit.transform != null && Perfect)
                {
                    Scored();
                }
                else
                {
                    Missed();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            PerfectTrue();
            CurrNode = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            PerfectFalse();
            if (CurrNode)
            {
                Destroy(CurrNode);
                ExistingNodes.Remove(CurrNode);
                Missed();
            }
        }
    }

    public void PerfectTrue()
    {
        Perfect = true;
    }

    public void PerfectFalse()
    {
        Perfect = false;
    }
    #endregion

    #region Nodes
    private void NewNodes()
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
    }
    
    public void MoveNodes()
    {
        for (int i = 0; i < ExistingNodes.Count; i++)
        {
            ExistingNodes[i].transform.Translate(0, 0, MovementSpeed);
        }
    }
    #endregion
}
