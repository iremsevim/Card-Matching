using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectCategoryItem : MonoBehaviour
{
    public Text selectcategoryname;
    public System.Action OnClicked;



    public void SetUp(string name,System.Action _OnClicked)
    {
        selectcategoryname.text = name;
        OnClicked = _OnClicked;
    }
    public void Clicked()
    {

        OnClicked?.Invoke();
      
       
    }
}
