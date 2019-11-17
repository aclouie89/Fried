using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FireSprite {One = 0, Two, Three, Four, Five}

public class Fire : MonoBehaviour
{
  public bool is_player_fire = false;
  private float time_alive = 30.0f;

  private Material[] fire_mat;
  private int last_mesh_index = (int)FireSprite.Five;
  private int cur_fire = 0;

  private float time_to_anim = 0.15f;
  private float anim_start_time;

  private bool fire_death_enabled = false;
  private float live_time;
  private GameObject on_fire;

  private float min_dist = 6.0f;
  private GameObject Player1;
  private GameObject Player2;
  private GameObject burned_player;

  // movement for held fire
  private float smoothTime = 0.001f;
  private Vector3 AVelocity = Vector3.zero;

  public void startFire(GameObject thing, bool killItWithFire)
  {
    Player1 = GameObject.FindGameObjectWithTag("Player1");
    Player2 = GameObject.FindGameObjectWithTag("Player2");
    on_fire = thing;
    fire_death_enabled = killItWithFire;
    live_time = Time.time;
  }

  public void killFire()
  {
    Debug.Log("Fire ordered to destroy self");
    Destroy(gameObject);
  }

  // Start is called before the first frame update
  void Start()
  {
    fire_mat = GetComponent<Renderer>().materials;
    // set up the sprite
    for(int x = 0; x < fire_mat.Length; x++)
    {
      GetComponent<Renderer>().materials[x].color = new Color(1,1,1,0f);
    }
    showSingleMesh((int) FireSprite.One);
    anim_start_time = Time.time;
    if(is_player_fire)
      time_alive = 8f;
    else
      time_alive = 30f;
  }

  // display only one mesh
  private void showSingleMesh(int index)
  {
    if(index != last_mesh_index)
    {
      Color fade = new Color(1,1,1,0f);
      Color vis = new Color(1,1,1,1f);
      GetComponent<Renderer>().materials[index].color = new Color(1,1,1,1f);;
      GetComponent<Renderer>().materials[last_mesh_index].color = new Color(1,1,1,0f);
      last_mesh_index = index;
    }
  }

  private int nextMesh()
  {
    if(cur_fire == (int)FireSprite.Five)
      cur_fire = (int)FireSprite.One;
    else
      cur_fire++;
    return cur_fire;
  }

  public void set_player_burnt(GameObject player)
  {
    burned_player = player;
  }

  // finds closest game object given a list
  private GameObject findClosestPlayerInDist()
  {
    // player 1 distance
    Vector3 position = transform.position;
    Vector3 p1_diff = Player1.GetComponent<Transform>().position - position;
    float player1_dist = p1_diff.sqrMagnitude;
    // player 2 distance
    Vector3 p2_diff = Player2.GetComponent<Transform>().position - position;
    float player2_dist = p2_diff.sqrMagnitude;

    // Debug.Log("P1 dist: " + player1_dist);
    // Debug.Log("P2 dist: " + player2_dist);
    if(player1_dist < min_dist && player1_dist < player2_dist)
    {
      // Debug.Log("Player1 found");
      return Player1;
    }
    else if(player2_dist < min_dist && player2_dist < player1_dist)
    {
      // Debug.Log("Player2 found");
      return Player2;
    }
    return null;
  }

  // Update is called once per frame
  void Update()
  {
    // animate the fire
    if(Time.time >= anim_start_time + time_to_anim)
    {
      anim_start_time = Time.time;
      showSingleMesh(nextMesh()); 
    }
    // move and destroy this fire
    if(fire_death_enabled)
    {
      // move the fire
      if(on_fire != null && on_fire.activeInHierarchy)
        transform.position = Vector3.SmoothDamp(transform.position, on_fire.transform.position + new Vector3(0,0.6f,0.0f), ref AVelocity, smoothTime);
      else
      {
        // Debug.Log("Fire destroying self since other object did");
        // it prob hit something, check if its a player
        GameObject player = findClosestPlayerInDist();
        // SET THIS GUY ON FIRE
        if(player != null)
          player.GetComponent<PlayerControl>().SetOnFire();
        Destroy(gameObject);
      }        
      if(Time.time >= live_time + time_alive)
      {
        // tell player we're done with fire
        if(is_player_fire && burned_player != null)
          burned_player.GetComponent<PlayerControl>().FireComplete();
        Debug.Log("Fire destroying self");
        Destroy(gameObject);
      }
    }
  }
}
