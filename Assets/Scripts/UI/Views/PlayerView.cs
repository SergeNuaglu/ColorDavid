using TMPro;
using UnityEngine;

public class PlayerView : View
{
    [SerializeField] private TMP_Text _playerNumber;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerScore;

    private int _number;
    private string _name;
    private int _score;
    private bool _isUser;

    public void Init(int playerNumber, string playerName, int playerScore, bool isUser = false)
    {
        _number = playerNumber;
        _name = playerName;
        _score = playerScore;
        _isUser = isUser;
    }

    public override void Render()
    {
        _playerNumber.text = _number.ToString();
        _playerName.text = _name;
        _playerScore.text = _score.ToString();

        if (_isUser)
            ActivityStateFrame.TurnOn();
        else
            ActivityStateFrame.TurnOff();
    }
}
