using Assets.Scripts;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Properties

    public GameObject Node;
    public GameObject CurrNode;
    public int HitPrecision_N = 0;
    public int HitPrecision_NE = 0;
    public int HitPrecision_E = 0;
    public int HitPrecision_SE = 0;
    public int HitPrecision_S = 0;
    public int HitPrecision_SW = 0;
    public int HitPrecision_W = 0;
    public int HitPrecision_NW = 0;
    [SerializeField] private Material RingMaterial;
    private Color MutedDarkGreen = new Color(46/255f, 64 / 255f, 69 / 255f);
    private Color MutedRed = new Color(88 / 255f, 44 / 255f, 77 / 255f);
    private Color MutedBlue = new Color(50 / 255f, 59 / 255f, 120 / 255f);
    public float MovementSpeed = Constants.MovementSpeed;
    public IList<NodeStartPoint> StartingPoints = new List<NodeStartPoint>
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
    public List<GameObject> ExistingNodes = new List<GameObject>();
    public int PointToCreateFrom = 0;
    public float NextTime = 0;
    public ScoreVariable Score;
    public HotStreakVariable HotStreak;
    public HighestStreakVariable HighestStreak;
    public int HitInARow = 0;
    public int MissedNodesOrTapsInARow = 0;
    public bool GameOver = false;

    #endregion

    void Start()
    {
        Score.Value = 0;
        HotStreak.Multiplier = 1;
        HighestStreak.Value = 0;
        RingMaterial.color = MutedDarkGreen;
    }
    // Update is called once per frame
    void Update()
    {

        NewNodes();

        MoveNodes();

        CheckForTapInput();

        CheckForClickInput();

    }

    #region Scoring

    public void Scored(string direction, int precision)
    {
        Debug.Log("Hit!");
        Destroy(CurrNode);
        ExistingNodes.Remove(CurrNode);
        Score.Value += (Constants.AddToScore * HotStreak.Multiplier) + precision;
        HitInARow++;
        MissedNodesOrTapsInARow = 0;
        if (HitInARow >= Constants.HotStreakThreshold * HotStreak.Multiplier && HotStreak.Multiplier < 3)
        {
            HotStreak.Multiplier++;
            if (HotStreak.Multiplier == 2)
            {
                RingMaterial.color = MutedRed;
            } else if (HotStreak.Multiplier == 3)
            {
                RingMaterial.color = MutedBlue;
            }
            
        }

        // Handle resetting the HitPrecision based on direction
        if (direction == "NE")
        {
            HitPrecision_NE = 0;
        }
        else if (direction == "NW")
        {
            HitPrecision_NW = 0;
        }
        else if (direction == "SW")
        {
            HitPrecision_SW = 0;
        }
        else if (direction == "SE")
        {
            HitPrecision_SE = 0;
        }

    }

    public void Missed()
    {
        Debug.Log("Miss!");
        ///Debug.Log(HitPrecision_NE);
        RingMaterial.color = MutedDarkGreen;
        /*
        if (HitInARow > HighestStreak.Value)
        {
            HighestStreak.Value = HitInARow;
        }
        */
        HitInARow = 0;
        MissedNodesOrTapsInARow++;
        HotStreak.Multiplier = 1;
        if (MissedNodesOrTapsInARow > Constants.AmountMissedToGameOver)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }

    #endregion

    #region Input

    private void CheckForTapInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
            {
                if (hit.transform != null && HitPrecision_NE >0)
                {
                    Scored("NE", HitPrecision_NE);
                }
                else
                {
                    Missed();
                }
            }

        }
    }

    private void CheckForClickInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
            {
                if (hit.transform.name.Contains("NE") && HitPrecision_NE > 0)
                {
                    Scored("NE", HitPrecision_NE);
                }
                else if (hit.transform.name.Contains("SE") && HitPrecision_SE > 0)
                {
                    Scored("SE", HitPrecision_SE);
                }
                else if (hit.transform.name.Contains("SW") && HitPrecision_SW > 0)
                {
                    Scored("SW", HitPrecision_SW);
                }
                else if (hit.transform.name.Contains("NW") && HitPrecision_NW > 0)
                {
                    Scored("NW", HitPrecision_NW);
                } else
                {
                    Missed();
                }
            }
        }
    }

 
    #endregion

    #region Nodes
    private void NewNodes()
    {
        if (Time.time >= NextTime)
        {
            GameObject NewNode = Instantiate(Node, StartingPoints[PointToCreateFrom].Transform, StartingPoints[PointToCreateFrom].Rotation);

            // Adding name to differentiate nodes before adding to existingNodes
            NewNode.name = StartingPoints[PointToCreateFrom].Name;
            ExistingNodes.Add(NewNode);

            NextTime += Constants.Interval;
            PointToCreateFrom++;
            if (PointToCreateFrom > StartingPoints.Count - 1)
            {
                PointToCreateFrom = 0;
            }
        }
    }
    
    private void MoveNodes()
    {
        for (int i = 0; i < ExistingNodes.Count; i++)
        {
            ExistingNodes[i].transform.Translate(0, 0, MovementSpeed);
        }
    }

    #endregion
}
