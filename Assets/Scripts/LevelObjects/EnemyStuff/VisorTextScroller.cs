using UnityEngine;

/*
---------------------------------------------------------------------------------------------------------------------------------------
    OLD SCRIPT THAT IS NOT IN USE AND PROBABLY WILL NEVER BE USED----------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------
*/

public class VisorTextScroller : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private float textScrollSpeed;
    private RectTransform textObjectRectTransform;

    void Start()
    {
        textObjectRectTransform = textObject.GetComponent<RectTransform>();
    }
    
    void Update()
    {
        textObjectRectTransform.localPosition -= Vector3.forward * textScrollSpeed * Time.deltaTime;

        if (textObjectRectTransform.localPosition.z < -(textObjectRectTransform.rect.width * .1f + .5f))
        {
            textObjectRectTransform.localPosition = new Vector3(textObjectRectTransform.localPosition.x, textObjectRectTransform.localPosition.y, 0.5f);
        }
    }
}
