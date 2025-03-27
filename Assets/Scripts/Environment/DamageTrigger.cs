using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private int damageAmount = 5;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //take damage
            Debug.Log("Player took damage");
            collision.GetComponent<Player>().TakeDamage(damageAmount);
        }
    }    
}
