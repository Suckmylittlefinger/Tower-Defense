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

        // Get the selected turret from the BuildManager
        TurretBuilder turretToBuild = BuildManager.main.GetTurretSelected();

        // Check if there is enough money to buy the turret
        if (LevelManager.main.SpendMoney(turretToBuild.cost))
        {
            // Instantiate the turret if purchase was successful
            turret = Instantiate(turretToBuild.prefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Not enough money to build turret.");
        }

        
        //For testing
        Debug.Log("Tile clicked" + name);
    }
}
