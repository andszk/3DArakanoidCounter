using Assets.Scripts;
using Assets.Scripts.PowerUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

internal class PowerUpFactory
{
    private static List<TypeInfo> powerUps = Assembly.GetExecutingAssembly().DefinedTypes.Where(t => t.BaseType == typeof(PowerUp)).ToList();
    private static Random rnd = new Random();

    internal static PowerUp GetRandomPowerUp()
    {
        var index = rnd.Next(powerUps.Count);
        return Activator.CreateInstance(powerUps[index]) as PowerUp;
    }
}