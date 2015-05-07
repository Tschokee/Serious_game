using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SeriousGameWPF.Static;

namespace SeriousGameWPF.Games
{
    public class Osztoka:Game
    {

        public Osztoka(Start StartGame)
        {

                ImageUri = MainMenuHandler.ConvertStringToImageSource("/Images/osztoka.jpg");
                Name = "Osztóka";
                Start = StartGame;
                this.GenerateActiveContent = GenerateActiveContentforOsztoka;
                this.ResultCheck = ResultCheckforOsztoka;
                
                GameModes = new ObservableCollection<GameMode>();
                Background=new SolidColorBrush(Color.FromArgb(0xff,0xcc,0xff,0xff));
                for (int i = 0; i < 10; i++)
                {
                    this.GameModes.Add(new GameMode() { GameDesc = (i + 1).ToString(), StartParameters = (i + 1).ToString() });
                }
          
        }
        
        public void ResultCheckforOsztoka()
        {

            foreach (GameContent result in this.ActiveContent)
            {
                result.Draggable = false;
                if (result is IResult)
                {
                    if ((result as GameContent).State == State.Solved)
                    {
                        GameContent temp;
                        if (this.CollusionTest(result as GameContent, out temp))
                        {
                            if (temp.PairID == (result as GameContent).PairID)
                            {
                                temp.State = State.Solved;
                                (result as GameContent).State = State.Solved;
                            }
                            else temp.State = State.Default;
                        }
                        else (result as GameContent).State = State.Default;
                    }
                }
            }
            //TODO implement this
            foreach (GameContent gameContent in this.ActiveContent)
            {
                switch (gameContent.State)
                { case State.Solved:
                    {
                    gameContent.ImageUri=MainMenuHandler.ConvertStringToImageSource("Images/Osztoka/greenBalloon.png");
                    break;
                    }
                case State.Default:
                    {
                        gameContent.ImageUri = MainMenuHandler.ConvertStringToImageSource("Images/Osztoka/redBalloon.png");
                        break;
                    }


                } 
                
            }
        }
        public void GenerateActiveContentforOsztoka(GameMode gm)
        {
            //itt kell eldönteni hogy mit akarsz kirajzolni gamemode függvényében
            if (gm.StartParameters!=null)
            {
                MainMenuHandler.SelectedGame.ActiveContent = new ObservableCollection<GameContent>();
                MainMenuHandler.SelectedGame.ActiveContent.Add(new BackgroundContent("Images/osztoka.jpg", -15, 400, "", 100, 100));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new BackgroundContent("Images/Osztoka/felho.png", 650, 10, "", 100, 100));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new Balloon(1, 200, 50, "10"));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new Answer (1, 200, 400, "10"));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new Balloon(2, 200, 150, "14"));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new Answer (2, 350, 400, "14"));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new Balloon(3, 200, 250, "15"));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new Answer (3, 500, 400, "15"));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new Balloon(4, 200, 350, "20"));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new Answer (4, 650, 400, "20"));
            }
           
        }
    }
    public class Balloon : GameContent
    {

        public Balloon(int PairID, double PosX,double PosY,string TextContent)
        {
            DefaultPosX = 400;
            DefaultPosY = 400;
            this.ZIndex = 1;
            ImageUri = MainMenuHandler.ConvertStringToImageSource("Images/Osztoka/blueBalloon.png");
            Name = "Balloon";
            this.PairID = PairID;
            this.PosX = PosX;
            this.PosY = PosY;
            this.TextContent = TextContent;
            TextLeft = 45;
            TextTop = 45;
            ViewboxHeight = 100;
            ViewboxWidth = 100;
            Draggable = true;
            State = State.Default;
            this.SetActive = SetActiveforOsztoka;
        }
        public void SetActiveforOsztoka(bool t)
        {
            if (t)
            {
                ImageUri = MainMenuHandler.ConvertStringToImageSource("Images/Osztoka/pinkBalloon.png");
            }
            else
            {
                ImageUri = MainMenuHandler.ConvertStringToImageSource("Images/Osztoka/blueBalloon.png");
            }

        }
   }
    public class Answer : GameContent,IResult
    {

        public Answer(int PairID, double PosX, double PosY, string TextContent)
        {
            DefaultPosX = 400;
            DefaultPosY = 400;
            this.ZIndex = 0;
            ImageUri = MainMenuHandler.ConvertStringToImageSource("Images/Osztoka/yellowBalloon.png");
            Name = "BalloonAnswer";
            this.PairID = PairID;
            this.PosX = PosX;
            this.PosY = PosY;
            this.TextContent = TextContent;
            TextLeft = 45;
            TextTop = 45;
            ViewboxHeight = 150;
            ViewboxWidth = 150;
            State = State.Default;
            Draggable = false;
        }
    }
    public class BackgroundContent : GameContent
    {

        public BackgroundContent(string ImageUri, double PosX, double PosY, string TextContent,double Height, double Width)
        {
            DefaultPosX = 400;
            DefaultPosY = 400;
            this.ZIndex = 0;
            this.ImageUri = MainMenuHandler.ConvertStringToImageSource(ImageUri);
            Name = "BackgroundContent";
            this.PairID = PairID;
            this.PosX = PosX;
            this.PosY = PosY;
            this.TextContent = TextContent;
            TextLeft = 0;
            TextTop  = 0;
            this.ViewboxHeight = Height;
            this.ViewboxWidth = Width;
            State = State.Undefined;
            Draggable = false;
        }
    }
    
}
