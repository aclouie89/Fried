using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum KnifeSprite {One = 0, Two, Three, Four}
public class Chopping : MonoBehaviour
{
    // knife aniations
    private Material[] knives;
    private int cur_knife;
    private int last_mesh_index;
    private float anim_start_time;
    private float time_to_anim = 0.025f;

    // Start is called before the first frame update
    void Start()
    {
      cur_knife = (int) KnifeSprite.One;
      last_mesh_index = (int) KnifeSprite.Four;
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

    public void chopComplete()
    {
      Destroy(gameObject);
    }

    private int nextMesh()
    {
      cur_knife++;
      // kill spawned lightning here
      // non spawned lightning must set the sprite back to one
      if(cur_knife > (int)KnifeSprite.Four)
        cur_knife = (int) KnifeSprite.One;

      return cur_knife;
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
