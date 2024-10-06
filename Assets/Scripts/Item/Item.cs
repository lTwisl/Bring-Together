using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static event Action<Item> Merged;

    [field: SerializeField] public int Index { get; private set; }

    private bool _isInitiatorContact = true;

    private void Start()
    {
        StartCoroutine(ObjectScaling());
    }

    private IEnumerator ObjectScaling()
    {
        float counter = 0;
        float duration = 0.5f;
        Vector3 targetScale = transform.localScale;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, counter / duration);
            yield return null;
        }
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверка на наличие необходимого компонента
        if (collision.gameObject.TryGetComponent(out Item other) == false)
            return;

        if (other.Index != Index)
            return;

        // Проверяем чтобы оба объекта не находились в контакте
        if (_isInitiatorContact == false || other._isInitiatorContact == false)
            return;

        _isInitiatorContact = false;
        other._isInitiatorContact = false;

        Vector3 contactPoint = collision.GetContact(0).point;
        Merge(other, contactPoint);
    }

    private void Merge(Item other, Vector3 contactPoint)
    {
        GameObject nextItemPrefab = GameLevelManager.Instance.ItemsContainer[Index + 1].prefab;

        GameObject nextItem = Instantiate(nextItemPrefab);
        nextItem.transform.position = contactPoint;

        Merged?.Invoke(nextItem.GetComponent<Item>());

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
