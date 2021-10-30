using UnityEngine;

namespace Assets.Scripts
{
    public class NodeStartPoint
    {
        public string Name { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float RotX { get; set; }
        public float RotY { get; set; }
        public float RotZ { get; set; }
        public Vector3 Transform { get; set; }
        public Quaternion Rotation { get; set; }

        public NodeStartPoint(string name, float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
        {
            Name = name;
            PosX = posX;
            PosY = posY;
            PosZ = posZ;
            RotX = rotX;
            RotY = rotY;
            RotZ = rotZ;
            Transform = new Vector3(posX, posY, posZ);
            Rotation = Quaternion.Euler(rotX, rotY, rotZ);
        }
    }
}
