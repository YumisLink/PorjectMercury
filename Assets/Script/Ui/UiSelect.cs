using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiSelect : MonoBehaviour
{
    public GameObject Style;//选择框的样式
    public GameObject Grid;
    public EventSystem ES;

    private List<SelectElement> _elements;
    private Action<int> _selectCallback;

    private void Awake()
    {
        _elements = new List<SelectElement>();
    }

    //callback传入的int表示，选中了第几个text
    public void ShowOptions(Action<int> callback, params string[] text)
    {
        if (text.Length <= 0) throw new ArgumentException("至少输入一个text");
        _selectCallback = callback ?? throw new ArgumentNullException("callback不能为null");
        gameObject.SetActive(true);
        GameManager.StopGame();
        for (int i = 0; i < text.Length; i++)
        {
            string t = text[i];
            int temp = i;
            var e = Instantiate(Style, Grid.transform).GetComponent<SelectElement>();
            e.Txt.text = t;
            e.Btn.onClick.AddListener(() =>
            {
                GameManager.ContinueGame();
                _selectCallback(temp);
                foreach (var select in _elements)
                {
                    Destroy(select);
                }
                _elements.Clear();
                gameObject.SetActive(false);
            });
            _elements.Add(e);
        }
        ES.SetSelectedGameObject(_elements[0].gameObject);
    }
}
