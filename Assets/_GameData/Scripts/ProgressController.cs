using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressController : MonoBehaviour
{
    public static Action<int,Vector3> OnScore;
    private readonly int MaxScore = 500;
    private int _currentScore;
    public Image progressImage;
    public TMP_Text progressText;
    public GameObject scorePrefab;
    public void OnEnable()
    {
        OnScore += OnScoreHandler;
    }

    private void Awake()
    {
        progressImage.fillAmount = 0;
        progressText.text = _currentScore + "/" + MaxScore;
    }

    private void OnScoreHandler(int score,Vector3 pos)
    {
        Destroy(Instantiate(scorePrefab, pos, Quaternion.identity),2);
        _currentScore = Mathf.Min(MaxScore, _currentScore + score);
        progressImage.fillAmount = (float)_currentScore / MaxScore;
        progressText.text = _currentScore + "/" + MaxScore;
    }

    public void OnDestroy()
    {
        OnScore -= OnScoreHandler;
    }
}
