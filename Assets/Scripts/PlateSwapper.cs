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

    private float min_dist = 1.0f;
   
     // check for table we're on
      private GameObject findTable()
      {
          float closest_distance = Mathf.Infinity;
          GameObject[] gos = GameObject.FindGameObjectsWithTag("normal_table");
          GameObject closest = null;
          Vector3 position = transform.position;
          foreach (GameObject go in gos)
          {
            Vector3 diff = go.GetComponent<Transform>().position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < closest_distance && curDistance < min_dist)
            {
              closest = go;
              closest_distance = curDistance;
            }
          }

          return closest;
      }

    // tell our table to update
    private void updateTable(GameObject table, GameObject new_plate)
    {
        table.GetComponent<NormalTable>().putOnTable(new_plate);
    }
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
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);

            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_cheese" || plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_lettuce"
                || plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
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
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_lettuce_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_cheese" || plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_lettuce"
                || plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
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
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_lettuce_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_lettuce_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);

            }
            else if (plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_cheese" || plate.gameObject.GetComponent<Renderer>().tag == "plate_tomato_lettuce"
                || plate.gameObject.GetComponent<Renderer>().tag == "plate_lettuce_cheese")
            {
                plate.gameObject.GetComponent<Renderer>().material = cut_tomato_lettuce_cheese_mat;
                plate.gameObject.GetComponent<Renderer>().tag = "plate_tomato_lettuce_cheese";
                Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
                updateTable(findTable(), plate);
                Destroy(col.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }

        }

    }
}
