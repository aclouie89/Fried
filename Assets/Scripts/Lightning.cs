using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LightningSprite {One = 0, Two, Three, Four, Five, Six, Seven, Eight, Kill}
enum StrikeState {Start=0, Wait, Strike}

public class Lightning : MonoBehaviour
{
  public bool dev_disable_harmful = false;
  private bool spawned_lightning = false;
  private Material[] lightning_mat;
  private int last_mesh_index = (int) LightningSprite.Two;
  private int cur_lightning = (int) LightningSprite.One;

  // max time between lightning strikes
  private float max_time_strike = 15f;
  private float min_time_strike = 3f;
  // strike = true means we throw out lightning
  private int strike = (int)StrikeState.Start;

  // animation times
  private float time_to_anim = 0.1f;
  private float anim_start_time;

  // player tracking
  private bool is_tracking = false;
  private float time_to_light = 2.0f;
  private float time_to_light_minimum = 1.0f;
  private int player_to_hit = (int) PlayerNum.None;
  private bool hit_player = false;

  // players
  private GameObject Player1;
  private GameObject Player2;
  private Vector3 p1_pos;
  private Vector3 p2_pos;
  private float min_dist = 6.0f;  // tested value

  public void startLightning()
  {
    spawned_lightning = true;
  }

  // Start is called before the first frame update
  void Start()
  {
    Player1 = GameObject.FindGameObjectWithTag("Player1");
    Player2 = GameObject.FindGameObjectWithTag("Player2");
    lightning_mat = GetComponent<Renderer>().materials;
    // set up sprite
    for(int x = 0; x < lightning_mat.Length-1; x++)
    {
      GetComponent<Renderer>().materials[x].color = new Color(1,1,1,0f);
    }
    showSingleMesh((int) LightningSprite.One);
    anim_start_time = Time.time;

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
    cur_lightning++;
    // kill spawned lightning here
    // non spawned lightning must set the sprite back to one
    if(cur_lightning == (int)LightningSprite.Kill)
    {
      cur_lightning = (int) LightningSprite.One;
      if(spawned_lightning)
        Destroy(gameObject);
    }
    return cur_lightning;
  }

  // coroutine to to wait for player to move
  private IEnumerator waitToLight()
  {
    // random time to wait
    System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);
    float cur_time = (float) rnd.NextDouble() * time_to_light + time_to_light_minimum;
    Debug.Log("Lightning strike in: " + cur_time + " seconds");
    while(cur_time > 0)
    {
      cur_time -= Time.deltaTime;
      yield return null;
    }
    // hit the player location
    strikeLocation();
    // strike complete, reset params
    strike = (int)StrikeState.Start;
    is_tracking = false;
  }

  // track players last location
  private void trackPlayer()
  {
    if(is_tracking == false)
    {
      p1_pos = Player1.GetComponent<Transform>().position;
      p2_pos = Player2.GetComponent<Transform>().position;
      is_tracking = true;
      // choose a random player
      System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);
      player_to_hit = rnd.Next(0,2);
      Debug.Log("Player " + player_to_hit + " is being targeted by lightning!!");
      StartCoroutine(waitToLight());
    }
  }  

  // rng timer to throw out lightning
  private IEnumerator waitToLightningStrike()
  {
    // strike started
    strike = (int)StrikeState.Wait;
    // random time to wait
    System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);
    float cur_time = (float) rnd.NextDouble() * max_time_strike + min_time_strike;
    Debug.Log("waitToLightningStrike() time to wait: " + cur_time);
    // waiting
    while(cur_time > 0)
    {
      cur_time -= Time.deltaTime;
      yield return null;
    }
    strike = (int)StrikeState.Strike;
    Debug.Log("waitToLightningStrike() wait complete ");

  }

  // spawn a lightning to hit the players last location
  private void strikeLocation()
  {
    Vector3 pos;
    // get player to hit position
    if(player_to_hit == (int) PlayerNum.One)
      pos = p1_pos;
    else
      pos = p2_pos;
    // spawn a lightning here
    GameObject lightning = Instantiate(gameObject, pos + new Vector3(-1.0f,2.0f,0.0f), gameObject.transform.rotation) as GameObject;
    lightning.GetComponent<Lightning>().startLightning();
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

    //Debug.Log("Lightning P1 dist: " + player1_dist);
    //Debug.Log("Lightning P2 dist: " + player2_dist);
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

  // cc player and set player on fire if lightning contacts

  // Update is called once per frame
  void Update()
  {
    // animate the lightning
    if(Time.time >= anim_start_time + time_to_anim)
    {
      anim_start_time = Time.time;
      showSingleMesh(nextMesh()); 
    }
    // non spawned lightning will generate the RNG script
    if(spawned_lightning == false)
    {
      // strike start trigger, wait for some time before striking
      if(strike == (int)StrikeState.Start)
        StartCoroutine(waitToLightningStrike());
      else if(strike == (int)StrikeState.Strike)
        trackPlayer();
    }
    // spawned lightning searches for closest player
    else
    {
      // only hit one player
      if(hit_player == false && dev_disable_harmful == false)
      {
        GameObject closest_player = findClosestPlayerInDist();
        if(closest_player != null)
        {
          hit_player = true;
          Debug.Log("Closest player found by lightning: " + closest_player.tag);
          closest_player.GetComponent<PlayerControl>().HitByObject();
          closest_player.GetComponent<PlayerControl>().SetOnFire();
        }
      }
    }
  }
}
