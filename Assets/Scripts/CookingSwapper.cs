using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingSwapper : MonoBehaviour
{

    public Material cooked_steak_mat;
    public Material burnt_rock_mat;
    public GameObject pan;
    public Image progress_bar;

    private GameObject primordial_fire;
    private GameObject fire;

    private GameObject primordial_smoke;
    private GameObject smoke;

    private GameObject player_occupying = null;
    private GameObject cooking_item = null;
    private bool occupied = false;
    // dont let more than one player chop at a time
    private float process_wait_time = 0f;
    private float process_start_time = 0f;
    // time to burn
    private float time_to_burn = 3f;
    float timer;

    public AudioClip cookingSound;
    private AudioSource source;
    public SoundVolume sliderValue;
    //public Audio sound;

    // Start is called before the first frame update
    void Start()
    {
        source = this.gameObject.AddComponent<AudioSource>();
        // find the fire
        primordial_fire = GameObject.FindGameObjectWithTag("Fire");
        // find the smoke
        primordial_smoke = GameObject.FindGameObjectWithTag("Smoke");

    }
    void ProgressBar()
    {
        progress_bar.enabled = true;
        progress_bar.fillAmount += 0.35f * Time.deltaTime;
        //Debug.Log(progress_bar.fillAmount);
        if (progress_bar.fillAmount >= 1f)
        {
            progress_bar.enabled = false;
            //sound.stopSizzleSound();
        }

    }
    // the player has started chopping
    public float PlayerStartedCooking(GameObject player, GameObject item)
    {
        if(!occupied)
        {
            //sound.stopSizzleSound();
            source.Stop();
            float time_to_cook = 3.0f;
            progress_bar.fillAmount = 0;
            ProgressBar();
            //sound.playSizzleSound();
            source.PlayOneShot(cookingSound, sliderValue.value);
            Debug.Log(player.tag + " placed: " + item.tag + " on cutting board");
            // YOU CANT CUT THIS
            if(item.tag != "steak")
            {
                Debug.Log("You can't cook a " + item.tag);
                process_wait_time = time_to_burn;
                process_start_time = Time.time;
                cooking_item = item;
                occupied = true;
                
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
            //kill the fire if they picked it up in time
            if(Time.time < process_wait_time + process_start_time)
            {
                Debug.Log("KILLING FIRE");
                if(fire != null)
                {
                    fire.GetComponent<Fire>().killFire();
                    fire = null;
                }
            }
            occupied = false;
            //sound.stopSizzleSound();
            source.Stop();
            cooking_item = null;
            progress_bar.enabled = false;
            // reset time
            process_wait_time = 0f;
            process_start_time = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // turn off occupation after a set amount of time
        if(occupied && cooking_item != null)
        {
            if(smoke == null)
                smoke = Instantiate(primordial_smoke, gameObject.transform.position + new Vector3(0.0f, 1.0f, 0.1f), primordial_smoke.transform.rotation) as GameObject;
            ProgressBar();
            //sound.playSizzleSound();
          // source.PlayOneShot(cookingSound, 1F);
            // check if item is cooked
            if (cooking_item.tag == "steak" && Time.time >= process_wait_time + process_start_time)
            {
                Debug.Log("COOKED STEAK DONE");
                cooking_item.tag = "cooked_steak";
                cooking_item.GetComponent<Renderer>().material = cooked_steak_mat;
                // set up burn timer
                process_start_time = Time.time;
                process_wait_time = time_to_burn;
                progress_bar.fillAmount = 0;
                // set up the fire
                fire = Instantiate(primordial_fire, primordial_fire.transform.position, primordial_fire.transform.rotation) as GameObject;
                fire.GetComponent<Fire>().startFire(cooking_item, true);
                ProgressBar();
                //sound.playSizzleSound();
                source.PlayOneShot(cookingSound, sliderValue.value);

            }
            // check if item is burnt
            if(cooking_item.tag == "cooked_steak" && Time.time  >= process_wait_time + process_start_time)
            {
                Debug.Log("WE BURNT THE STEAK");
                // we burned it
                cooking_item.tag = "burnt_rock";
                cooking_item.GetComponent<Renderer>().material = burnt_rock_mat;
                // remove timer
                process_start_time = 0f;
                process_wait_time = 0f;
                progress_bar.enabled = false;
            }
            // check if item is not a steak
            if((cooking_item.tag != "steak" && cooking_item.tag != "cooked_steak" && cooking_item.tag != "burnt_rock") && fire == null)
            {
                // set up burn timer
                progress_bar.fillAmount = 0;
                // set up the fire
                fire = Instantiate(primordial_fire, primordial_fire.transform.position, primordial_fire.transform.rotation) as GameObject;
                fire.GetComponent<Fire>().startFire(cooking_item, true);
                ProgressBar();
                //sound.playSizzleSound();
                source.PlayOneShot(cookingSound, sliderValue.value);
            }
            else if((cooking_item.tag != "steak" && cooking_item.tag != "cooked_steak" && cooking_item.tag != "burnt_rock") && fire != null)
            {
                if(Time.time >= process_wait_time + process_start_time)
                {
                    cooking_item.tag = "burnt_rock";
                    cooking_item.GetComponent<Renderer>().material = burnt_rock_mat;
                    process_start_time = 0f;
                    process_wait_time = 0f;
                    progress_bar.enabled = false;
                }
            }
        }
        else
        {
            if(smoke != null)
            {
                smoke.GetComponent<Smoke>().cookComplete();
                smoke = null;
                //sound.playSizzleSound();
                source.PlayOneShot(cookingSound, sliderValue.value);
            }
            occupied = false;
            //sound.stopSizzleSound();
            source.Stop();
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CookingSwapper : MonoBehaviour
// {

//     public Material cooked_steak_mat;
//     public Material burnt_rock;

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
//             col.gameObject.GetComponent<Renderer>().material = burnt_rock;
//             col.gameObject.GetComponent<Renderer>().tag = "burnt_steak";
//             Destroy(col.gameObject, 3);
//         }

//     }
//     // Update is called once per frame
//     void Update()
//     {

//     }
// }
