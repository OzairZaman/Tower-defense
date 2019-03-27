using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placable : MonoBehaviour {

    public bool isAvailable = true;
    public Transform pivotPoint;

    /// <summary>
    /// Returns the point point attached to the tile (if any)
    /// </summary>
    /// <returns>Returns placeable position if no pivot is made</returns>

    public Vector3 GetPivotPoint()
    {
        if (pivotPoint == null)
        {
            //
            return transform.position;

        }
        //
        return pivotPoint.position;
    }
}
