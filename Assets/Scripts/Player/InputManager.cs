using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerMotor playerMotor;
    public Weapons weapons;
    public Makrov makrov;
     
    void Start()
    {
        weapons = GetComponent<Weapons>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("Jumping");
            playerMotor.Jump();
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            print("Grouching");
            playerMotor.Crouch();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerMotor.Sprint();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weapons.equipPistol();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            makrov.Fire();
            print("FIRING");
        }
    }



}
