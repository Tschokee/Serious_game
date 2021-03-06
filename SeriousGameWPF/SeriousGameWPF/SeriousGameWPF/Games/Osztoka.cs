﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using SeriousGameWPF.Static;

namespace SeriousGameWPF.Games
{
    public class PositionElement
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public PositionElement(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class Osztoka:Game
    {
        private List<PositionElement> balloonPositionList = new List<PositionElement>();
        private List<PositionElement> answerPositionList = new List<PositionElement>();

        private void FillPositionLists()
        {
            balloonPositionList.Add(new PositionElement(100, 100));
            balloonPositionList.Add(new PositionElement(160, 200));
            balloonPositionList.Add(new PositionElement(220, 100));
            balloonPositionList.Add(new PositionElement(280, 200));
            balloonPositionList.Add(new PositionElement(340, 200));
            balloonPositionList.Add(new PositionElement(400, 200));
            balloonPositionList.Add(new PositionElement(460, 100));
            balloonPositionList.Add(new PositionElement(520, 100));
            balloonPositionList.Add(new PositionElement(580, 200));
            balloonPositionList.Add(new PositionElement(640, 100));

            answerPositionList.Add(new PositionElement(100, 400));
            answerPositionList.Add(new PositionElement(160, 350));
            answerPositionList.Add(new PositionElement(220, 400));
            answerPositionList.Add(new PositionElement(280, 350));
            answerPositionList.Add(new PositionElement(340, 400));
            answerPositionList.Add(new PositionElement(400, 350));
            answerPositionList.Add(new PositionElement(460, 400));
            answerPositionList.Add(new PositionElement(520, 350));
            answerPositionList.Add(new PositionElement(580, 400));
            answerPositionList.Add(new PositionElement(640, 350));
        }

        public static void Shuffle<T>(IList<T> list)
        {
            var random = new Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = random.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public void GenerateRandomLists() 
        {
            FillPositionLists();
            //Randomize
            Shuffle(balloonPositionList);
            Shuffle(answerPositionList);
        }

        public Osztoka(Start StartGame)
        {
               // GenerateRandomLists();
                ImageUri = MainMenuHandler.ConvertStringToImageSource("/Images/Osztoka/bohoc.png");
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
                GenerateRandomLists();
                MainMenuHandler.SelectedGame.ActiveContent = new ObservableCollection<GameContent>();
                MainMenuHandler.SelectedGame.ActiveContent.Add(new BackgroundContent("Images/Osztoka/osztoka_rule.png", 200, 10, "", 100, 100));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new BackgroundContent("Images/Osztoka/bohoc.png", -15, 465, "", 100, 100));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new BackgroundContent("Images/Osztoka/felho.png", 650, 150, "", 100, 100));
                MainMenuHandler.SelectedGame.ActiveContent.Add(new BackgroundContent("Images/Osztoka/felho_mirrored.png", -15, 100, "", 100, 100));
                GenerateSpecificVariables(gm.StartParameters, MainMenuHandler.SelectedGame.ActiveContent);
            }
           
        }

        private void GenerateSpecificVariables(string param, ObservableCollection<GameContent> activeContent)
        {
            Random random = new Random();
            for (var i = 1; i < 11; i++)
            {
                int ballonRandomizer = random.Next(0, balloonPositionList.Count);
                int answerRandomizer = random.Next(0, answerPositionList.Count);
                //activeContent.Add(new Balloon(i, i*50, i*50, i.ToString()));
                //activeContent.Add(new Answer(i, i * 50+20, i * 50+20, i*int.Parse(param) + ":" + param));

                activeContent.Add(new Balloon(i, balloonPositionList[ballonRandomizer].X, balloonPositionList[ballonRandomizer].Y, i.ToString()));
                balloonPositionList.RemoveAt(ballonRandomizer);
                activeContent.Add(new Answer(i, answerPositionList[answerRandomizer].X, answerPositionList[answerRandomizer].Y, i * int.Parse(param) + ":" + param));
                answerPositionList.RemoveAt(answerRandomizer);
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
            this.TextFontSize = MainMenuHandler.FontSize;
            TextLeft = 45;
            TextTop = 45;
            ViewboxHeight = 70;
            ViewboxWidth = 70;
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
            this.TextFontSize = MainMenuHandler.FontSize-8;
            TextLeft = 35;
            TextTop = 45;
            ViewboxHeight = 105;
            ViewboxWidth = 105;
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
            this.PairID = -1;
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
