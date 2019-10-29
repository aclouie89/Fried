using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlateSwapper : MonoBehaviour
{

    public Material cut_tomato_mat;
    public Material cut_lettuce_mat;
    public Material cut_cheese_mat;
    public Material cut_tomato_lettuce_mat;
    public Material cut_tomato_cheese_mat;
    public Material cut_lettuce_cheese_mat;
    public Material cut_tomato_lettuce_cheese_mat;

    public GameObject plate;
   
    private IEnumerator OnTriggerEnter(Collider col)
    {
        Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
        if (col.gameObject.GetComponent<Renderer>().tag == "cut_tomato")
        {
           
            if (plate.gameObject.GetComponent<Renderer>().tag == "Plate")
            {
                Debug.Log(col.gameObject.GetComponent<Renderer>().tag);
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);

            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_cheese" || plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_lettuce"
                || plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }

        }
        else if (col.gameObject.GetComponent<Renderer>().tag == "cut_lettuce")
        {
            yield return new WaitForSeconds(0.01f);
            if (plate.gameObject.GetComponent<Renderer>().tag == "Plate")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_lettuce";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_lettuce_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_cheese" || plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_lettuce"
                || plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }

        }
        else if (col.gameObject.GetComponent<Renderer>().tag == "cut_cheese")
        {
            yield return new WaitForSeconds(0.01f);
            if (plate.gameObject.GetComponent<Renderer>().tag == "Plate")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_lettuce_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_lettuce_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);

            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_cheese" || plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_lettuce"
                || plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                Destroy(col.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }

        }

    }
}
