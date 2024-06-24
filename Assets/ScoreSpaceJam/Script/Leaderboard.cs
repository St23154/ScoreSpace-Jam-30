using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class Leaderboard : MonoBehaviour
{
    string leaderboardID = "GlobalHighscore";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public IEnumerator SubmitScoreRoutine(int scoreToUploud)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUploud, leaderboardID,(response) =>
        {
            if(response.success)
            {
                Debug.Log("Score uplouded");
                done = true;    
            }
            else
            {
                Debug.Log("failed" + response);
                done = true;
            }
        });
        yield return new WaitWhile(() => done==false);
    }
}
