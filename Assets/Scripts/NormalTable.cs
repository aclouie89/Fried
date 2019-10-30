using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTable : MonoBehaviour
{
  GameObject item = null;
  float min_dist = 1.0f;

  // Start is called before the first frame update
  void Start()
  {
      
  }

  public GameObject isOnTable()
  {
    if(item != null)
      Debug.Log("isOnTable " + item.tag);
    return item;
  }

  public void putOnTable(GameObject new_item)
  {
    Debug.Log("putOnTable " + new_item.tag);
    item = new_item;
  }

  public void removeOnTable()
  {
    Debug.Log("removeOnTable " + item.tag);
    item = null;
  }


  // periodically check if there's an item on top just in case it bugs out
  private bool objectOnTop()
  {
    // doesnt exist
    if(item == null)
      return false;
    // check distance, if it's far away return false
    Vector3 position = transform.position;
    Vector3 diff = item.GetComponent<Transform>().position - position;
    float curDistance = diff.sqrMagnitude;
    if(curDistance > min_dist)
      return false;
    return true;
  }
  // Update is called once per frame
  void Update()
  {
    // Commented out for performance, maybe we want this in the future?
    // objectOnTop() min_dist needs to be configured properly
    /*if(item != null)
      if(!objectOnTop())
        item = null;*/
  }
}
