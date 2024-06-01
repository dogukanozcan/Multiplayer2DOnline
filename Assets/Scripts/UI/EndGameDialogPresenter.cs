using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameDialogPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private Color _greenColor;
    [SerializeField] private Color _blueColor;
    [SerializeField] private TextMeshProUGUI _winnerInfoLabel;
        
    private void Start()
    {
        GameNetworkManager.Instance.OnGameEnd += OnGameEnd;
    }

    public void OnGameEnd()
    {
        _endGamePanel.SetActive(true);
        if (GameNetworkManager.Instance.spawnedPlayers.Count == 0) return;
        var host = GameNetworkManager.Instance.spawnedPlayers[0].IsOwnedByServer;
        if (host)
        {
            _winnerInfoLabel.text = "<#" + ColorUtility.ToHtmlStringRGB(_greenColor).ToString() + "> ";
            _winnerInfoLabel.text += "GREEN</color> WON!";
        }
        else
        {
            _winnerInfoLabel.text = "<#" + ColorUtility.ToHtmlStringRGB(_blueColor).ToString() + "> ";
            _winnerInfoLabel.text += "BLUE</color> WON!";
        }
    }
}
