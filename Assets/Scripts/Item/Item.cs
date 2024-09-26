using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    //public static Action Merge;
    //[HideInInspector] public ItemsManager ItemsManager;

    //[Header("- - - Индивидуальные параметры обьекта - - -")]
    //public int itemIndexInClass;
    //public bool main = false;
    //public bool contact = false;

    public static event Action<Item> Merged;

    [field: SerializeField] public int Index { get; /*private*/ set; }

    private bool _isInitiatorContact = true;

    private void Start()
    {
        StartCoroutine(ObjectScaling());
    }

    IEnumerator ObjectScaling()
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

        // Определяем кто инициировал слияние
        if (_isInitiatorContact == false)
            return;
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

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    return;

    //    // Столкновение с обьектами игнора
    //    if (collision.gameObject.layer == 3)
    //    {
    //        //Debug.Log("Обьект просчитал столкновение с обьектом - <color=blue>" + collision.gameObject.name + "</color>, класса Borders");
    //        return;
    //    }

    //    // Столкновение с другим предметом
    //    try
    //    {
    //        Item ItemCollision = collision.gameObject.GetComponent<Item>();
    //        if (ItemCollision.itemIndexInClass == itemIndexInClass)
    //        {
    //            // Проверка, кто иниицировал контакт
    //            Vector2 selfVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
    //            Vector2 otherVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
    //            float velocityValueSelf = Mathf.Sqrt(Mathf.Pow(selfVelocity.x, 2) + Mathf.Pow(selfVelocity.y, 2));
    //            float velocityValueOther = Mathf.Sqrt(Mathf.Pow(otherVelocity.x, 2) + Mathf.Pow(otherVelocity.y, 2));

    //            if (velocityValueSelf > velocityValueOther)
    //                main = true;
    //            else
    //                main = false;

    //            // Отправляем данные об контакте менеджеру
    //            if (main && !contact)
    //            {
    //                Debug.Log("Слияние обьекта: <color=teal>" + gameObject.name + "</color> c обьектом <color=teal>" + collision.gameObject.name + "</color>");
    //                ItemsManager.contactsPoints.Add(collision.GetContact(0).point);
    //                ItemsManager.mergedIndexes.Add(itemIndexInClass + 1);
    //                ItemsManager.destroyObjects.Add(gameObject);
    //                ItemsManager.destroyObjects.Add(collision.gameObject);
    //                Merge?.Invoke();
    //            }
    //            contact = true;
    //        }
    //    }
    //    catch
    //    {
    //        Debug.LogWarning("Обьект просчитал столкновение с обьектом - <color=orange>" + collision.gameObject.name + "</color>, с которомы не должен был");
    //    }
    //}
}
