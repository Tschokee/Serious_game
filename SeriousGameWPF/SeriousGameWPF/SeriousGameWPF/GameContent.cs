using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeriousGameWPF
{
    public enum State
    {
        Default,
        Undefined,
        Solved
    }
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
        private bool _focus;
        public double ViewboxHeight
        {
            get { return _viewboxHeight; }
            set { _viewboxHeight = value;
            OnPropertyChanged("ViewboxHeight");
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
        public bool Collusion(GameContent toTest)
        {
            /*
            this.ViewboxHeight;
            this.ViewboxWidth;
            this.PosX;
            this.PosY; * */

            if (this.PosX > toTest.PosX && this.PosX < toTest.PosX + toTest.ViewboxWidth && this.PosY > toTest.PosY && this.PosY < toTest.PosY + toTest.ViewboxHeight/2)
                return true;
            if (this.PosX + this.ViewboxWidth > toTest.PosX && this.PosX + this.ViewboxWidth < toTest.PosX + toTest.ViewboxWidth && this.PosY > toTest.PosY && this.PosY < toTest.PosY + toTest.ViewboxHeight / 2)
                return true;
            if (this.PosX > toTest.PosX && this.PosX < toTest.PosX + toTest.ViewboxWidth && this.PosY + this.ViewboxHeight / 2 > toTest.PosY && this.PosY + this.ViewboxHeight / 2 < toTest.PosY + toTest.ViewboxHeight / 2)
                return true;
            if (this.PosX + this.ViewboxWidth > toTest.PosX && this.PosX + this.ViewboxWidth < toTest.PosX + toTest.ViewboxWidth && this.PosY + this.ViewboxHeight / 2 > toTest.PosY && this.PosY + this.ViewboxHeight / 2 < toTest.PosY + toTest.ViewboxHeight / 2)
                return true;
            return false;
            
        }

    }
}
