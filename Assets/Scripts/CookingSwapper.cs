using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSwapper : MonoBehaviour
{

    public Material cooked_steak_mat;
    public Material burnt_steak_mat;

    public GameObject stove;

    // Start is called before the first frame update
    void Start()
    {

    }

    private IEnumerator OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Renderer>().tag == "steak")
        {
            yield return new WaitForSeconds(5);
            col.gameObject.GetComponent<Renderer>().material = cooked_steak_mat;
            col.gameObject.GetComponent<Renderer>().tag = "cooked_steak";
            Debug.Log(col.gameObject.GetComponent<Renderer>().tag);
            yield return new WaitForSeconds(5);
            col.gameObject.GetComponent<Renderer>().material = burnt_steak_mat;
            col.gameObject.GetComponent<Renderer>().tag = "burnt_steak";
            Destroy(col.gameObject, 3);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
