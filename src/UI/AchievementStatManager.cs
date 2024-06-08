using UnityEngine;
using UnityEngine.UI;

namespace UltraAchievements_Revamped.UI;

public class AchievementStatManager : MonoBehaviour
{
    public Text completedAchievements;
    public Text totalAchievements;
    public Text miniGameScore;

    private void Start()
    {
        int achievements = 0;
        int compAchievements = 0;
        int miniGameScore = 0;
            
        foreach (AchievementInfo info in AchievementManager.IdToAchInfo.Values)
        {
            if (info.isCompleted)
            {
                compAchievements++;
            }

            achievements++;
        }

        totalAchievements.text = $"<color=orange>{achievements}</color> - COMPLETED ACHIEVEMENTS";
        completedAchievements.text = $"<color=orange>{compAchievements}</color> - TOTAL ACHIEVEMENTS";
        
        //TODO add mini game scores
    }
}