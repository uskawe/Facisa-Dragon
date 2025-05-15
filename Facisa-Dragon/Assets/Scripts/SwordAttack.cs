using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string nomeDaAnimacao = "Attack";
    [SerializeField] private int dano = 20;

    private bool podeCausarDano = false;

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            animator.Play(nomeDaAnimacao, 0, 0f);
            podeCausarDano = true;
            StartCoroutine(DesativarDanoDepoisDe(0.5f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!podeCausarDano) return;

        if (other.CompareTag("Enemy"))
        {
            EnemyDamage enemy = other.GetComponent<EnemyDamage>();
            if (enemy != null)
            {
                enemy.AplicarDano(dano);
                podeCausarDano = false; // Evita múltiplos danos no mesmo ataque
            }
        }
    }

    private IEnumerator DesativarDanoDepoisDe(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        podeCausarDano = false;
    }
}
