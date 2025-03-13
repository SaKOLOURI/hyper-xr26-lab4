using TMPro;
using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField]
    private LaserPointer laserPointer;
    [SerializeField]
    private RaycastTargetButton gameButton;
    [SerializeField]
    private RaycastTargetButton exampleButton;
    [SerializeField]
    private RaycastTargetButton exitButton;
    [SerializeField]
    private RaycastTargetButton InstructionButton;
    [SerializeField]
    private TextMeshPro winorlose;
    [SerializeField]
    private GameObject gameObject;
    private GameManager gameManager;
    private IInputProvider inputProvider;

    private void Start()
    {
        if(gameManager == null)
        {
            gameManager = GameManager.Instance; // Use the existing GameManager instance
            if (gameManager == null)
            {
#if UNITY_EDITOR
                gameManager = GameManager.BootstrapFromEditor(); // Bootstrap if necessary
#else
                Debug.LogError("GameManager instance not found!");
                return;
#endif
            }
            Initialize(gameManager);
        }

        gameButton.OnClickedEvent += OnGameClicked;
        exampleButton.OnClickedEvent += OnExampleClicked;
        exitButton.OnClickedEvent += OnExitClicked;
        InstructionButton.OnClickedEvent += OnInsClicked;

    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;

        inputProvider = gameManager.InputProvider;
        inputProvider.GetRigTransform().position = Vector3.zero;

        laserPointer.Initialize(inputProvider.GetRightController());
    }

    private void OnGameClicked()
    {
        gameManager.score = 0;
        gameManager.GoToGameScene();
    }

    private void OnExampleClicked()
    {
        gameManager.GoToGameExampleScene();

    }
    private void OnExitClicked()
    {
 
        Application.Quit();
    }
    private void OnInsClicked()
    {

        Debug.Log("zer");
        gameObject.SetActive(!gameObject.activeSelf);
    }
    private void Update()
    {
        if (gameManager.gamehasbinstarted == true && gameManager.score > 200)
        { winorlose.text = "YOU WIN"; }
        else if (gameManager.gamehasbinstarted == false)
        { winorlose.text = "ARE YOU READY!"; }
        else
        { winorlose.text = "YOU LOSED"; }
    }

}


