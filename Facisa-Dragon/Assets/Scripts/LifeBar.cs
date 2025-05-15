using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private Image barraDeVidaImage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AlterarBarraDeVida(int vidaAtual, int vidaMaxima)
    {
        barraDeVidaImage.fillAmount = (float)vidaAtual / vidaMaxima;
    }
}
