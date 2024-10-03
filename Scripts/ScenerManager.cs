using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScenerManager : MonoBehaviour
{
    [SerializeField] private Text       m_Text         = null;

    [Header("Login")]
    [SerializeField] private InputField m_loginPasswordInput = null;
    [SerializeField] private InputField m_loginEmailInput    = null;

    [Header("Register")]
    [SerializeField] private InputField m_nameInput         = null;
    [SerializeField] private InputField m_lastnameInput     = null;
    [SerializeField] private InputField m_ageInput          = null;
    [SerializeField] private InputField m_usernameInput     = null;
    [SerializeField] private InputField m_emailInput        = null;
    [SerializeField] private InputField m_passwordInput     = null;
    [SerializeField] private InputField m_repasswordInput   = null;

    private NetworkManager m_networkManager = null;

    private void Awake()
    {
        m_networkManager = GameObject.FindObjectOfType<NetworkManager>();
    }
    
    public void SubmitLogin()
    {
        if (m_loginPasswordInput.text == "" || m_loginEmailInput.text == "")
        {
            m_Text.text = "CAMPOS INCOMPLETOS, ERROR 201";
            return;
        }
        m_Text.text = "Procesando...";

        m_networkManager.CheckUser(m_loginPasswordInput.text, m_loginEmailInput.text, delegate (Response response)
        {
            m_Text.text = response.message;
        });
    }

    public void SubmitRegister()
    {

        if (m_nameInput.text == "" || m_lastnameInput.text == "" || m_ageInput.text == "" || m_usernameInput.text == "" || m_emailInput.text == "" || m_passwordInput.text == "" || m_repasswordInput.text == "")
        {
            m_Text.text = "CAMPOS INCOMPLETOS, ERROR 201";
            return;
        }

        if (m_passwordInput.text == m_repasswordInput.text)
        {
            m_Text.text = "Procesando...";
            
            m_networkManager.CreateUser(m_nameInput.text, m_lastnameInput.text, m_ageInput.text, m_usernameInput.text, m_emailInput.text, m_passwordInput.text, delegate(Response response)
            {
                m_Text.text = response.message;
            });
        }
        else
        {
            m_Text.text = "CLAVES NO COINCIDEN, ERROR 202";
        }
    }
}
