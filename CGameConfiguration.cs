using Rocket.API;

namespace Simuciokas.SimuciokasCGames
{
    public class SimuciokasCGamesConfiguration : IRocketPluginConfiguration
    {
        public int Every_NumberOfSeconds = 120;
        public int Default_Amount_For_Sum = 10;
        public int Default_Amount_For_Minus = 10;
        public int Default_Amount_For_Multiplication = 10;
        public int Default_Amount_For_Division = 10;
        public int Extra_Amount_For_Sum = 50;
        public int Extra_Amount_For_Minus = 50;
        public int Extra_Amount_For_Multiplication = 50;
        public int Extra_Amount_For_Division = 50;
        public bool Custom_Extra_Amount_Enabled = false;
        public int Feex_Points_For_Sum = 1;
        public int Feex_Points_For_Minus = 1;
        public int Feex_Points_For_Multiplication = 1;
        public int Feex_Points_For_Division = 1;
        public bool Use_Feex_Points = false;


        public void LoadDefaults()
        {
            Every_NumberOfSeconds = 120;
            Default_Amount_For_Sum = 10;
            Default_Amount_For_Minus = 10;
            Default_Amount_For_Multiplication = 10;
            Default_Amount_For_Division = 10;
            Extra_Amount_For_Sum = 50;
            Extra_Amount_For_Minus = 50;
            Extra_Amount_For_Multiplication = 50;
            Extra_Amount_For_Division = 50;
            Custom_Extra_Amount_Enabled = false;
            Feex_Points_For_Sum = 1;
            Feex_Points_For_Minus = 1;
            Feex_Points_For_Multiplication = 1;
            Feex_Points_For_Division = 1;
            Use_Feex_Points = false;

        }
    }
}
