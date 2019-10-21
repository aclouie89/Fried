using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSwapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("entered");
    }
    private void OnCollisionEnter(Collision col)
    {    
            Debug.Log("entered");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
