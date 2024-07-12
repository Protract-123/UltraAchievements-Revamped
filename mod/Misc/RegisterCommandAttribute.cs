using System;
using System.Reflection;
using GameConsole;
using HarmonyLib;

namespace UltraAchievements_Revamped;

[AttributeUsage(AttributeTargets.Class)]
[HarmonyPatch]
public class RegisterCommandAttribute : Attribute
{
    [HarmonyPatch(typeof(GameConsole.Console), "Awake"), HarmonyPostfix]
    private static void RegisterAll(GameConsole.Console __instance)
    {
        foreach (Type type in Assembly.GetExecutingAssembly().DefinedTypes)
        {
            if (type.GetCustomAttribute<RegisterCommandAttribute>() == null)
            {
                continue;
            }

            __instance.RegisterCommand((ICommand)Activator.CreateInstance(type));
        }
    }
}
