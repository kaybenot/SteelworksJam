using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAttack : BaseBossAction
{
    [SerializeField] private List<Transform> stones;
    [SerializeField] private Transform movableStones;
    [SerializeField] private int stoneShowAmount;
    [SerializeField] private float showTime;
    [SerializeField] private float showSpeed;   
    [SerializeField] private float hideSpeed;
    [SerializeField] private float yHeight = 0f;

    private Coroutine currentShowCoroutine;

    private float startYSize;

    private SpecialActionType specialActionType => SpecialActionType.OnAttack;
    public override SpecialActionType SpecialActionType => specialActionType;

    public override void Init(Boss boss)
    {
        startYSize = movableStones.localPosition.y;
    }

    public override void PlayAction(Boss boss)
    {
        if(currentShowCoroutine != null)
        {
            StopCoroutine(currentShowCoroutine);
            movableStones.localPosition = new Vector3(movableStones.localPosition.x, startYSize, movableStones.localPosition.z);
            currentShowCoroutine = null;
        }

        foreach (var stone in stones)
        {
            stone.gameObject.SetActive(false);
        }

        var stonesList = new List<Transform>(stones);
        for (int i = 0; i < stoneShowAmount; i++)
        {
            int rand = Random.Range(0, stonesList.Count);
            stonesList[rand].gameObject.SetActive(true);
            stonesList.RemoveAt(rand);
        }

        currentShowCoroutine = StartCoroutine(ShowStones());
    }

    private IEnumerator ShowStones()
    {
        float t = 0;
        while (t < 1)
        {
            movableStones.localPosition = new Vector3(movableStones.localPosition.x, Mathf.Lerp(startYSize, yHeight, t), movableStones.localPosition.z);
            t += Time.deltaTime * showSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(showTime);

        t = 0;
        while (t < 1)
        {
            movableStones.localPosition = new Vector3(movableStones.localPosition.x, Mathf.Lerp(yHeight, startYSize, t), movableStones.localPosition.z);
            t += Time.deltaTime * hideSpeed;
            yield return null;

        }
    }
}
