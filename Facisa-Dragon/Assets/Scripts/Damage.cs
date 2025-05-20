using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    private int vidaAtual;
    private int vidaTotal = 100;

    [SerializeField] private LifeBar lifeBar;

    private CharacterController characterController;
    private Vector3 knockbackVelocidade;
    private float knockbackDuracao = 0.2f;
    private float knockbackTimer;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        vidaAtual = vidaTotal;
        lifeBar.AlterarBarraDeVida(vidaAtual, vidaTotal);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Aqui você pode testar dano, se quiser
            // AplicarDano(10);
        }
    }

    private void FixedUpdate()
    {
        if (knockbackTimer > 0)
        {
            characterController.Move(knockbackVelocidade * Time.fixedDeltaTime);
            knockbackTimer -= Time.fixedDeltaTime;
        }
    }

    public void AplicarDano(int dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaTotal);
        lifeBar.AlterarBarraDeVida(vidaAtual, vidaTotal);

        if (vidaAtual <= 0)
        {
            StartCoroutine(ReiniciarCenaDepoisDe(3f)); // Espera 3 segundos
        }
    }

    public void ReceberKnockback(Vector3 direcao, float forca)
    {
        knockbackVelocidade = direcao.normalized * forca;
        knockbackTimer = knockbackDuracao;
    }

    private IEnumerator ReiniciarCenaDepoisDe(float tempo)
    {
        // Aqui você pode desativar o controle do jogador se quiser
        yield return new WaitForSeconds(tempo);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
