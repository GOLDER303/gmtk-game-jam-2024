using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject fillSprite;

    private void Awake()
    {
        fillSprite = transform.Find("Fill").Find("FillSprite").gameObject;
    }

    public void SetSize(float size)
    {
        if (size < 0)
        {
            size = 0;
        }
        else if (size > 1)
        {
            size = 1;
        }

        fillSprite.transform.localScale = new Vector3(size, fillSprite.transform.localScale.y);
    }
}
