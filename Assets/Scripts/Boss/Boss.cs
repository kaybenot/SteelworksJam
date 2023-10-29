using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour, IDamagable
{
    [SerializeField] protected int startingHealth;
    [SerializeField] protected BossShotManager shotManager;
    [SerializeField] protected BossInteraction bossInteraction;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite attackSprite;
    [SerializeField] protected Sprite deadSprite;
    [SerializeField] protected ParticleSystem damageParticles;

    [Header("Boss speciala actions")]
    [SerializeField] protected List<BaseBossAction> specialActions;

    protected Sprite baseSprite;
    protected int currentHealth;
    protected Coroutine spriteCoroutine;
    protected Transform spawnPoint;
    protected BossSpawnManager spawnManager;
    protected int ghostBossIndex;

    public Transform SpawnPoint => spawnPoint;
    public int GhostBossIndex => ghostBossIndex;

    private void OnDestroy()
    {
        if (spriteCoroutine != null)
        {
            spriteRenderer.sprite = baseSprite;
            StopCoroutine(spriteCoroutine);
            spriteCoroutine = null;
        }
    }

    public virtual void Init(Transform spawnPoint, BossSpawnManager spawnManager, int ghostBossIndex)
    {
        this.spawnManager = spawnManager;
        this.spawnPoint = spawnPoint;
        this.ghostBossIndex = ghostBossIndex;
        bossInteraction.Init(spawnManager, this);
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
