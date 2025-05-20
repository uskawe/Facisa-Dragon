using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private int dano = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyDamage enemy = other.GetComponent<EnemyDamage>();
            if (enemy != null)
            {
                enemy.AplicarDano(dano);
            }
        }
    }
}
