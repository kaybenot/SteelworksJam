using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : BaseBossAction
{
    [SerializeField] private ParticleSystem dissapearParticles;
    [SerializeField] private Transform pointsParent;
    [SerializeField] private Transform particleParent;
    [SerializeField] private List<Transform> randomTeleportPoints;

    private Transform currentTarget;
    private SpecialActionType specialActionType => SpecialActionType.OnAttack;
    public override SpecialActionType SpecialActionType => specialActionType;

    public void Start()
    {
        pointsParent.transform.parent = null;
        particleParent.transform.parent = null;
    }

    public override void PlayAction(Boss boss)
    {
        var pointsWithoutCurrent = new List<Transform>(randomTeleportPoints);
        if (currentTarget != null)
        {
            pointsWithoutCurrent.Remove(currentTarget);
        }
        if (dissapearParticles != null)
        {
            particleParent.transform.position = boss.transform.position;
            particleParent.transform.rotation = boss.transform.rotation;
            dissapearParticles.Play();
        }

        int randomIndex = Random.Range(0, pointsWithoutCurrent.Count);
        currentTarget = pointsWithoutCurrent[randomIndex];
        boss.transform.position = currentTarget.position;
    }
}
