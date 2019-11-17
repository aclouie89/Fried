using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player facing
enum ThrownOrientation {North = 0, East, South, West}

public class Projectile : MonoBehaviour
{
  /* DEBUG levels
   * 1 - Actions only
   * 2 - Search level
   * 3 - Verbose
   * 4 - Hyperverbose
   */
  int DEBUG = 1;
  private bool thrown = false;
  private int orientation;
  private Vector3 orig_loc;
  private float dist_to_travel = 200.0f;
  private bool spinning = false;

  // projectile rotation
  private float spin_time = 3.0f;
  private float rotateSpeed = 900.0f;

  // player tag
  private string player_tag;

  // movement for held items
  private float velocity = 44.0f;
  private float smoothTime = 2.0f;
  private Vector3 AVelocity = Vector3.zero;
  private float final_y = 3.0f;
  
  void dbgprint(int level, string text)
  {
    if(DEBUG >= level)
      Debug.Log(text);
  }

  // Start is called before the first frame update
  void Start()
  {
  }
  
  void setUpProjectile()
  {
    // set orientation modifiers
    float n_mod, e_mod, s_mod, w_mod;
    n_mod = e_mod = s_mod = w_mod = 0.0f;
    float offset = 1.0f;
    if(orientation == (int)ThrownOrientation.North)
    {
      n_mod = offset;
    }
    if(orientation == (int)ThrownOrientation.East)
    {
      e_mod = offset * 1.5f;
    }
    if(orientation == (int)ThrownOrientation.South)
    {
      s_mod = -offset / 1f;
    }
    if(orientation == (int)ThrownOrientation.West)
    {
      w_mod = -offset * 1.5f;
    }
    float x_part = e_mod + w_mod;
    float z_part = n_mod + s_mod;
    dbgprint(3, "orientation: " + orientation.ToString());
    dbgprint(3, "setup x part: " + x_part.ToString());
    dbgprint(3, "setup z part: " + z_part.ToString());
    // for now just snap to Y coordinate
    // will dampen in the future
    gameObject.transform.position = new Vector3(transform.position.x + x_part, final_y, transform.position.z + z_part);
  }

  // sets up throwing values
  public void setThrown(int face, string tag)
  {
    player_tag = tag;
    thrown = true;
    orientation = face;
    setUpProjectile();
    orig_loc = transform.position;
  }

  // hits an object
  void OnTriggerEnter (Collider other) 
  {
    if(thrown)
    {
      // hit a player
      if(other.tag == player_tag)
      {
        dbgprint(3, "Thrown object hit: " + other.tag.ToString());
        GameObject player = GameObject.FindWithTag(other.tag);
        var link_player = player.GetComponent<PlayerControl>();
        link_player.HitByObject();
      }
      else
      {
        dbgprint(3, "Thrown object hit an object: " + other.tag.ToString());
      }
      dbgprint(3, "Destroying self: " + gameObject.tag);
      Destroy(gameObject);
    }
  }

  // spin the object, take the wheels jesus
  private IEnumerator spinObject()
  {
    float cur_time = spin_time;
    while(cur_time > 0)
    {
      transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
      cur_time -= Time.deltaTime;
      yield return null;
    }
    spinning = false;
    transform.rotation = Quaternion.identity;
  }

  // move an object
  void moveObject()
  {
    // set orientation modifiers
    float n_mod, e_mod, s_mod, w_mod;
    n_mod = e_mod = s_mod = w_mod = 0.0f;
    if(orientation == (int)ThrownOrientation.North)
    {
      n_mod = 1.0f;
      s_mod = 1.0f;
    }
    else if(orientation == (int)ThrownOrientation.East)
    {
      e_mod = 1.0f;
      w_mod = 1.0f;
    }
    else if(orientation == (int)ThrownOrientation.South)
    {
      n_mod = 1.0f;
      s_mod = -1.0f;
    }
    else if(orientation == (int)ThrownOrientation.West)
    {
      e_mod = 1.0f;
      w_mod = -1.0f;
    }
  
    // move object
    float x_part = velocity * e_mod * w_mod;
    float z_part = velocity * n_mod * s_mod;
    dbgprint(5, "X_part: " + x_part.ToString());
    dbgprint(5, "Z_part: " + z_part.ToString());
    transform.position = Vector3.SmoothDamp(transform.position, transform.position + new Vector3(x_part, 0f, z_part), ref AVelocity, smoothTime);
  }

  // Update is called once per frame
  void Update()
  {
    // if thrown
    if(thrown)
    {
      if(!spinning)
      {
        spinning = true;
        StartCoroutine(spinObject());
      }
      //Debug.Log("Location: " + transform.position.ToString());
      Vector3 diff = transform.position - orig_loc;
      // still needs to travel
      if(dist_to_travel > diff.sqrMagnitude)
      {
        dbgprint(4, "Distance traveled: " + diff.sqrMagnitude.ToString());
        moveObject();
      }
      else
      {
        dbgprint(3, "Done throwing");
        thrown = false;
        Destroy(gameObject);
      }
    }
  }
}
