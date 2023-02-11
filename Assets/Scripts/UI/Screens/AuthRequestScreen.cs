using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AuthRequestScreen : Screen
{
    [SerializeField] private Button  _authButton;

    public event Action Authorized;
    public event Action PersonalDataPermissed;

    private void OnEnable()
    {
        _authButton.onClick.AddListener(OnAuthButtonClicked);
        Authorized += OnAuthorized;
    }

    private void OnDisable()
    {
        _authButton.onClick.RemoveListener(OnAuthButtonClicked);
        Authorized -= OnAuthorized;
    }

    private void OnAuthButtonClicked()
    {
        PlayerAccount.Authorize(Authorized);
    }

    private void OnAuthorized()
    {
        PlayerAccount.RequestPersonalProfileDataPermission(PersonalDataPermissed);
    }
}
