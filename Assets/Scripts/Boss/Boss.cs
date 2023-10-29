using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour, IDamagable
{
    [SerializeField] private int startingHealth;
    [SerializeField] private BossShotManager shotManager;
    [SerializeField] private BossInteraction bossInteraction;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite attackSprite;
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private ParticleSystem damageParticles;

    [Header("Boss speciala actions")]
    [SerializeField] private List<BaseBossAction> specialActions;

    private Sprite baseSprite;
    private int currentHealth;
    private Coroutine spriteCoroutine;
    private Transform spawnPoint;

    public Transform SpawnPoint => spawnPoint;

    private void OnDestroy()
    {
        if (spriteCoroutine != null)
        {
            spriteRenderer.sprite = baseSprite;
            StopCoroutine(spriteCoroutine);
            spriteCoroutine = null;
        }
    }

    public void Init(Transform spawnPoint)
    {
        this.spawnPoint = spawnPoint;
        bossInteraction.gameObject.SetActive(false);
        baseSprite = spriteRenderer.sprite;
        currentHealth = startingHealth;
        shotManager.Init(Attack);
        foreach (var action in specialActions)
        {
            action.Init(this);
        }
    }

    public void Attack()
    {
        PlaySpecialAction(SpecialActionType.OnAttack);
        if (spriteCoroutine != null)
        {
            spriteRenderer.sprite = baseSprite;
            StopCoroutine(spriteCoroutine);
            spriteCoroutine = null;
        }

        spriteCoroutine = StartCoroutine(ChangeAttackSprite(0.25f));
    }

    private IEnumerator ChangeAttackSprite(float spriteVisibleTime)
    {
        spriteRenderer.sprite = attackSprite;
        yield return new WaitForSeconds(spriteVisibleTime);
        spriteRenderer.sprite = baseSprite;
        spriteCoroutine = null;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        damageParticles.Play();
        CommandProcessor.SendCommand($"Canvas.SetEnemyHealth {(float)((float)currentHealth / (float)startingHealth)}");
        if (currentHealth <= 0)
        {
            CommandProcessor.SendCommand("Canvas.SetEnemyHealth 0");
            OnDeath();
        }
    }

    private void PlaySpecialAction(SpecialActionType actionType)
    {
        if(specialActions != null & specialActions.Count > 0)
        {
            var onCurrentType = specialActions.Where(x => x.SpecialActionType.Equals(actionType)); 
            if (onCurrentType != null)
            {
                foreach (var action in onCurrentType)
                {
                    action.PlayAction(this);
                }
            }
        }
    }

    private void OnDeath()
    {
        Debug.Log("Boss killed");
        shotManager.DisableAttacking();
        spriteRenderer.sprite = deadSprite;
        bossInteraction.gameObject.SetActive(true);
        CommandProcessor.SendCommand("Boss.End");
    }
}
