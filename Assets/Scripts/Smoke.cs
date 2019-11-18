using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SmokeSprite {One = 0, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Eleven, Twelve}
public class Smoke : MonoBehaviour
{
  private Material[] smoke_mat;
  private int cur_smoke;
  private int last_mesh_index;
  private float anim_start_time;
  private float time_to_anim = 0.05f;

  // Start is called before the first frame update
  void Start()
  {
    cur_smoke = (int) SmokeSprite.One;
    last_mesh_index = (int) SmokeSprite.Twelve;
    anim_start_time = Time.time;
  }

  public void cookComplete()
  {
    Destroy(gameObject);
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
    cur_smoke++;
    // kill spawned lightning here
    // non spawned lightning must set the sprite back to one
    if(cur_smoke > (int)SmokeSprite.Twelve)
      cur_smoke = (int) SmokeSprite.One;

    return cur_smoke;
  }

  // Update is called once per frame
  void Update()
  {
    // animate the smoke
    if(Time.time >= anim_start_time + time_to_anim)
    {
      anim_start_time = Time.time;
      showSingleMesh(nextMesh()); 
    }
  }
}
