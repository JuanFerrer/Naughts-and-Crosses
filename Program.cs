namespace NaughtsAndCrosses
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager.Init();
            UserSelection userSelection = UserSelection.None;
            do
            {
                Menu.ShowMenu();
                userSelection = Menu.MakeUserSelect();

                switch (userSelection)
                {
                    case UserSelection.Start:
                        {
                            GameManager.StartGame();
                            do
                            {
                                GameManager.TurnCycle();
                            } while (GameManager.InGame);

                            Menu.ShowResults();
                            break;
                        }
                    case UserSelection.Options:
                        {
                            Menu.ShowOptions();
                            if (Menu.ReadYesNo()) GameManager.ChangeGameMode();
                            break;
                        }
                    case UserSelection.Exit:
                        {
                            Menu.ShowExit();
                            userSelection = Menu.ReadYesNo() ? UserSelection.Exit : UserSelection.None;
                            break;
                        }
                }
            } while (userSelection != UserSelection.Exit);
        }
    }
}
