using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    public void ReceberKnockback(Vector3 direcao, float forca)
    {
        knockbackVelocidade = direcao.normalized * forca;
        knockbackTimer = knockbackDuracao;
    }
}
