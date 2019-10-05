using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwappingMaterials : MonoBehaviour
{
    public Material[] material;
    public KeyCode key;
    Renderer rend;

    float timer = 1f;
    float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = material[0];

    }

    void UnChopped() {
        GetComponent<Renderer>().material = material[0];
        Invoke("Chopped", 3.0f);

    }
    void Chopped()
    {
        GetComponent<Renderer>().material = material[1];
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Invoke("UnChopped", 1.0f);
        }
    }
}
