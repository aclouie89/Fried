using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum BoardOrientation {North=0, East, South, West}
public class ChoppingBoardSwapper : MonoBehaviour
{
    public Material cut_tomato_mat;
    public Material cut_lettuce_mat;
    public Material cut_cheese_mat;

    public GameObject board;

    // knife aniations
    private GameObject base_knife;
    private GameObject knife = null;
    public int orientation = (int)BoardOrientation.North;
    
    private GameObject player_occupying = null;
    private bool occupied = false;
    // dont let more than one player chop at a time
    private float process_wait_time = 0f;
    private float process_start_time = 0f;
    public Image progress_bar;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        // find the knife
        if(orientation == (int) BoardOrientation.South || orientation == (int) BoardOrientation.East)
            base_knife = GameObject.FindGameObjectWithTag("Knives East");
        else
            base_knife = GameObject.FindGameObjectWithTag("Knives West");
    }

    void ProgressBar()
    {
        progress_bar.enabled = true;
        
        progress_bar.fillAmount += 0.35f * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = .35f;//<--this happens about every second;
           
        }

        if (progress_bar.fillAmount >= 1f)
        {
            progress_bar.enabled = false;
        }

    }

    // private Vector3 getKnifeRotation()
    // {
    //     if(orientation == (int)BoardOrientation.North)
    //         return new Vector3(0.5)
    //     else if(orientation == (int)BoardOrientation.East)
    //     else if(orientation == (int)BoardOrientation.South)
    //     else if(orientation == (int)BoardOrientation.West)

    //     return new Vector3(0,0,0);
    // }

    // the player has started chopping
    public float PlayerStartedChopping(GameObject player, GameObject item)
    {
        if (!occupied)
        {
            float time_to_chop = 3.0f;
            progress_bar.fillAmount = 0;
            ProgressBar();
            // start chopping animation
            if(knife == null)
            {
                Vector3 offset;
                // find the knife
                if(orientation == (int) BoardOrientation.South || orientation == (int) BoardOrientation.East)
                    offset = new Vector3(-0.5f, 0.8f, 0.0f);
                else
                    offset = new Vector3(0.5f, 0.8f, 0.0f);
                knife = Instantiate(base_knife, gameObject.transform.position + offset, base_knife.transform.rotation) as GameObject;
            }
            
            Debug.Log(player.tag + " placed: " + item.tag + " on cutting board");
            // YOU CANT CUT THIS
            if (item.tag != "tomato" && item.tag != "Lettuce" && item.tag != "Cheese")
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
    public void PlayerFinishedChopping(GameObject item)
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
        process_wait_time = 0f;
        process_start_time = 0f;
    }

    // private IEnumerator OnTriggerEnter(Collider col)
    // {
    //     if (col.gameObject.GetComponent<Renderer>().tag == "tomato")
    //     {
    //         Debug.Log(col.gameObject.GetComponent<Renderer>().tag);
    //         yield return new WaitForSeconds(5);
    //         col.gameObject.GetComponent<Renderer>().material = cut_tomato_mat;
    //         col.gameObject.GetComponent<Renderer>().tag = "cut_tomato";
    //         Debug.Log(col.gameObject.GetComponent<Renderer>().tag);

    //     }
    //     else if (col.gameObject.GetComponent<Renderer>().tag == "Lettuce")
    //     {
    //         Debug.Log(col.gameObject.GetComponent<Renderer>().tag);
    //         yield return new WaitForSeconds(5);
    //         col.gameObject.GetComponent<Renderer>().material = cut_lettuce_mat;
    //         col.gameObject.GetComponent<Renderer>().tag = "cut_lettuce";
    //         Debug.Log(col.gameObject.GetComponent<Renderer>().tag);

    //     }
    //     else if (col.gameObject.GetComponent<Renderer>().tag == "Cheese")
    //     {
    //         Debug.Log(col.gameObject.GetComponent<Renderer>().tag);
    //         yield return new WaitForSeconds(5);
    //         col.gameObject.GetComponent<Renderer>().material = cut_cheese_mat;
    //         col.gameObject.GetComponent<Renderer>().tag = "cut_cheese";
    //         Debug.Log(col.gameObject.GetComponent<Renderer>().tag);

    //     }


    // }
    // Update is called once per frame
    void Update()
    {
        // turn off occupation after a set amount of time
        if (occupied)
        {
            // check if player got cc'd if so remove occupied flag
            //i = 0;
            timer -= Time.deltaTime;
            ProgressBar();
            if (player_occupying.GetComponent<PlayerControl>().status == (int)PlayerStatus.CC)
            {
                if(knife != null)
                {
                    knife.GetComponent<Chopping>().chopComplete();
                    knife = null;
                }
                occupied = false;
                process_start_time = 0f;
                process_wait_time = 0f;

              
            }
            // check if time is exceeded
            if (process_wait_time < Time.time - process_start_time)
            {
                if(knife != null)
                {
                    knife.GetComponent<Chopping>().chopComplete();
                    knife = null;
                }
                occupied = false;
                process_start_time = 0f;
                process_wait_time = 0f;
              
            }
        }
        else
        {        
            if(knife != null)
            {
                knife.GetComponent<Chopping>().chopComplete();
                knife = null;
            }
        }
    }
}

