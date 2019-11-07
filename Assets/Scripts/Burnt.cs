using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnt : MonoBehaviour
{
  public Material burned;
  bool burnt = false;
  private void im_burnt()
  {
    burnt = true;
  }

  // Start is called before the first frame update
  void Start()
  {
      
  }

  // Update is called once per frame
  void Update()
  {
    if(burnt)
    {
      //gameObject.set
    }
  }
}
