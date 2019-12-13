using System;
using System.Collections.Generic;
using System.Text;

namespace cliant
{
    class TreeData
    {
        public float LocationX { set; get; }
        public float LocationY { set; get; }
        public int GrowStage { set; get; }
        public List<Decoration> Decorations { set; get; }
        public int Point { set; get; }
        public int NumofHuman { set; get; }
        public float Time { set; get; }
        public int DecoMax { set; get; }
        public TreeData(float locationX, float locationY, int growStage, List<Decoration> decorations, int point, int NumofHuman, float time, int decoMax)
        {
            this.LocationX = locationX;
            this.LocationY = locationY;
            this.GrowStage = growStage;
            this.Decorations = decorations;
            this.Point = point;
            this.NumofHuman = NumofHuman;
            this.Time = time;
            this.DecoMax = decoMax;
        }

        public void ContentUpdate()
        {
            throw new NotImplementedException("ContentUpdateは未実装です");
        }
    }
}
