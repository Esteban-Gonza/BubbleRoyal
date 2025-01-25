using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [SerializeField] int min, seg;
    [SerializeField] Text tiempo;

    private float restante;
    private bool enMarcha;
  
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (enMarcha)
        {
            restante -= Time.deltaTime;
            if(restante < 1)
            {
                enMarcha = true;
            }
            int tempMin = Mathf.FloorToInt(restante/60);
            int tempSeg = Mathf.FloorToInt(restante%60);
            tiempo.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
            
        }

    }

    private void Awake()
    {
        restante = (min * 60) + seg;
        enMarcha = true;
    }
}
