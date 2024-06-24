using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inputScore;

    [SerializeField]
    private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore(){
        Debug.Log(Minuteur.tempTime);
        submitScoreEvent.Invoke(inputName.text, Minuteur.tempTime);
        
    }
}
