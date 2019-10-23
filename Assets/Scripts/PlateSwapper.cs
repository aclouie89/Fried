using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlateSwapper : MonoBehaviour
{

    public Material cut_tomato_mat;
    public Material cut_lettuce_mat;
    public Material cut_cheese_mat;

    public GameObject plate;

    // Start is called before the first frame update
    void Start()
    {

    }

    private IEnumerator OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Renderer>().tag == "tomato")
        {
            yield return new WaitForSeconds(5);
            plate.gameObject.GetComponent<Renderer>().material = cut_tomato_mat;
           
            plate.gameObject.GetComponent<Renderer>().tag = "tomato_chopped";
            Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
            Destroy(col);
        }
        if (col.gameObject.GetComponent<Renderer>().tag == "lettuce")
        {
            yield return new WaitForSeconds(5);
            plate.gameObject.GetComponent<Renderer>().material = cut_lettuce_mat;

            plate.gameObject.GetComponent<Renderer>().tag = "lettuce_chopped";
            Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
            Destroy(col);
        }
        if (col.gameObject.GetComponent<Renderer>().tag == "cheese")
        {
            yield return new WaitForSeconds(5);
            plate.gameObject.GetComponent<Renderer>().material = cut_cheese_mat;

            plate.gameObject.GetComponent<Renderer>().tag = "cheese_chopped";
            Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
            Destroy(col);
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
