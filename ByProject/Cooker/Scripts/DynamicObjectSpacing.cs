using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicObjectSpacing : MonoBehaviour
{
    [Header("Spacing between objects")]
    public float spacing = 2f;

    [Header("Auto update every frame?")]
    public bool autoUpdate = true;

    int count = 0;

    void Update()
    {
        if (autoUpdate)
        {
            ArrangeChildren();
        }
    }

    public void ArrangeChildren()
    {
        List<Transform> children = new List<Transform>();
        count = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                children.Add(transform.GetChild(i));
                count++;
            }

        }
        if (count == 0) return;

        children = children.OrderBy(child => child.localPosition.x).ToList();
        float centerIndex = (count - 1) / 2f;
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            float indexOffset = i - centerIndex;
            float targetX = indexOffset * spacing;
            Vector3 newPos = new Vector3(targetX, transform.root.position.y, 0f);
            children[i].GetComponent<SlotLerp>().lerpTarget = newPos;
            children[i].GetComponent<SlotLerp>().lerping = true;
        }
    }

}
