using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable, ICommandListener
{
    [SerializeField]
    private CinemachineImpulseSource cinemachineImpulseSource;
    public GameObject Head;
    public Transform HeadMountPoint;
    public float JumpHeight = 0.5f;
    public float MovementSpeed = 1f;
    public float LookSensitivity = 0.1f;
    public int StartingHealth = 50;
    public PlayerShootingManager ShootingManager;
    public float maxInteractDistance = 1.0f;

    public string ListenerName { get; set; } = "Player";

    public bool IsGrounded => Vector3.Dot(collisionNormal, Vector3.up) > 0.7f;

    public HidingPlace hidingPlace;

    private Rigidbody rb;
    private Vector3 collisionNormal = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private int currentHealth;
    private bool canShoot = false;
    private bool canMove = true;
    private AudioSource footstepsAudio;
    public float footstepsCooldown = 1.0f;
    private float currentFootstepsCooldown = 0.0f;

    private void Awake()
    {
        CommandProcessor.RegisterListener(this);
        currentHealth = StartingHealth;
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogAssertion("Could not locate Rigidbody on Player!");

        if (Head == null)
            Debug.LogAssertion("There is no assigned Head to Player script!");

        footstepsAudio = GetComponent<AudioSource>();
        if(footstepsAudio == null)
            Debug.LogAssertion("Could not locate AudioSource on Player!");

    }

    public void ProcessCommand(string command, List<string> parameters)
    {
        switch (command)
        {
            case "EnableGun":
                canShoot = true;
                break;
            case "DisableGun":
                canShoot = false;
                break;
            case "PushPlayer":
                if (parameters.Count > 0)
                    PushPlayer(int.Parse(parameters[0]));
                else
                    PushPlayer(5f);
                break;
            default:
                Debug.LogWarning($"Unimplemented command: {command}");
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void FixedUpdate()
    {
        EvaluateMovement();
    }

    private void Update()
    {
        currentFootstepsCooldown += Time.deltaTime;
        if (rb.velocity.magnitude > 0.5 && currentFootstepsCooldown > footstepsCooldown)
        {
            footstepsAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            footstepsAudio.panStereo = -footstepsAudio.panStereo;
            footstepsAudio.Play();
            currentFootstepsCooldown = 0.0f;
        }

        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Interactable");
        if (Physics.Raycast(Head.transform.position, Head.transform.forward, out hit, maxInteractDistance, mask))
        {
            // Show Hint
            CommandProcessor.SendCommand("Canvas.ShowHint");
        }
        else
        {
            CommandProcessor.SendCommand("Canvas.HideHint");
        }
    }

    public void Move(Vector2 direction)
    {
        velocity = new Vector3(direction.x * MovementSpeed, 0f, direction.y * MovementSpeed);
    }

    public void PushPlayer(float speed)
    {
        var pushVelocity = -Camera.main.transform.forward * speed;
        pushVelocity.y = 0.75f * (speed / 2f);
        rb.velocity += pushVelocity;
        StartCoroutine(BlockMovementForTime(0.8f));
    }

    public void Turn(Vector2 delta)
    {
        delta *= LookSensitivity;
        Head.transform.eulerAngles += new Vector3(0f, delta.x, 0f);
        
        var headEulerAngles = Head.transform.eulerAngles;
        if (headEulerAngles.x > 180f)
            headEulerAngles.x -= 360f;
        
        if (headEulerAngles.x - delta.y < 90f && headEulerAngles.x - delta.y > -90f)
            Head.transform.eulerAngles += new Vector3(-delta.y, 0f, 0f);
        else if (headEulerAngles.x - delta.y > 0)
            Head.transform.eulerAngles = new Vector3(90f, Head.transform.eulerAngles.y, Head.transform.eulerAngles.z);
        else
            Head.transform.eulerAngles = new Vector3(-90f, Head.transform.eulerAngles.y, Head.transform.eulerAngles.z);
    }

    public void Interact()
    {
        Debug.Log("Tried interacting");

        if(hidingPlace != null)
        {
            hidingPlace.Unhide();
            hidingPlace = null;
            return;
        }

        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Interactable");
        if (Physics.Raycast(Head.transform.position, Head.transform.forward, out hit, maxInteractDistance, mask))
        {
            hit.collider.gameObject.GetComponent<IInteractable>().Use(this);

            hidingPlace = hit.collider.gameObject.GetComponent<HidingPlace>();
        }
    }

    public void Shoot()
    {
        //if (!canShoot)
        //    return;

        Debug.Log("Used gun");
        ShootingManager.ShootGuns();
    }

    public void Jump()
    {
        if (!IsGrounded || !canMove)
            return;
        
        var jumpForce = Mathf.Sqrt(-2f * Physics.gravity.y * JumpHeight);
        var jumpDirection = Vector3.up;

        rb.velocity += jumpDirection * jumpForce;
    }

    private void EvaluateCollision(Collision collision)
    {
        collisionNormal = Vector3.zero;
        
        for (var i = 0; i < collision.contactCount; i++)
        {
            var contact = collision.contacts[i];
            collisionNormal += contact.normal;
            collisionNormal.Normalize();
        }
    }

    private void EvaluateMovement()
    {
        if (!canMove)
            return;

        var forward = Head.transform.forward;
        forward.y = 0f;
        forward.Normalize();
        
        var right = Head.transform.right;
        right.y = 0f;
        right.Normalize();

        rb.velocity = forward * velocity.z + right * velocity.x + rb.velocity.y * Vector3.up;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Debug.Log("You died!");
        CommandProcessor.SendCommand("Canvas.GameOver");
    }

    public IEnumerator BlockMovementForTime(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
