using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction anyKeyAction;

    [Header("UI Enable")]
    [SerializeField] private GameObject mainMenu;

    [Header("UI Disable")]
    [SerializeField] private GameObject initialMenu;

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 1f;

    private CanvasGroup mainMenuGroup;
    private CanvasGroup initialMenuGroup;

    void Awake()
    {
        var menuMap = inputActions.FindActionMap("Menu");
        anyKeyAction = menuMap.FindAction("AnyKey");

        anyKeyAction.performed += ctx => ShowMenuUI();
        anyKeyAction.Enable();

        mainMenuGroup = mainMenu.GetComponent<CanvasGroup>();
        initialMenuGroup = initialMenu.GetComponent<CanvasGroup>();

        mainMenu.SetActive(true);
        mainMenuGroup.alpha = 0f;
        mainMenuGroup.interactable = false;
        mainMenuGroup.blocksRaycasts = false;

        initialMenu.SetActive(true);
        initialMenuGroup.alpha = 1f;
    }

    void ShowMenuUI()
    {
        StartCoroutine(FadeOutInitialThenFadeInMain());
        anyKeyAction.Disable();
    }

    IEnumerator FadeOutInitialThenFadeInMain()
    {
        float time = 0f;

        while (time < transitionDuration)
        {
            float t = time / transitionDuration;
            if (initialMenuGroup != null)
                initialMenuGroup.alpha = 1f - t;

            time += Time.deltaTime;
            yield return null;
        }

        if (initialMenuGroup != null)
        {
            initialMenuGroup.alpha = 0f;
            initialMenuGroup.interactable = false;
            initialMenuGroup.blocksRaycasts = false;
            initialMenu.SetActive(false);
        }

        mainMenu.SetActive(true);
        time = 0f;

        while (time < transitionDuration)
        {
            float t = time / transitionDuration;
            if (mainMenuGroup != null)
                mainMenuGroup.alpha = t;

            time += Time.deltaTime;
            yield return null;
        }

        if (mainMenuGroup != null)
        {
            mainMenuGroup.alpha = 1f;
            mainMenuGroup.interactable = true;
            mainMenuGroup.blocksRaycasts = true;
        }
    }

    void OnDestroy()
    {
        anyKeyAction.Disable();
    }
}
