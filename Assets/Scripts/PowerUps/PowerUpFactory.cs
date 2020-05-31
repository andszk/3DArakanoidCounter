using Assets.Scripts;
using Assets.Scripts.PowerUps;
using System;

internal class PowerUpFactory
{
    internal static PowerUp GetRandomPowerUp()
    {
        return new TripleBalls();
    }
}