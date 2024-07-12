using GameConsole;

namespace UltraAchievements_Lib.Commands;

[RegisterCommand]
public class GiveAchievement : ICommand
{
    public void Execute(Console con, string[] args)
    {
        if (args.Length == 0 || !AchievementManager.IdToAchInfo.TryGetValue(args[0], out AchievementInfo achInfo))
        {
            return;
        }
        
        AchievementManager.MarkAchievementComplete(achInfo);
    }

    public string Name => "Add item";
    public string Description => "Gives you an achievement based on the specified ID";
    public string Command => "ual_addach";
}