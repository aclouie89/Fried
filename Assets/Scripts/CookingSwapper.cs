using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingSwapper : MonoBehaviour
{

    public Material cooked_steak_mat;
    public Material burnt_steak_mat;
    public GameObject pan;
    public Image progress_bar;

    private GameObject player_occupying = null;
    private GameObject cooking_item = null;
    private bool occupied = false;
    // dont let more than one player chop at a time
    private float process_wait_time = 0f;
    private float process_start_time = 0f;
    // time to burn
    private float time_to_burn = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }
    void ProgressBar()
    {
        progress_bar.enabled = true;
        progress_bar.fillAmount += 0.35f * Time.deltaTime;
        Debug.Log(progress_bar.fillAmount);
        if (progress_bar.fillAmount >= 1f)
        {
            progress_bar.enabled = false;
        }

    }
    // the player has started chopping
    public float PlayerStartedCooking(GameObject player, GameObject item)
    {
        if(!occupied)
        {
            float time_to_cook = 3.0f;
            progress_bar.fillAmount = 0;
            ProgressBar();
            Debug.Log(player.tag + " placed: " + item.tag + " on cutting board");
            // YOU CANT CUT THIS
            if(item.tag != "steak")
            {
                Debug.Log("You can't cook a " + item.tag);
                return 0f;
            }
            process_wait_time = time_to_cook;
            process_start_time = Time.time;
            player_occupying = player;
            occupied = true;
            cooking_item = item;
            return time_to_cook;
        }
        else
        {
            Debug.Log("Occupied");
            return 0f;
        }
    }

    // player says they're finished, swap the item
    public void PlayerPickedUpItem()
    {
        if(occupied && cooking_item != null)
        {
            occupied = false;
            cooking_item = null;
            // reset time
            float process_wait_time = 0f;
            float process_start_time = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // turn off occupation after a set amount of time
        if(occupied && cooking_item != null)
        {
            ProgressBar();
            // check if item is cooked
            if(cooking_item.tag == "steak" && process_wait_time < Time.time - process_start_time)
            {
                Debug.Log("COOKED STEAK DONE");
                cooking_item.tag = "cooked_steak";
                cooking_item.GetComponent<Renderer>().material = cooked_steak_mat;
                // set up burn timer
                process_start_time = Time.time;
                process_wait_time = time_to_burn;
                progress_bar.fillAmount = 0;
                ProgressBar();

            }
            // check if item is burnt
            if(cooking_item.tag == "cooked_steak" && process_wait_time < Time.time - process_start_time)
            {
                Debug.Log("WE BURNT THE STEAK");
                // we burned it
                cooking_item.tag = "burnt_steak";
                cooking_item.GetComponent<Renderer>().material = burnt_steak_mat;
                // remove timer
                process_start_time = 0f;
                process_wait_time = 0f;
            }
        }
        else
        {
            occupied = false;
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CookingSwapper : MonoBehaviour
// {

//     public Material cooked_steak_mat;
//     public Material burnt_steak_mat;

//     public GameObject stove;

//     // Start is called before the first frame update
//     void Start()
//     {

//     }

//     private IEnumerator OnTriggerEnter(Collider col)
//     {
//         if (col.gameObject.GetComponent<Renderer>().tag == "steak")
//         {
//             yield return new WaitForSeconds(5);
//             col.gameObject.GetComponent<Renderer>().material = cooked_steak_mat;
//             col.gameObject.GetComponent<Renderer>().tag = "cooked_steak";
//             Debug.Log(col.gameObject.GetComponent<Renderer>().tag);
//             yield return new WaitForSeconds(5);
//             col.gameObject.GetComponent<Renderer>().material = burnt_steak_mat;
//             col.gameObject.GetComponent<Renderer>().tag = "burnt_steak";
//             Destroy(col.gameObject, 3);
//         }

//     }
//     // Update is called once per frame
//     void Update()
//     {

//     }
// }
