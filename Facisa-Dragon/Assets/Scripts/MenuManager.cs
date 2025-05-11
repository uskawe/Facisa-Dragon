using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public InputActionAsset inputActions; 
    private InputAction anyKeyAction;

    void Awake()
    {
        var menuMap = inputActions.FindActionMap("Menu");
        anyKeyAction = menuMap.FindAction("AnyKey");

        anyKeyAction.performed += ctx => ChangeScene();
        anyKeyAction.Enable();
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    void OnDestroy()
    {
        anyKeyAction.Disable();
    }
}
