﻿using Micropack.DesignPattern.Domain.Strategy;

namespace Micropack.DesignPattern.Domain
{
    // this class is not SRP => use FactoryMethod to fixed it
    // only when we add new RoundingMethods type we must change this class
    // but if change any RoundingMethods class, this class must change

    // this class is not OCP => use FactoryMethod to fixed it
    // when we add new RoundingMethods type this means we change this class
    public class SettingSimpleFactory
    {
        public static RoundingMethodStrategy GetRoundingMethodStrategy(Setting setting)
        {
            return setting.RoundingMethod switch
            {
                RoundingMethods.RoundToUp => new RoundToUp(),
                RoundingMethods.RoundToDown => new RoundToDown(),
                RoundingMethods.RoundToNearest => new RoundToNearest(),
                _ => new NullRounding(),
            };
        }
    }
}
