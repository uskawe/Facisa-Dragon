using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private int vidaAtual;
    private int vidaTotal = 100;

    [SerializeField] private LifeBar lifeBar;
    [SerializeField] private GameObject objetoParaAtivarDepoisDaMorte;
    [SerializeField] private GameObject[] hudsParaDesativar;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float tempoEntreAtaques = 2f;

    private float tempoDoUltimoAtaque;

    private void Start()
    {
        vidaAtual = vidaTotal;
        lifeBar.AlterarBarraDeVida(vidaAtual, vidaTotal);

        if (objetoParaAtivarDepoisDaMorte != null)
        {
            Renderer renderer = objetoParaAtivarDepoisDaMorte.GetComponent<Renderer>();
            CanvasGroup canvasGroup = objetoParaAtivarDepoisDaMorte.GetComponent<CanvasGroup>();

            if (renderer != null)
                renderer.enabled = false;

            if (canvasGroup != null)
                canvasGroup.alpha = 0f;

            objetoParaAtivarDepoisDaMorte.SetActive(false);
        }
    }

    public void AplicarDano(int dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaTotal);
        lifeBar.AlterarBarraDeVida(vidaAtual, vidaTotal);

        if (vidaAtual <= 0)
        {
            foreach (GameObject hud in hudsParaDesativar)
            {
                if (hud != null)
                    hud.SetActive(false);
            }

            if (objetoParaAtivarDepoisDaMorte != null)
            {
                objetoParaAtivarDepoisDaMorte.SetActive(true);

                CanvasGroup canvasGroup = objetoParaAtivarDepoisDaMorte.GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    StartCoroutine(FazerFadeIn(canvasGroup));
                }
                else
                {
                    Renderer renderer = objetoParaAtivarDepoisDaMorte.GetComponent<Renderer>();
                    if (renderer != null)
                        renderer.enabled = true;
                }
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator FazerFadeIn(CanvasGroup canvasGroup)
    {
        float tempo = 0f;

        while (tempo < fadeDuration)
        {
            tempo += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, tempo / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TentarAtacarJogador(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TentarAtacarJogador(other);
        }
    }

    private void TentarAtacarJogador(Collider other)
    {
        if (Time.time >= tempoDoUltimoAtaque + tempoEntreAtaques)
        {
            Damage damageScript = other.GetComponent<Damage>();
            if (damageScript != null)
            {
                damageScript.AplicarDano(10);

                Vector3 direcaoKnockback = other.transform.position - transform.position;
                damageScript.ReceberKnockback(direcaoKnockback, 5f); // força = 5
                tempoDoUltimoAtaque = Time.time;
            }
        }
    }
}
