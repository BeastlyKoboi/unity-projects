using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockAnim : MonoBehaviour
{
    public TextMeshPro textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    IEnumerator Blink()
    {
        Color color = textMeshPro.color;

        while (true)
        {
            for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
            {
                color.a = alpha;
                textMeshPro.color = color;
                yield return new WaitForSeconds(.1f);
            }

            for (float alpha = 0; alpha <= 1f; alpha += 0.1f)
            {
                color.a = alpha;
                textMeshPro.color = color;
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
