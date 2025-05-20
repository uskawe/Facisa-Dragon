using UnityEngine;
using UnityEngine.InputSystem;

public class PlayAnimationOnClick : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string nomeDaAnimacao = "Attack"; // ou qualquer nome que você queira

    // Esse método será chamado automaticamente pelo PlayerInput no modo Send Messages
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!gameObject.activeInHierarchy || !enabled) return;

        if (context.performed)
        {
            animator.Play(nomeDaAnimacao, 0, 0f);
        }
    }
}
