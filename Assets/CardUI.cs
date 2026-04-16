using System.Collections;
using UnityEngine;

public class CardUI : MonoBehaviour
{

    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI manaText;
    public TMPro.TextMeshProUGUI descText;
    public bool selected = false;
    Vector2 higlightedTransform;
    RectTransform rect;
    public CardRuntime cardData;
    Coroutine moveRoutine;
    public bool higlightTransformSet = false;
    Vector2 basePosition;
    bool hasBasePosition = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        higlightedTransform = new(rect.anchoredPosition.x, rect.anchoredPosition.y + 80);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRectTransform() 
    {

    }

    public void PlayedCard() 
    { 
        Destroy(this.gameObject);
    }

    public void DeselectCard() 
    {
        this.transform.GetChild(3).gameObject.SetActive(false);
        Canvas canvas = GetComponent<Canvas>();
        canvas.overrideSorting = false;
    }

    public void SelectCard()
    {
        if (!hasBasePosition) return;

        Canvas canvas = GetComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 100;

        Vector2 highlightPos = basePosition + new Vector2(0, 80);

        MoveTo(highlightPos, 0);

        this.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void Setup(CardRuntime cardDataGotten)
    {
        cardData = cardDataGotten;

        if (cardData == null)
        {
            Debug.LogError("CardData is NULL");
            return;
        }

        if (nameText == null)
        {
            Debug.LogError("nameText is NULL on " + gameObject.name);
            return;
        }

        nameText.text = cardData.cardName;
        manaText.text = cardData.manaCost.ToString();
        descText.text = cardData.description;
    }

    public void MoveTo(Vector2 targetPos, float targetRot)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

       
        moveRoutine = StartCoroutine(MoveToPosition(targetPos, targetRot));
    }

    IEnumerator MoveToPosition(Vector2 targetPos, float targetRot)
    {
        RectTransform rect = GetComponent<RectTransform>();
        if (rect == null) yield break;

        Vector2 startPos = rect.anchoredPosition;
        float startRot = rect.localEulerAngles.z;

        float time = 0;
        float duration = 0.2f;

        while (time < duration)
        {
            if (rect == null) yield break;

            time += Time.deltaTime;
            float t = time / duration;

            rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            float rot = Mathf.LerpAngle(startRot, targetRot, t);
            rect.localRotation = Quaternion.Euler(0, 0, rot);

            yield return null;
        }

        rect.anchoredPosition = targetPos;
    }

    public void SetBasePosition(Vector2 pos)
    {
        basePosition = pos;
        hasBasePosition = true;
    }
}
