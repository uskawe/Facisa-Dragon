using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Novo sistema de Input

public class Damage : MonoBehaviour
{
    private int vidaAtual;
    private int vidaTotal = 100;

    [SerializeField] private LifeBar lifeBar;

    private void Start()
    {
        vidaAtual = vidaTotal;
        lifeBar.AlterarBarraDeVida(vidaAtual, vidaTotal);
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            AplicarDano(10);
        }
    }

    private void AplicarDano(int dano)
    {
        vidaAtual -= dano;
        lifeBar.AlterarBarraDeVida(vidaAtual, vidaTotal);
    }
}
