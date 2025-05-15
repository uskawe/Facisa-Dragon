using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private Image barraDeVidaImage;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + cameraTransform.forward);
    }

    public void AlterarBarraDeVida(int vidaAtual, int vidaMaxima)
    {
        barraDeVidaImage.fillAmount = (float)vidaAtual / vidaMaxima;
    }
}
