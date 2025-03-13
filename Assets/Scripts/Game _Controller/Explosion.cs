/*
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class ExplodeOnTouch : MonoBehaviour
{
    [SerializeField]
    private GameObject fragmentPrefab; // Prefab of smaller pieces
    [SerializeField]
    private int fragmentCount = 10; // Number of pieces to spawn
    [SerializeField]
    private float explosionForce = 500f; // Force applied to fragments
    [SerializeField]
    private float explosionRadius = 5f; // Radius of the explosion
    [SerializeField]
    private AudioClip explosionSound;

    private bool hasExploded = false;

    // Reference to the RaycastTargetButton (could also be set via Inspector)
    private RaycastTargetButton raycastTargetButton;

    // Score variables
    private GameManager gameManager; // Public score that will be accessed by the GameSceneController
    private int scorePerExplosion = 10; // Points per explosion
    private AudioSource audioSource; // Reference to the AudioSource component
    public void Initialize(GameManager gameManager)
    { 
        this.gameManager = gameManager;
    }

    private void Start()
    {
        // Try to get the RaycastTargetButton component
        raycastTargetButton = GetComponentInParent<RaycastTargetButton>();

        if (raycastTargetButton != null)
        {
            // Subscribe to events if raycastTargetButton is found
            raycastTargetButton.OnHoverEnterEvent += HandleHoverEnter;
            raycastTargetButton.OnClickedEvent += HandleClicked;
        }
        // Get the AudioSource component from the current object (your prefab)
        audioSource = GetComponent<AudioSource>();

        // Check if the AudioSource is not assigned in the Inspector
        if (audioSource == null)
        {
            // If AudioSource is not attached, print a warning
            Debug.LogWarning("AudioSource not found on the object. Please add an AudioSource component.");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events when this object is destroyed
        if (raycastTargetButton != null)
        {
            raycastTargetButton.OnHoverEnterEvent -= HandleHoverEnter;
            raycastTargetButton.OnClickedEvent -= HandleClicked;
        }
    }

    // Handle hover enter
    private void HandleHoverEnter()
    {
        // Optionally, trigger the explosion when hovering (if you want)
        // Explode();  // Uncomment if you want explosion on hover
    }

    // Handle click event
    private void HandleClicked()
    {
        // Trigger the explosion when clicked
        Explode();
    }

    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound); // Plays the sound once
        }
        // Destroy the original object
        Destroy(gameObject);

        // Spawn fragments
        for (int i = 0; i < fragmentCount; i++)
        {
            // Create a fragment at the object’s position with random rotation
            GameObject fragment = Instantiate(
                fragmentPrefab,
                transform.position,
                Random.rotation
            );

            // Add Rigidbody to fragment if not already present
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = fragment.AddComponent<Rigidbody>();
            }

            // Apply explosion force
            rb.AddExplosionForce(
                explosionForce,
                transform.position,
                explosionRadius
            );

            // Optional: Destroy fragments after some time to clean up
            Destroy(fragment, 5f);
        }

        // Add score for this explosion
        gameManager.score+= scorePerExplosion;
        Debug.Log("Score:"+gameManager.score);
    }
}*/
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class ExplodeOnTouch : MonoBehaviour
{
    [SerializeField]
    private GameObject fragmentPrefab; // Prefab of smaller pieces
    [SerializeField]
    private int fragmentCount = 10; // Number of pieces to spawn
    [SerializeField]
    private float explosionForce = 500f; // Force applied to fragments
    [SerializeField]
    private float explosionRadius = 5f; // Radius of the explosion
    [SerializeField]
    private AudioClip[] explosionSounds; // Explosion sound

    private bool hasExploded = false;

    // Reference to the RaycastTargetButton (could also be set via Inspector)
    private RaycastTargetButton raycastTargetButton;

    // Score variables
    private GameManager gameManager; // Public score that will be accessed by the GameSceneController
    private int scorePerExplosion = 10; // Points per explosion

 //   private AudioSource audioSource; // Reference to the AudioSource component

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Start()
    {
        // Try to get the RaycastTargetButton component
        raycastTargetButton = GetComponentInParent<RaycastTargetButton>();

        if (raycastTargetButton != null)
        {
            // Subscribe to events if raycastTargetButton is found
            raycastTargetButton.OnHoverEnterEvent += HandleHoverEnter;
            raycastTargetButton.OnClickedEvent += HandleClicked;
        }

        // Get the AudioSource component from the current object (your prefab)
       // audioSource = GetComponent<AudioSource>();

        // Check if the AudioSource is not assigned in the Inspector
       /* if (audioSource == null)
        {
            // If AudioSource is not attached, print a warning
            Debug.LogWarning("AudioSource not found on the object. Please add an AudioSource component.");
        }*/
    }

    private void OnDestroy()
    {
        // Unsubscribe from events when this object is destroyed
        if (raycastTargetButton != null)
        {
            raycastTargetButton.OnHoverEnterEvent -= HandleHoverEnter;
            raycastTargetButton.OnClickedEvent -= HandleClicked;
        }
    }

    // Handle hover enter (currently not used)
    private void HandleHoverEnter()
    {
        // Optionally, trigger the explosion when hovering (if you want)
        // Explode();  // Uncomment if you want explosion on hover
    }

    // Handle click event
    private void HandleClicked()
    {
        // Trigger the explosion when clicked
        Explode();
    }

    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // Play explosion sound only if audioSource is assigned and explosionSound is provided
        /*       if (audioSource != null && explosionSound != null)
               {
                   audioSource.PlayOneShot(explosionSound); // Plays the sound once
               }
               else
               {
                   Debug.LogWarning("Explosion sound not played: audioSource or explosionSound is missing.");
               }*/

        // Destroy the original object (bouncy ball)
        soundFXmanager.instance.PlayrandomSoundFXClip(explosionSounds, transform, 1f);
        Destroy(gameObject);

        // Spawn fragments
        for (int i = 0; i < fragmentCount; i++)
        {
            // Create a fragment at the object’s position with random rotation
            GameObject fragment = Instantiate(
                fragmentPrefab,
                transform.position,
                Random.rotation
            );

            // Add Rigidbody to fragment if not already present
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = fragment.AddComponent<Rigidbody>();
            }

            // Apply explosion force to the fragment
            rb.AddExplosionForce(
                explosionForce,
                transform.position,
                explosionRadius
            );

            // Optional: Destroy fragments after some time to clean up
            Destroy(fragment, 5f);
        }

        // Add score for this explosion
        gameManager.score += scorePerExplosion;
        Debug.Log("Score: " + gameManager.score);
    }
}




