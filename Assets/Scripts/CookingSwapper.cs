using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSwapper : MonoBehaviour
{
    public Material cut_tomato_mat;
    public Material cut_lettuce_mat;
    public Material cut_cheese_mat;

    public GameObject board;

    private GameObject player_occupying = null;
    private bool occupied = false;
    // dont let more than one player chop at a time
    private float process_wait_time = 0f;
    private float process_start_time = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // the player has started chopping
    public float PlayerStartedCooking(GameObject player, GameObject item)
    {
        if(!occupied)
        {
            float time_to_chop = 3.0f;
            Debug.Log(player.tag + " placed: " + item.tag + " on cutting board");
            // YOU CANT CUT THIS
            if(item.tag != "tomato" && item.tag != "Lettuce" && item.tag != "Cheese")
            {
                Debug.Log("You can't chop a " + item.tag);
                return 0f;
            }
            process_wait_time = time_to_chop;
            process_start_time = Time.time;
            player_occupying = player;
            occupied = true;
            return time_to_chop;
        }
        else
        {
            Debug.Log("Occupied");
            return 0f;
        }
    }

    // player says they're finished, swap the item
    public void PlayerFinishedCooking(GameObject item)
    {
        if (item.tag == "tomato")
        {
            item.GetComponent<Renderer>().material = cut_tomato_mat;
            item.tag = "cut_tomato";
            occupied = false;
       
        }
        else if (item.tag == "Lettuce")
        {
            item.GetComponent<Renderer>().material = cut_lettuce_mat;
            item.tag = "cut_lettuce";
            occupied = false;
           
        }
        else if (item.tag == "Cheese")
        {
            item.GetComponent<Renderer>().material = cut_cheese_mat;
            item.tag = "cut_cheese";
            occupied = false;
        }
        // reset time
        float process_wait_time = 0f;
        float process_start_time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // turn off occupation after a set amount of time
        if(occupied)
        {
            // check if player got cc'd if so remove occupied flag
            if(player_occupying.GetComponent<PlayerControl>().status == (int) PlayerStatus.CC)
            {
                occupied = false;
                process_start_time = 0f;
                process_wait_time = 0f;
            }
            // check if time is exceeded
            if(process_wait_time < Time.time - process_start_time)
            {
                occupied = false;
                process_start_time = 0f;
                process_wait_time = 0f;
            }
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
