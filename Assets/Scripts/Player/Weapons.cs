using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField]
    public GameObject Pistol;
    private bool pistolEquipped = false;
    public Makrov makrov;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void equipPistol()
    {
        if(pistolEquipped)
        {
            //unequip pistol
            Pistol.SetActive(false);
            print("disabling");
            pistolEquipped = false;
        }else{
            Pistol.SetActive(true);
            print("enabling");
            pistolEquipped = true;
        }
    }
}
