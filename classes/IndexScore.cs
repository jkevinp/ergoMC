using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using ProjectK.ErgoMC.Assessment.Library;
namespace ProjectK.ErgoMC.Assessment.classes
{

    public class IndexScore: Model, INotifyPropertyChanged
    //: INotifyPropertyChanged
    {
        public IndexScore()
        {

        }
        public int min = 0;
        public int max = 0;
        public bool isAllowedZero = false;
        public int rula_id = 0;
        public string name = string.Empty;
        public int employee_id = 0;
        public int score = 0;
        public int additionalScore = 0;
        public int SetAscore(int x) { this.additionalScore = x; return this.additionalScore; }
        public int GetAscore() { return this.additionalScore; }
        public int getTotal() { return (this.additionalScore + score); }
        public int getScore() { return this.score; }
        public int SetScore(int x) {score = x; return this.score;}
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
