using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class how_to_play : MonoBehaviour
{
    public GameObject p1, p2, p3;
    private int page;

    void OnEnable()
    {
        page = 1;
        p1.SetActive(true);
    }
    public void next()
    {
        switch (page)
        {
            case 1:
                p1.SetActive(false);
                p2.SetActive(true);
                page = 2;
                break;
            case 2:
                p2.SetActive(false);
                p3.SetActive(true);
                page = 3;
                break;
            case 3:
                p3.SetActive(false);
                gameObject.SetActive(false);
                page = 0;
                break;
        }
    }
}
