using UnityEngine;
using System.Collections;

public class AttackVisual : MonoBehaviour
{
    public float timeToAttack = 1f; // Время, за которое цвет станет красным
    public string thisType;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private BoxCollider2D boxCollider;
    private EnemyPoolManager EPM;
    private void Awake()
    {
        EPM = EnemyPoolManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void InitializeTime(float time)
    {
        timeToAttack = time;
        StartCoroutine(AttackSequence());
    }
    public void StartAttack()
    {
        StartCoroutine(AttackSequence());
    }

    private IEnumerator AttackSequence()
    {
        float elapsedTime = 0f;
        float step = 0.1f; // шаг 0.1 сек
        Color startColor = Color.white;
        Color endColor = Color.red;

        while (elapsedTime < timeToAttack)
        {
            float t = elapsedTime / timeToAttack;
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
            yield return new WaitForSeconds(step);
            elapsedTime += step;
        }
    
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(0.1f);

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerStats.Instance.TakeDamage();
            }
        }

        EPM.ReturnAttack(thisType, gameObject);
    }

}
