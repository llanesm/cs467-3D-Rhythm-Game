namespace Assets.Scripts
{
    public class AudioSyncedNodeStart
    {
        public float StartTime;
        public NodeStartPoint StartPoint;

        public AudioSyncedNodeStart(float startTime, NodeStartPoint startPoint)
        {
            StartTime = startTime;
            StartPoint = startPoint;
        }
    }
}