using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject tower; //Reference to the tower currently placed on this tile (null if no tower is placed)

    private void OnMouseDown()
    {
        if (tower != null) //Prevents placing a tower on another tower
        {
            return;
        }

        GameObject towerToBuild = BuildManager.main.GetTowerSelected(); //Gets the currently selected tower from the BuildManager
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity); //Instantiates the tower on this tile at the tile's position

        Debug.Log("Tile clicked" + name);
    }
}
