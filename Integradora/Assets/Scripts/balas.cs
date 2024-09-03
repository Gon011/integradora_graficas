using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balas : MonoBehaviour
{
    public float limx = 25f;
    public float limz = 25f;
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -limx || transform.position.x > limx
        || transform.position.z < -limz || transform.position.z > limz)
        {
            Destroy(gameObject);
        }
    }
}
