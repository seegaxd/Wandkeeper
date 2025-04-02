using UnityEngine;
using System.Collections;

public class AttackVisual : MonoBehaviour
{
    public float timeToAttack = 1f; // Время, за которое цвет станет красным
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private BoxCollider2D boxCollider;
    private void Awake()
    {
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
        // 1. Постепенно меняем цвет на красный
        float elapsedTime = 0f;
        while (elapsedTime < timeToAttack)
        {
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, elapsedTime / timeToAttack);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = Color.red; // Гарантируем, что цвет точно станет красным

        // 2. Резко на 0.5 секунды синий
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(0.1f);

        // 3. Проверяем, есть ли игрок внутри коллайдера
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerStats.Instance.TakeDamage();
            }
        }

        // Можно отключить объект после атаки
        gameObject.SetActive(false);
    }
}
