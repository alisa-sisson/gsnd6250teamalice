using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static class Prompts
    {
        public const string Jump = "Press [Space] to jump.";
        public const string Crouch = "Press [Left Ctrl] to crouch.";
        public const string Pickup= "Press [E] to pick up an object.";
    }

    public static class Tags
    {
        public const string PlayerHand = "PlayerHand";
        public const string Player = "Player";
        public const string Stuff = "Stuff";
        public const string Sphere = "Sphere";
        public const string Cylinder = "Cylinder";
        public const string Cube = "Cube";
    }

    public static class Items
    {
        public const string ShrinkPotion = "Shrink Potion";
        public const string NormalPotion = "Normal Potion";
        public const string GrowPotion = "Grow Potion";
    }
}
