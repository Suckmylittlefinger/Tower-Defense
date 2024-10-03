using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject turret; //Reference to the turret currently placed on this tile (null if no turret is placed)

    private void OnMouseDown()
    {
        if (turret != null) //Prevents placing a turret on another turret
        {
            return;
        }

        TurretBuilder turretToBuild = BuildManager.main.GetTurretSelected(); //Gets the currently selected turret from the BuildManager
        turret = Instantiate(turretToBuild.prefab, transform.position, Quaternion.identity); //Instantiates the turret on this tile at the tile's position

        Debug.Log("Tile clicked" + name);
    }
}
