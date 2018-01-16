using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AicraftBoundary : MonoBehaviour {

    private bool m_outsideBoundary = false; // if aircraft is outside boundary variable
    private CanvasGroup m_canvasGroup;      // canvas
    private Text m_text;                    // text from canvas

    private int m_countdown;                // countdown timer
    private bool m_triggered = false;       // dont play coroutine twice at the same time

    private CanvasGroup m_canvasGroupRed;

    private void Start()
    {
        m_canvasGroup = GameObject.Find("OutOfBounds").GetComponent<CanvasGroup>();
        m_text = GameObject.Find("Countdown").GetComponent<Text>();
        m_canvasGroupRed = GameObject.Find("OutOfBoundsRed").GetComponent<CanvasGroup>();
        // countdown reset to 10
        m_countdown = 10;
        // set red texture to invisible
        m_canvasGroupRed.alpha = 0;
    }

    private void OnTriggerExit(Collider other)
    {
        // if exits aircraft boundary
        if(other.tag == "aircraftBoundary")
        {
            // outside boundary is true
            m_outsideBoundary = true;
            // if coroutine not triggered yet
            if(m_triggered == false)
            StartCoroutine(OutOfBounds());  // Start outofbounds coroutine
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if entering
        if(other.tag == "aircraftBoundary")
        {
            // no longer outside bounds
            m_outsideBoundary = false;
            // start inbounds coroutine
            StartCoroutine(InBounds());
            // reset triggered
            m_triggered = false;
            // reset red canvas
            while (m_canvasGroupRed.alpha > 0)
                m_canvasGroupRed.alpha -= Time.deltaTime / 2;
        }
    }

    IEnumerator OutOfBounds()
    {
        m_triggered = true;
        m_countdown = 10;
        
        // while staying outside of bounds
        while(m_outsideBoundary == true)
        {
            // set text to countdown
            m_text.text = "" + m_countdown;

            // increase canvas red alpha every loop
            if (m_canvasGroupRed.alpha < 0.4f)
            {
                m_canvasGroupRed.alpha += Time.deltaTime / 2f;
            }

            // fade in canvas
            while(m_canvasGroup.alpha < 1)
            {
                m_canvasGroup.alpha += Time.deltaTime * 2;
                yield return null;
            }

            // wait 1 second
            yield return new WaitForSecondsRealtime(1);

            // if countdown not equal to 0
            if (m_countdown != 0)
            {
                // take away 1
                m_countdown -= 1;
            }
            else if (m_countdown <= 0)
            {
                // else gameover
                SceneManager.LoadScene(3);
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator InBounds()
    {
        // fade canvas back to invisible
        while (m_canvasGroup.alpha > 0)
        {
            m_canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        yield return null;
    }
}
