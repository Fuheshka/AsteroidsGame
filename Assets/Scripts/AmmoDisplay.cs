using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    public int LaserAmmo;
    public bool isFiring;

    private void Update()
    {
        if ((Input.GetMouseButtonDown(1) || Input.GetKey(KeyCode.X)) && !isFiring && LaserAmmo > 0)
        {
            isFiring = true;
            LaserAmmo--;
            isFiring = false;
        }
    }
}
