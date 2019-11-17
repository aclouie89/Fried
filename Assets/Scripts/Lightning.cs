using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LightningSprite {One = 0, Two, Three, Four, Five, Six, Seven, Eight, Kill}

public class Lightning : MonoBehaviour
{
  private bool spawned_lightning = false;
  private Material[] lightning_mat;
  private int last_mesh_index = (int) LightningSprite.Two;
  private int cur_lightning = (int) LightningSprite.One;

  // animation times
  private float time_to_anim = 0.1f;
  private float anim_start_time;

  // player tracking
  private bool is_tracking = false;
  private float time_to_light = 0.75f;
  private int player_to_hit = (int) PlayerNum.None;

  // players
  private GameObject Player1;
  private GameObject Player2;
  private Vector3 p1_pos;
  private Vector3 p2_pos;

  void startLightning()
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
      if(spawned_lightning)
        Destroy(gameObject);
      else
        cur_lightning = (int) LightningSprite.One;
    return cur_lightning;
  }

  // new random index
  private int randInt(int max)
  {
    System.Random rnd = new System.Random((int)Time.time);
    int rnd_index = rnd.Next(max);
    // Debug.Log("Random int chosen: " + rnd_index + " with a max of: " + max);

    return rnd_index;
  }

  // coroutine to to wait for player to move
  private IEnumerator waitToLight()
  {
    float cur_time = time_to_light;
    while(cur_time > 0)
    {
      cur_time -= Time.deltaTime;
      yield return null;
    }
    is_tracking = false;
  }

  // track players last location
  private void trackPlayer()
  {
    if(is_tracking == false)
    {
      // Debug.Log("trackPlayer()");
      p1_pos = Player1.GetComponent<Transform>().position;
      p2_pos = Player2.GetComponent<Transform>().position;
      is_tracking = true;
      // choose a random player
      player_to_hit = randInt((int) PlayerNum.Two);
      StartCoroutine(waitToLight());
    }
  }  

  // rng timer to throw out lightning

  // spawn a lightning to hit a player

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
      trackPlayer();
    }
  }
}
