using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Actor
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double[] GraphData { get; set; }

        public Actor()
        {
            //default 0 everywhere;
        }

        public Actor(Actor actor)
        {
            //this.Id = actor.Id;
            this.ProjectId = actor.ProjectId;
            //We have to change these below always
            //this.X = actor.X;
            //this.Y = actor.Y;
        }

        public virtual void SetDataSize(int size)
        {
            GraphData = new double[size];
        }
    }
}
