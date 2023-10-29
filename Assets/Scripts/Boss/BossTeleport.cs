using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : BaseBossAction
{
    [SerializeField] private ParticleSystem dissapearParticles;
    [SerializeField] private List<Transform> randomTeleportPoints;

    private Transform currentTarget;
    private SpecialActionType specialActionType => SpecialActionType.OnAttack;
    public override SpecialActionType SpecialActionType => specialActionType;

    public override void PlayAction(Boss boss)
    {
        var pointsWithoutCurrent = new List<Transform>(randomTeleportPoints);
        if (currentTarget != null)
        {
            if (dissapearParticles != null)
            {
                dissapearParticles.transform.position = currentTarget.position;
                dissapearParticles.Play();
            }
            pointsWithoutCurrent.Remove(currentTarget);
        }

        int randomIndex = Random.Range(0, randomTeleportPoints.Count);
        currentTarget = randomTeleportPoints[randomIndex];
        boss.transform.position = currentTarget.position;
    }
}
