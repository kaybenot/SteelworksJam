using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIState
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Transform player;
    protected AIState nextState;
    protected NavMeshAgent agent;

    float visDist = 15.0f;
    float visAngle = 180.0f;

    public AIState(GameObject _npc, NavMeshAgent _agent, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        stage = EVENT.ENTER;
        player = _player;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public AIState Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            return true;
        }
        return false;
    }
}

public class Idle : AIState
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Transform _player)
                : base(_npc, _agent, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer() && player.gameObject.GetComponent<Player>().hidingPlace == null)
        {
            nextState = new Pursue(npc, agent, player);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0, 100) < 10)
        {
            nextState = new Patrol(npc, agent, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        //IDLE
        base.Exit();
    }
}

public class Patrol : AIState
{
    int currentIndex = -1;

    public Patrol(GameObject _npc, NavMeshAgent _agent, Transform _player)
                : base(_npc, _agent, _player)
    {
        name = STATE.PATROL;
        //SPEED
        agent.speed = 2;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        float lastDist = Mathf.Infinity;
        for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++)
        {
            GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if (distance < lastDist)
            {
                currentIndex = i - 1;
                lastDist = distance;
            }
        }
        base.Enter();
    }

    public override void Update()
    {

        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;

            agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);
        }

        if (CanSeePlayer() && player.gameObject.GetComponent<Player>().hidingPlace == null)
        {
            nextState = new Pursue(npc, agent, player);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class Pursue : AIState
{

    public Pursue(GameObject _npc, NavMeshAgent _agent, Transform _player)
                : base(_npc, _agent, _player)
    {
        name = STATE.PURSUE;
        //SPEED
        agent.speed = 3;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        AudioManager.instance.PlayMusic(AudioClipType.BossMusic);
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);
        if (agent.hasPath)
        {
            if (player.gameObject.GetComponent<Player>().hidingPlace != null)
            {
                nextState = new Idle(npc, agent, player);
                stage = EVENT.EXIT;
            }
            else
            if (!CanSeePlayer())
            {
                nextState = new Patrol(npc, agent, player);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        AudioManager.instance.PlayMusic(AudioClipType.BossMusic,false);
        base.Exit();
    }
}
