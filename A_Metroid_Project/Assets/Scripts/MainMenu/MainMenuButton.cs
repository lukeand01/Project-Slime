using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : ButtonBase
{
    [SerializeField] GameObject selected;
    [SerializeField] GameObject targetUI;
    Color selectedColor = Color.black;
    Color originalColor;
    Image sr;
    private void Start()
    {
        sr = GetComponent<Image>();
        originalColor = sr.color;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        //targetUI.SetActive(true);
        StartCoroutine(RecoverColor());
    }

    IEnumerator RecoverColor()
    {
        sr.color = selectedColor;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        selected.SetActive(true);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        selected.SetActive(false);
    }
}
