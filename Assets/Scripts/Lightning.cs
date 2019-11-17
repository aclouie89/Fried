using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LightningSprite {One = 0, Two, Three, Four, Five, Six, Seven, Eight}

public class Lightning : MonoBehaviour
{
  private bool spawned_lightning = false;
  private float time_alive = 30.0f;
  private Material[] lightning_mat;
  private int last_mesh_index = (int) LightningSprite.Two;
  private int cur_lightning = (int) LightningSprite.One;

  private float time_to_anim = 0.15f;
  private float anim_start_time;

  private float live_time;

  void startLightning()
  {
    spawned_lightning = true;
    live_time = Time.time;
  }

  // Start is called before the first frame update
  void Start()
  {
    lightning_mat = GetComponent<Renderer>().materials;
    // set up sprite
    for(int x = 0; x < lightning_mat.Length; x++)
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
    if(cur_lightning == (int)LightningSprite.Five)
      cur_lightning = (int)LightningSprite.One;
    else
      cur_lightning++;
    return cur_lightning;
  }

  // Update is called once per frame
  void Update()
  {
    // animate the lightning
    if(Time.time >= anim_start_time + time_to_anim)
    {
      anim_start_time = Time.time;
      showSingleMesh(nextMesh()); 
    }
    // destroy spawned lightning
    if(spawned_lightning)
      if(Time.time >= live_time + time_alive)
      {
        Debug.Log("Lightning destroying self");
        Destroy(gameObject);
      }
  }
}
