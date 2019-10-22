using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlateSwapper : MonoBehaviour
{

    public Material mat;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider col)
    {    
            Debug.Log("trigger");

        Material mat = col.gameObject.GetComponent<Material>;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
