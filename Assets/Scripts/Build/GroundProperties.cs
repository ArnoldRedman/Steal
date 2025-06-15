using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundProperties : MonoBehaviour
{
    public GroundPropertyData groundProperty=new GroundPropertyData();
    
    
    void Start()
    {
        groundProperty.GroundName=this.gameObject.name;     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
