using System;
using UnityEngine;

[Serializable]
public class TurretBuilder
{
    public string name;
    public GameObject prefab;
    public int cost;

    public TurretBuilder (string _name, GameObject _prefab, int _cost)
    {
        name = _name;
        prefab = _prefab;
        cost = _cost;
    }
}
