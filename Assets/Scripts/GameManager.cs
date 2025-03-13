using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private VRInputManager vrInputManagerPrefab;
    [SerializeField]
    private KeyboardMouseInputManager keyboardMouseInputManager;
    [SerializeField]
    private bool useKeyboardMouse = false;
    [SerializeField]
    private LoadingScreen loadingScreen;
    //   [SerializeField]
    //   private TextMeshProUGUI scoreBoarde; // This is the TextMeshPro component for displaying the score
    public bool gamehasbinstarted = false;

    public static GameManager Instance { get; private set; } // Singleton
    public IInputProvider InputProvider { get; private set; }
    public LoadingScreen LoadingScreen => loadingScreen;
    public int score = 0;
    private bool inTransition = false;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Prevent duplicate GameManager instances
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

#if !UNITY_EDITOR
		useKeyboardMouse = false;
#endif
        if (useKeyboardMouse)
        {
            InputProvider = Instantiate(keyboardMouseInputManager, transform);
        }
        else
        {
            InputProvider = Instantiate(vrInputManagerPrefab, transform);
        }

        InputProvider.TryInitialize();
        loadingScreen.Initialize(InputProvider.GetHeadTransform());
    }
/*    public void Update()
    {
        // Display the score from ExplodeOnTouch
        scoreBoarde.text = $"Score:" + score; // Display updated score
    }*/
    public Coroutine GoToGameScene()
    {

        return StartCoroutine(LoadScene("Game", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<GameSceneController>();
            sceneController.Initialize(this);
        }));
    }

    public Coroutine GoToGameExampleScene()
    {
        return StartCoroutine(LoadScene("Game_Example", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<ExampleGameSceneController>();
            sceneController.Initialize(this);
        }));
    }

    public Coroutine GoToMenuScene()
    {
        return StartCoroutine(LoadScene("Menu", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<MenuSceneController>();
            sceneController.Initialize(this);
        }));
    }

    private IEnumerator LoadScene(string sceneName, Action sceneLoadedCallback)
    {
        if(inTransition)
        {
            yield break;//Exit!
        }

        inTransition = true;
        yield return loadingScreen.Show();

        yield return SceneManager.LoadSceneAsync(sceneName);
        sceneLoadedCallback?.Invoke();

        yield return loadingScreen.Hide();
        inTransition = false;
    }

    public static GameManager BootstrapFromEditor()
    {
#if UNITY_EDITOR
        var gameManagerPrefab = Resources.Load<GameManager>("GameManager");
        var gameManager = Instantiate(gameManagerPrefab);
        return gameManager;
#else
        throw new NotImplementedException();
#endif
    }
}

