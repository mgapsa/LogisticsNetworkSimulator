using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Actor
    {
        public SimulationModel SimulationModel { get; set; }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Actor()
        {
            //default 0 everywhere;
        }

        public Actor(Actor actor, SimulationModel model)
        {
            //this.Id = actor.Id;
            this.ProjectId = actor.ProjectId;
            this.SimulationModel = model;
            //We have to change these below always
            //this.X = actor.X;
            //this.Y = actor.Y;
        }
    }
}
