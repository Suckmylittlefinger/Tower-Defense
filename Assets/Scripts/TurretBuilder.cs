using System;
using UnityEngine;

[Serializable]
public class TurretBuilder
{
    public string name;
    public GameObject prefab;

    public TurretBuilder (string _name, GameObject _prefab)
    {
        name = _name;
        prefab = _prefab;
    }
}
