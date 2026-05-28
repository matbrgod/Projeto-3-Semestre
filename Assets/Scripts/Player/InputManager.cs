using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [Header("Refs")]
    public PlayerInputSystem playerControl;
    AnimatorManager animManager;
    PlayerMovement playerMove;
    PlayerRespawn playerRespawn;
    PlayerInteract playerInteract;
    DialogueManager dialogueManager;
    PauseManager pauseManager;
    AudioManager audioManager;

    [Header("Vetores dos Inputs")]
    public Vector3 moveInput;
    public Vector2 camInput;

    [Header("Valores dos Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public float camXInput;
    public float camYInput;
    public float moveAmout;
    public float respawnCounter = 3f;

    [Header("Flags dos Inputs")]
    public bool jumpInput;
    public bool sprintInput;
    public bool dashInput;
    public bool pauseInput;
    public bool interactInput;
    public bool respawnInput;
    public bool progressionInput;

    public float time;
    public GameObject img;
    public Image imgFill;

    private void Awake()
    {
        animManager = GetComponent<AnimatorManager>();
        playerMove = GetComponent<PlayerMovement>();
        playerInteract = GetComponent<PlayerInteract>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        playerRespawn = GetComponent<PlayerRespawn>();
        pauseManager = FindFirstObjectByType<PauseManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (img != null) imgFill.fillAmount = time;
    }

    private void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new PlayerInputSystem();

            // detecta os inputs de movimento e da camera (respectivamente)
            playerControl.PlayerMove.Movement.performed += i => moveInput = i.ReadValue<Vector2>();
            playerControl.PlayerMove.Camera.performed += i => camInput = i.ReadValue<Vector2>();

            // detecta os inputs que o jogador fizer
            playerControl.PlayerActions.Jump.performed += i => jumpInput = true; // input de pulo

            // input do sprint
            playerControl.PlayerActions.Sprint.performed += i => sprintInput = true; // detecta se o botão foi pressionado
            playerControl.PlayerActions.Sprint.canceled += i => sprintInput = false; // detecta se ele deixou de ser pressionado

            playerControl.PlayerActions.Dash.performed += i => dashInput = true; // input do dash

            playerControl.PlayerActions.Pause.performed += i => pauseInput = true; // input de pause

            playerControl.PlayerActions.Interact.performed += i => interactInput = true; // input de interação

            //respawnInput = playerControl.PlayerActions.Respawn.IsPressed(); // input de respawn
            playerControl.PlayerActions.Respawn.performed += i => respawnInput = true;
            playerControl.PlayerActions.Respawn.canceled += i => respawnInput = false;

            playerControl.PlayerActions.Progression.performed += i => progressionInput = true; // input para abrir a UI do contador de conhecimento
        }

        playerControl.Enable(); // habilita o input system do jogador
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    public void HandleInputs()
    {
        // inputs de movimento
        HandleMovementInput();
        HandleJumpInput();
        HandleSprintInput();
        HandleDashInput();

        //inputs diversos
        HandlePauseInput();
        HandleRespawnInput();
        HandleInteractInput();
        HandleProgressionUiInput();
    }

    private void HandleMovementInput()
    {
        //playerMove.isWalking = playerControl.PlayerMove.Movement.IsPressed();
        verticalInput = moveInput.y;
        horizontalInput = moveInput.x;

        moveAmout = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animManager.UpdateAnimatorValues(0, moveAmout, playerMove.isSprinting);

        camYInput = camInput.y;
        camXInput = camInput.x;
    }

    private void HandleJumpInput()
    {
        if(jumpInput == true)
        {
            playerMove.HandleJump();
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                playerMove.HandleJump();
            jumpInput = false;
        }
    }

    private void HandleSprintInput()
    {
        if (sprintInput && moveAmout > 0.5f)
        {
            playerMove.isSprinting = true;
        }
        else
        {
            playerMove.isSprinting = false;
        }
    }

    private void HandleDashInput()
    {
        if (dashInput)
        {
            playerMove.HandleDash();
            dashInput = false;
        }
    }

    private void HandlePauseInput()
    {
        if (pauseInput)
        {
            if (!pauseManager.isPaused)
            {
                pauseManager.PauseGame();
                pauseInput = false;
            }
            else //if (pauseInput && pauseManager.isPaused)
            {
                pauseManager.ResumeGame();
                pauseInput = false;
            }
        }
    }

    private void HandleRespawnInput()
    {
        if (respawnInput)
        {
            img.gameObject.SetActive(true);
            time += Time.deltaTime;
            if (time >= respawnCounter)
            {
                playerRespawn.RespawnPlayer();
                respawnInput = false;
            }
            //respawnInput = false;
        }
        else if (!respawnInput)
        {
            img.gameObject.SetActive(false);
            time = 0f;
        }
    }

    private void HandleProgressionUiInput()
    {
        if (progressionInput)
        {
            StartCoroutine(playerInteract.OpenProgressionUi());
            progressionInput = false;
        }
    }

    private void HandleInteractInput()
    {
        if (interactInput)
        {
            if (playerInteract.canInteract && !dialogueManager.isDialogueActive)
            {
                playerInteract.HandleStoneInteract();
            }
            else if(playerInteract.canInteract && dialogueManager.isDialogueActive)
            {
                dialogueManager.EndDialogue();
            }
            if (playerInteract.shrineObj != null && playerInteract.miniShrine)
            {
                playerInteract.MiniShrineInteract();
            }
            interactInput = false;
        }
    }
}
