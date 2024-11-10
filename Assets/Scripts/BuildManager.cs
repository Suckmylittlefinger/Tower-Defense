using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private TurretBuilder[] turrets; //Array of turrets that can be built from TurretBuilder class

    private int turretSelected = 0; //Index to keep track of the currently selected turret

    private void Awake()
    {
        main = this; //Makes sure there is only one instance of BuildManager and makes it more easily accessible in other scripts
    }

    //Returns the currently selected turret prefab to be built
    public TurretBuilder GetTurretSelected()
    {
        return turrets[turretSelected]; //Select the turret based on the selected index
    }

    // For changing tower selected using UI
    public void SetTurretSelected(int _turretSelected)
    {
        turretSelected = _turretSelected;
    }

}
