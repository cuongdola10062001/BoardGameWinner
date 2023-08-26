using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public float minLimmitedX;
    public float maxLimmitedX;
    public float minLimmitedZ;
    public float maxLimmitedZ;

    public bool CheckArea(Vector3 position)
    {
        if (position.z >= minLimmitedZ && position.z <= maxLimmitedZ)
            return true;
        return false;
    }
}
