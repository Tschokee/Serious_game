﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeriousGameWPF
{
    public enum State
    {
        Default,
        Undefined,
        Solved,
        Active
    }
    public delegate void SetActive(bool t);
    public class GameContent:Displayable
    {
        private int _pairID;
        private double _defaultPosX;
        private double _defaultPosY;
        private State _state;
        private string _textContent;
        private double _textLeft;
        private double _textTop;
        private double _viewboxHeight;
        private double _viewboxWidth;
        private int _textFontSize;
        private bool _focus;
        private bool _draggable;
        public SetActive SetActive;
        public int TextFontSize
        {
            get { return _textFontSize; }
            set
            {
                _textFontSize = value;
                OnPropertyChanged("TextFontSize");
            }
        }
        public double ViewboxHeight
        {
            get { return _viewboxHeight; }
            set { _viewboxHeight = value;
            OnPropertyChanged("ViewboxHeight");
            }
        }
        public bool Draggable
        {
            get { return _draggable; }
            set
            {
                _draggable = value;
                OnPropertyChanged("Draggable");
            }
        }

        public double ViewboxWidth
        {
            get { return _viewboxWidth; }
            set { _viewboxWidth = value;
            OnPropertyChanged("ViewboxWidth");
            }
        }

        public double TextTop
        {
            get { return _textTop; }
            set { _textTop = value;
            OnPropertyChanged("TextLeft");
            }
        }
        public double TextLeft
        {
            get { return _textLeft; }
            set { _textLeft = value;
            OnPropertyChanged("TextLeft");
            }
        }
        

        public string TextContent
        {
            get { return _textContent; }
            set { _textContent = value;
            OnPropertyChanged("TextContent");
            }
        }
        public int PairID { get { return _pairID; } set { this._pairID = value; OnPropertyChanged("PairID"); } }
        public double DefaultPosX { get { return _defaultPosX; } set { this._defaultPosX = value; OnPropertyChanged("DefaultPosX"); } }
        public double DefaultPosY { get { return _defaultPosY; } set { this._defaultPosY = value; OnPropertyChanged("DefaultPosY"); } }
        public State State { get { return _state; } set { this._state = value; OnPropertyChanged("State"); } }
        public GameContent()
        {
                
        }

        //TODO lehet jó lenne az alapértelmezetten kívül több konstruktor, ezt így nehéz használni

        //A Focus mechanika jó, de nem a méretet kellene növelni hanem valami normálisabb ötlet kellene
        public bool Focus { get{return _focus;}
            set
            {
                if (_focus)
                {
                    if (value == false)
                    {
                        this.ViewboxHeight /= 1.10;
                        this.ViewboxWidth /= 1.10;
                    }
                    _focus = value;
                }
                else
                {
                    if (value==true)
                    {
                    this.ViewboxHeight *= 1.10;
                    this.ViewboxWidth *= 1.10;
                    }
                   
                    _focus = value;
                }
                OnPropertyChanged("Focus");
            }
            }
        private bool CollusionCalc(GameContent toTest)
        {
            int div = 3;
            int div2 = 4;
            if (this.PosX >= toTest.PosX && this.PosX <= toTest.PosX + toTest.ViewboxWidth / div && this.PosY >= toTest.PosY && this.PosY <= toTest.PosY + toTest.ViewboxHeight / div2)
                return true;
            if (this.PosX + this.ViewboxWidth / div >= toTest.PosX && this.PosX + this.ViewboxWidth / div <= toTest.PosX + toTest.ViewboxWidth / div && this.PosY >= toTest.PosY && this.PosY <= toTest.PosY + toTest.ViewboxHeight / div2)
                return true;
            if (this.PosX >= toTest.PosX && this.PosX <= toTest.PosX + toTest.ViewboxWidth / div && this.PosY + this.ViewboxHeight / div2 >= toTest.PosY && this.PosY + this.ViewboxHeight / div2 <= toTest.PosY + toTest.ViewboxHeight / div2)
                return true;
            if (this.PosX + this.ViewboxWidth / div >= toTest.PosX && this.PosX + this.ViewboxWidth / div <= toTest.PosX + toTest.ViewboxWidth / div && this.PosY + this.ViewboxHeight / div2 >= toTest.PosY && this.PosY + this.ViewboxHeight / div2 <= toTest.PosY + toTest.ViewboxHeight / div2)
                return true;

            return false;
        
        }
        public bool Collusion(GameContent toTest)
        {
            /*
            this.ViewboxHeight;
            this.ViewboxWidth;
            this.PosX;
            this.PosY; * */
            
            return this.CollusionCalc(toTest)|toTest.CollusionCalc(this);
            
        }
        

    }
}
