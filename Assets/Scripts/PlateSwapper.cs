﻿using System.Collections;
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
            col.gameObject.GetComponent<Renderer>().material = cut_tomato_mat;
           
            col.gameObject.GetComponent<Renderer>().tag = "cut_tomato";
            Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
            Destroy(col);
        }
        if (col.gameObject.GetComponent<Renderer>().tag == "lettuce")
        {
            yield return new WaitForSeconds(5);
            col.gameObject.GetComponent<Renderer>().material = cut_lettuce_mat;

            col.gameObject.GetComponent<Renderer>().tag = "cut_lettuce";
            Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
            Destroy(col);
        }
        if (col.gameObject.GetComponent<Renderer>().tag == "cheese")
        {
            yield return new WaitForSeconds(5);
            col.gameObject.GetComponent<Renderer>().material = cut_cheese_mat;

            col.gameObject.GetComponent<Renderer>().tag = "cut_cheese";
            Debug.Log(plate.gameObject.GetComponent<Renderer>().tag);
            Destroy(col);
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
