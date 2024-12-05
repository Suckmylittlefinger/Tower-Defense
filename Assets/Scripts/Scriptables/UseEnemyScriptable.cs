using UnityEngine;

public class UseEnemyScriptable : MonoBehaviour
{
    [SerializeField] EnemySO enemy;
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemy.sprite;
        spriteRenderer.color = enemy.enemyColorSO.color;
    }

    
}
