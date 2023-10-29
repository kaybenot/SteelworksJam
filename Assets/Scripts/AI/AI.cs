using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] private Transform player;
    AIState currentState;
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        currentState = new Idle(this.gameObject, agent, player);
    }
    void Update()
    {
        currentState = currentState.Process();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.TryGetComponent(out IDamagable damagable) && player.gameObject.GetComponent<Player>().hidingPlace == null)
        {
            damagable.Damage(999);
        }
    }
}
