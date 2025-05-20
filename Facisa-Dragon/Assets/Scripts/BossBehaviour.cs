using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform alvo;
    [SerializeField] private Animator animator;

    [Header("Configuração")]
    [SerializeField] private float distanciaDeAtaque = 3f;
    [SerializeField] private float velocidade = 3f;
    [SerializeField] private string nomeAnimacaoAndando = "Walk";
    [SerializeField] private string nomeAnimacaoAtaque = "Attack";
    [SerializeField] private float duracaoAnimacaoAtaque = 2f;

    private bool estaAtacando = false;

    void Update()
    {
        if (alvo == null || estaAtacando) return;

        float distancia = Vector3.Distance(transform.position, alvo.position);

        if (distancia <= distanciaDeAtaque)
        {
            StartCoroutine(TocarAnimacaoDeAtaque());
        }
        else
        {
            // Mover em direção ao jogador
            Vector3 direcao = (alvo.position - transform.position).normalized;
            transform.position += direcao * velocidade * Time.deltaTime;

            // Tocar animação de andar
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(nomeAnimacaoAndando))
            {
                animator.Play(nomeAnimacaoAndando);
            }

            // Virar para o jogador
            transform.LookAt(new Vector3(alvo.position.x, transform.position.y, alvo.position.z));
        }
    }

    private System.Collections.IEnumerator TocarAnimacaoDeAtaque()
    {
        estaAtacando = true;
        animator.Play(nomeAnimacaoAtaque);

        yield return new WaitForSeconds(duracaoAnimacaoAtaque);

        estaAtacando = false;
    }
}
