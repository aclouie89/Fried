using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapping : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite WholeVeg;
    public Sprite ChoppedVeg;

    float timer = 1f;
    float delay = 1f;

    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = WholeVeg;    
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == WholeVeg) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = ChoppedVeg;
                timer = delay;
            }
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == WholeVeg)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = ChoppedVeg;
                timer = delay;
            }
        }
        
    }
}
