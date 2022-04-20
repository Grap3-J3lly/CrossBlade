using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------
    private List<string> snappableList = new List<string>(){"CardController"};

    //------------------------------------------------------
    //              STANDARD FUNCTIONS
    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //------------------------------------------------------
    //                  SNAP FUNCTIONS
    //------------------------------------------------------

    public bool SnapCheck(string classType) {
        if(snappableList.Contains(classType)) {
            return true;
        }
        return false;
    }

}
