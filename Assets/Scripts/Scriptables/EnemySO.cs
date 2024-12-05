using UnityEngine;

[CreateAssetMenu(fileName = "EnemyColorScriptableObject", menuName = "Enemies/Enemy")]

public class EnemySO : ScriptableObject
{
    public string enemyType;

    public EnemyColorSO enemyColorSO;

    public Sprite sprite;
}
