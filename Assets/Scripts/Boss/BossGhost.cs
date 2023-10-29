using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static AIState;

public class BossGhost : MonoBehaviour, IInteractable
{
    [SerializeField] private int bossGhostIndex;
    private Player player;
    private Transform fireplace;
    public bool canGoToTheFirePlace;
    private NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();   
        fireplace = GameObject.FindGameObjectWithTag("Fireplace").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
            Debug.LogAssertion("PlayerController could not find Player script!");
    }
    public void Use(Player player)
    {
        string command = "Boss." + bossGhostIndex;
        CommandProcessor.SendCommand(command);
        this.gameObject.SetActive(false);
    }

    public void GoToTheFireplace()
    {
        canGoToTheFirePlace = true;
    }
    private void Update()
    {
        if (canGoToTheFirePlace)
        {
            Vector3 direction = player.transform.position - transform.position;
            Vector3 directionToFireplace = fireplace.position - transform.position;
            //Debug.Log(directionToFireplace.magnitude);
            if (direction.magnitude <= 8f)
            {
                agent.speed = 2;
                agent.SetDestination(fireplace.position);
            }
            else
                agent.speed = 0;

            if (directionToFireplace.magnitude <= 0.8f)
            {
                DebuffManager.instance.ApplyRandomDebuff();
                canGoToTheFirePlace = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}
