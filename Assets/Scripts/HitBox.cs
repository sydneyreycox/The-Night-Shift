using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Player papa; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(papa.attacking && AcceptHeight(papa.attackHeight, enemy.type))
        {
            enemy.Die();
            Debug.Log("Enemy hit by attack");
            papa.IncreaseScore();
        }
    }

    bool AcceptHeight(EnemyType player, EnemyType enemy)
    {
        return enemy == EnemyType.Normal || player == enemy;
    }
}
