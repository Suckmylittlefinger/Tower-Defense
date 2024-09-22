using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private GameObject[] towers; //Array of towers that can be built

    private int towerSelected = 0; //Index to keep track of the currently selected tower

    private void Awake()
    {
        main = this;
    }

    //Returns the currently selected tower prefab to be built
    public GameObject GetTowerSelected()
    {
        return towers[towerSelected]; //Select the tower based on the selected index
    }

}
