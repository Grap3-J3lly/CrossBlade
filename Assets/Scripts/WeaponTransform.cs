using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponTransform 
{

    //------------------------------------------------------
    //              FULL TRANSFORMS BY WEAPON
    //------------------------------------------------------

    public List<Vector3> defaultGreatswordTransform = new List<Vector3> {
        new Vector3(1.5f, -1f, 0f), // Position
        new Vector3(0f, 0f, 45f),   // Rotation
        new Vector3(2f, 2f, 2f)     // Scale
    };

    public List<Vector3> defaultTowerShieldTransform = new List<Vector3> {
        new Vector3(0f, 0f, 0f), // Position
        new Vector3(0f, 0f, 0f),   // Rotation
        new Vector3(2f, 2f, 2f)     // Scale
    };

}
