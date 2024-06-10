using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter()
    {
       Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
