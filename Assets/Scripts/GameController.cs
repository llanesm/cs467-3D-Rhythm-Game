using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
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

    [SerializeField]
    private Material RingMaterial;

    private Color MutedDarkGreen = new Color(46/255f, 64 / 255f, 69 / 255f);
    private Color MutedRed = new Color(88 / 255f, 44 / 255f, 77 / 255f);
    private Color MutedBlue = new Color(50 / 255f, 59 / 255f, 120 / 255f);
    public float MovementSpeed = Constants.MovementSpeed;
    public ScoreVariable Score;
    public HotStreakVariable HotStreak;
    public HighestStreakVariable HighestStreak;
    public AudioSource MusicSource;

    public static readonly IList<NodeStartPoint> StartingPoints = new List<NodeStartPoint>
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

    public IList<GameObject> ExistingNodes = new List<GameObject>();

    public IList<AudioSyncedNodeStart> AudioSyncedNodes = new List<AudioSyncedNodeStart>    // list of node start times (audio time minus travel time) along with start point
    {
        new AudioSyncedNodeStart(8F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(10.8F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(13.5F - Constants.NodeDelay, StartingPoints[2]),
        new AudioSyncedNodeStart(16F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(18.5F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(20F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(21.5F - Constants.NodeDelay, StartingPoints[2]),
        new AudioSyncedNodeStart(23.5F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(24.1F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(26F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(26.8F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(29.4F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(29.6F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(31.9F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(32F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(32.2F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(34.7F - Constants.NodeDelay, StartingPoints[2]),
        new AudioSyncedNodeStart(34.9F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(37.8F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(40.2F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(41.6F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(42.9F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(43.9F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(44.5F - Constants.NodeDelay, StartingPoints[2]),
        new AudioSyncedNodeStart(44.9F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(45F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(45.4F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(46.4F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(47F - Constants.NodeDelay, StartingPoints[2]),
        new AudioSyncedNodeStart(47.5F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(48F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(49F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(49.8F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(50.9F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(51.7F - Constants.NodeDelay, StartingPoints[2]),
        new AudioSyncedNodeStart(52.3F - Constants.NodeDelay, StartingPoints[2]),
        new AudioSyncedNodeStart(52.7F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(53.5F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(54.5F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(55.5F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(56.2F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(57.1F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(58.4F - Constants.NodeDelay, StartingPoints[3]),
        new AudioSyncedNodeStart(58.9F - Constants.NodeDelay, StartingPoints[2]),
        new AudioSyncedNodeStart(59.9F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(60.5F - Constants.NodeDelay, StartingPoints[0]),
        new AudioSyncedNodeStart(60.9F - Constants.NodeDelay, StartingPoints[1]),
        new AudioSyncedNodeStart(61.3F - Constants.NodeDelay, StartingPoints[2]),
        new AudioSyncedNodeStart(62.9F - Constants.NodeDelay, StartingPoints[2]),
    };

    public int PointToCreateFrom = 0;
    public float NextTime = 0;
    public int HitInARow = 0;
    public int MissedNodesOrTapsInARow = 0;
    public int SyncedNodesPos = 0;

    #endregion

    private void Start()
    {
        Score.Value = 0;
        HotStreak.Multiplier = 1;
        HighestStreak.Value = 0;
        RingMaterial.color = MutedDarkGreen;
        MusicSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
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
            }
            else if (HotStreak.Multiplier == 3)
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
        RingMaterial.color = MutedDarkGreen;
        if (HitInARow > HighestStreak.Value)
        {
            HighestStreak.Value = HitInARow;
        }
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
        if (SyncedNodesPos < AudioSyncedNodes.Count && MusicSource.time >= AudioSyncedNodes[SyncedNodesPos].StartTime)
        {
            GameObject NewNode = Instantiate(Node, AudioSyncedNodes[SyncedNodesPos].StartPoint.Transform, AudioSyncedNodes[SyncedNodesPos].StartPoint.Rotation);

            // Adding name to differentiate nodes before adding to existingNodes
            NewNode.name = AudioSyncedNodes[SyncedNodesPos].StartPoint.Name;
            ExistingNodes.Add(NewNode);
            SyncedNodesPos++;
        }
    }

    private void MoveNodes()
    {
        for (int i = 0; i < ExistingNodes.Count; i++)
        {
            ExistingNodes[i].transform.Translate(0, 0, MovementSpeed);
        }

        if (!MusicSource.isPlaying)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }

    #endregion
}