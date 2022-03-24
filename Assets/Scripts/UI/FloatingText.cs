using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FloatingText : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private float _speed;

    private TMP_Text _addedMight;

    private void Awake()
    {
        _addedMight = GetComponent<TMP_Text>();
    }

    public void Init(int addedMight)
    {
        _addedMight.text = $"{addedMight}";
        StartCoroutine(FloatAway());
        StartCoroutine(Fade(_addedMight));
    }

    private IEnumerator FloatAway()
    {
        while(true)
        { 
            transform.position += transform.up * _speed * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator Fade(TMP_Text text)
    {
        yield return new WaitForSeconds(0.2f);

        float changeSpeed = 1 / _time;

        while (text.color.a > 0)
        {
            Color color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);
            color.a = Mathf.MoveTowards(color.a, 0, changeSpeed * Time.deltaTime);
            text.color = color;

            yield return null;
        }

        Destroy(gameObject);
    }
}
