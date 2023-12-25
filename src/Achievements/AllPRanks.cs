using System.Collections.Generic;
using System.Linq;

namespace UltraAchievements_Revamped.Achievements;

[RegisterAchievement("ultraAchievements.allprank")]
public class AllPRanks
{
    private static List<int> _levelInts =
        [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 666, 667];
    public static void CheckRanks()
    {
        foreach (int levelNum in _levelInts)
        {
            RankData levelData = GameProgressSaver.GetRank(levelNum);
            
            if (!levelData.ranks.Contains(12))
            {
                return;
            }
            
        }
        AchievementManager.MarkAchievementComplete(AchievementManager.GetAchievementInfo(typeof(AllPRanks)));

    }
}