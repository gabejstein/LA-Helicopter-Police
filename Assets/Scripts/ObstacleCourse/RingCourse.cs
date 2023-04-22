using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RingCourse : MonoBehaviour
{
    Ring[] rings;
    [SerializeField] Text timerUI;

    [SerializeField] bool TimeAttackMode = false;
    [SerializeField] float TimeLimit = 60f; //in seconds?
    float remainingTime;
    public Action OnCompleted;

    // Start is called before the first frame update
    void Start()
    {
        rings = GetComponentsInChildren<Ring>();

        for (int i = 0; i < rings.Length; i++)
        {
            rings[i].orderNumber = i;
            rings[i].ringCourse = this;
        }

    }

    public void OnEnterRing(Ring ring)
    {
        if(ring.orderNumber==0)
        {
            ring.gameObject.SetActive(false);

            if(TimeAttackMode)
            {
                StartCoroutine(StartTimeAttack());
            }
            return;
        }

        int previousRing = ring.orderNumber - 1;

        if(!rings[previousRing].gameObject.activeInHierarchy)
        {
            ring.gameObject.SetActive(false);

            if (ring.orderNumber == rings.Length - 1)
                OnCompletedCourse();

            if (TimeAttackMode)
            {
                remainingTime += ring.TimeBonus;
                UpdateTimeUI();
            }
                
        }
    }

    void OnCompletedCourse()
    {
        Debug.Log("You finished the ring course!!!");

        if(TimeAttackMode)
            StopAllCoroutines();

        //TO DO: Call event code for something to happen after finishing.
        OnCompleted.Invoke();
    }

    IEnumerator StartTimeAttack()
    {
        Debug.Log("Beginning time attack!");
        remainingTime = TimeLimit;

        timerUI.gameObject.SetActive(true);

        while(remainingTime>0)
        {
            UpdateTimeUI();
            remainingTime -= 1f;
            yield return new WaitForSeconds(1);
        }

        Debug.Log("You failed the time attack!");
        timerUI.gameObject.SetActive(false);
        ResetAllRings();
        yield return null;
    }

    void UpdateTimeUI()
    {
        timerUI.text = remainingTime.ToString();
    }

    void ResetAllRings()
    {
        for (int i = 0; i < rings.Length; i++)
        {
            rings[i].gameObject.SetActive(true);
        }
    }
}
