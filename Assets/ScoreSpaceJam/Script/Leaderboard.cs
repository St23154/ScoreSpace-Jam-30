using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;   
    [SerializeField]
    private List<TextMeshProUGUI> scores;
    // Start is called before the first frame update
    private string publicLeaderboardKey = "50c1ca119b4372b4d0dccd42709d6d068d2f23e38035ef76e5ed1b951668d639";

private void Start(){
    GetLeaderboard();
}
    public void GetLeaderboard(){
        //Debug.Log("rsfrdfv");
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg)=>{
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++){
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score){
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username,
        score, ((msg)=>{
            GetLeaderboard();
        }));
    }
}
