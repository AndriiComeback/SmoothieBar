using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private string customerNameInPool;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform barPoint;
    [SerializeField] private RuntimeAnimatorController idle;

    public static CustomerController Instance;
    private void Awake() {
        Instance = this;
    }
    public void CallNewCustomer(Color targetColor) {
        StartCoroutine(CallNewCustomerInner(targetColor));
    }
    IEnumerator CallNewCustomerInner(Color targetColor) {
        float time = 2;
        GameObject customer = ObjectPooler.Instance.SpawnFromPool(customerNameInPool, spawnPoint.position, spawnPoint.rotation);
        customer.GetComponentInChildren<Image>().color = targetColor;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(customer.transform.DOMove(barPoint.position, time));
        mySequence.Join(customer.transform.DORotate(new Vector3(0, 160, 0), time).SetEase(Ease.Linear));
        yield return new WaitForSeconds(time * .75f);

        customer.GetComponent<Animator>().runtimeAnimatorController = idle;
    }
    public void SendCustomerAway() { 
    }
}
