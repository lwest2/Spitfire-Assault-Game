using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerGUIhelper : MonoBehaviour {
    // references: https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health?playlist=17144

    private float m_playerHealth = 100;
    private float m_playerBombs = 3;
    public static playerGUIhelper playergui;
    
    [SerializeField]
    private Slider m_healthSlider;

    [SerializeField]
    private Image m_damageImage;

    private float m_flashSpeed = 5.0f;
    private Color m_flashColour = new Color(1f, 0f, 0f, 0.1f);
    private bool m_isDamaged;
    private bool m_isDead;

    [SerializeField]
    private Text m_bombText;

    void OnEnable()
    {
        playergui = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(m_isDamaged)
        {
            m_damageImage.color = m_flashColour;
        }
        else
        {
            m_damageImage.color = Color.Lerp(m_damageImage.color, Color.clear, m_flashSpeed * Time.deltaTime);
        }
        m_isDamaged = false;
	}

    public void setPlayerHealth(float value)
    {
        m_isDamaged = true;
        m_playerHealth += value;
        m_healthSlider.value = m_playerHealth;

        if(m_playerHealth <= 0 && !m_isDead)
        {
            Death();
        }
        Debug.Log(m_playerHealth);
    }

    void Death()
    {
        m_isDead = true;
        Debug.Log("KILL PLAYER HERE");
    }

    public void setPlayerBombs(float value)
    {
        if (m_playerBombs > 0)
        {
            m_playerBombs += value;
            m_bombText.text = "Bombs: " + m_playerBombs.ToString();
        }
    }

    public float getPlayerBombs()
    {
        return m_playerBombs;
    }
}
