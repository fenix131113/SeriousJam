namespace Core
{
    public static class GameVariables
    {
        public static float MoneyPerClickMultiplier { get; private set; } = 1;
        public static float MoneyPerPassiveTick { get; private set; }

        public static void SetMoneyPerClick(float multiplier) => MoneyPerClickMultiplier = multiplier;
        
        public static void SetPassivePerTick(float multiplier) => MoneyPerPassiveTick = multiplier;
    }
}