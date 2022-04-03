using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static class Save
    {
        public const string Coins = nameof(Coins);
        public const string Level = nameof(Level);
        public const string LevelIndex = nameof(LevelIndex);
        public const string LevelSection = nameof(LevelSection);
        public const string CurrentCarId = nameof(CurrentCarId);
        public const string BoughtCarsCount = nameof(BoughtCarsCount);
        public const string CurrentLauncherId = nameof(CurrentLauncherId);
    }

    public static class Math
    {
        public const float Epsilon = 0.1f;
        public const int Health = 120;
    }

    public static class Scens
    {
        public const string Map1 = nameof(Map1);
    }

    public static class Bot
    {
        public static List<string> Nicknames = new List<string> {
            "Sam", "Daniil", "Datuloar", "Magick", "Stick",
            "Lara", "Alex", "Maga", "Lex", "Kirill", "Alexchrnv",
            "Kala3bk", "Niagara", "Black", "Lima", "Sanchez",
            "D3mid", "M0nesy", "S1mple", "Karba", "Leo", "Mamba",
            "M1lif", "Sum", "Sam", "Seka", "Lombard", "Gerich",
            "ITaxa", "Leha", "Jon", "Scar", "Teeky", "B3brO",
            "Dungeon", "Master", "Kira", "Navlny", "Putin",
            "Kot", "Bish", "Pash", "Rock", "Coi",
             "Max", "Korj", "Lutor", "Vactor", "Aim", "Bomber",
            "Ez4Me", "LegaX", "Canadia", "Bombla", "Sergo", "Vlad",
            "Tim", "Vactor", "Semen", "Peskov", "Jaba",
            "Phone", "Droid", "BogAima", "NoName", "Space"
        };
    }

    public static class Assets
    {
        public const string PlayerCarPath = "Cars/PlayerCar";
        public const string EnemyCarPath = "Cars/EnemyCar";
    }
}
