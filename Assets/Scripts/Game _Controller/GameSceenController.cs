using System.Collections;
using TMPro;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private BouncyBall bouncyBallPrefab;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private int spawnCount;
    [SerializeField]
    private float gameDuration;
    [SerializeField]
    private TextMeshPro timeLeftLabel;

    [SerializeField]
    private ParticleSystem leftHandParticleSystem;
    [SerializeField]
    private ParticleSystem rightHandParticleSystem;
    [SerializeField]
    private LaserPointer leftLaserPointer; // Laser for the left hand
    [SerializeField]
    private LaserPointer rightLaserPointer; // Laser for the right hand
    [SerializeField]
    private AudioClip shootingsound;
    //   [SerializeField]
    // private MenuSceneController menuSceneController;

    private GameManager gameManager;
    private IInputProvider inputProvider;
    private IControllerInput leftController;
    private IControllerInput rightController;
    private float startTime;
    //    public ExplodeOnTouch explodeOnTouch; // Reference to the ExplodeOnTouch script
    [SerializeField]
    private TextMeshPro scoreBoardText;
    private void Start()
    {
        // Ensure GameManager is initialized
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
            {
#if UNITY_EDITOR
                gameManager = GameManager.BootstrapFromEditor();
#else
                Debug.LogError("GameManager instance not found!");
                return;
#endif
            }
            Initialize(gameManager);
            /* menuSceneController = FindObjectOfType<MenuSceneController>();
             if (menuSceneController != null)
             {
                 Debug.Log("GOZO");
                 menuSceneController.gamehasbinstarted = true; 
             }
             else
             { Debug.Log("Menue Scene controller not found"); }*/
            
        }

        // Ensure laser pointers follow their respective controllers
        InitializeLaserPointers();
        SetupParticleSystems();
    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
        inputProvider = gameManager.InputProvider;
        leftController = inputProvider.GetLeftController();
        rightController = inputProvider.GetRightController();

        startTime = Time.time;
        StartCoroutine(GameSequence());

    }

    private void InitializeLaserPointers()
    {
        if (leftLaserPointer != null)
        {
            leftLaserPointer.Initialize(leftController);
        }
        else
        {
            Debug.LogWarning("Left laser pointer is not assigned!");
        }

        if (rightLaserPointer != null)
        {
            rightLaserPointer.Initialize(rightController);
        }
        else
        {
            Debug.LogWarning("Right laser pointer is not assigned!");
        }
    }

    private void SetupParticleSystems()
    {
        // Attach left-hand particle system to the left controller
        if (leftHandParticleSystem != null && leftController != null)
        {
            leftHandParticleSystem.transform.SetParent(leftController.GetTransform());
            leftHandParticleSystem.transform.localPosition = Vector3.zero;
            leftHandParticleSystem.transform.localRotation = Quaternion.identity;
        }

        // Attach right-hand particle system to the right controller
        if (rightHandParticleSystem != null && rightController != null)
        {
            rightHandParticleSystem.transform.SetParent(rightController.GetTransform());
            rightHandParticleSystem.transform.localPosition = Vector3.zero;
            rightHandParticleSystem.transform.localRotation = Quaternion.identity;
        }
    }

    private IEnumerator GameSequence()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(spawnDelay);

            var ball = Instantiate(bouncyBallPrefab,
                new Vector3(Random.Range(-10f, 10f), 10f, Random.Range(-10f, 10)),
                Quaternion.identity);

            var explode = ball.GetComponent<ExplodeOnTouch>();
            explode.Initialize(gameManager);

            if (i % 5 == 0)
            {
                ball.transform.localScale = Vector3.one * Random.Range(2.4f, 3f);
            }
            else
            {
                ball.transform.localScale = Vector3.one * Random.Range(0.4f, 1f);
            }
        }
    }

    private void Update()
    {
        ApplyJoystickMovement();

        // Emit particles when triggers are pressed
        if (leftController != null && leftController.IsTriggerPressed())
        {
            leftHandParticleSystem.Emit(1);
            soundFXmanager.instance.PlaySoundFXClip(shootingsound, transform, 1f);
        }

        if (rightController != null && rightController.IsTriggerPressed())
        {
            rightHandParticleSystem.Emit(1);
            soundFXmanager.instance.PlaySoundFXClip(shootingsound, transform, 1f);
        }

        // Update the game timer
        float elapsed = Time.time - startTime;
        float timeLeft = gameDuration - elapsed;
        if (timeLeft < 0)
        {
            gameManager.GoToMenuScene();
            timeLeftLabel.text = "Game Over";
        }
        else
        {
            timeLeftLabel.text = $"Time left: {timeLeft:0.00}";
        }
        if (gameManager != null && scoreBoardText != null)
            scoreBoardText.text = "Score=" + gameManager.score;

    }

    private void ApplyJoystickMovement()
    {
        var forward = inputProvider.GetHeadTransform().forward;
        forward.y = 0f;
        forward.Normalize();

        var right = inputProvider.GetHeadTransform().right;
        right.y = 0f;
        right.Normalize();

        forward *= rightController.Joystick.y;
        right *= rightController.Joystick.x;

        var movement = forward + right;
        movement *= movementSpeed * Time.deltaTime;

        inputProvider.GetRigTransform().position += movement;
    }
    private void FixedUpdate()
    {
        gameManager.gamehasbinstarted = true;
    }
}  

    
