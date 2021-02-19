using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MatchingCardBase : MonoBehaviour
{

    public GameObject card_back;
    public GameObject card_front;
    public System.Action OnCicked;
   
    public void OnMouseUpAsButton()
    {
        OnClicked();
    }
    public virtual void OnClicked()
    {
        if (card_front.activeInHierarchy) return;
        OnCicked?.Invoke();
       
        transform.DOScale(transform.localScale / 2, 0.15f).OnComplete(() => { transform.DOScale(transform.localScale * 2, 0.15f); });

       
        card_front.SetActive(true);
    }
    
   
}
