using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ConnectionCreator
    {
        //exception handling zeby tam byl try catch i tam mi lapalo i tam bede wiedzial i wysiwetla jakich polaczen nie mozna robic
        //tworze connectionUI, rysuje, dodaje wartosc do HashMapy
        public Object Target { get; set; }
        public SimulationModel Model { get; set; }
        IActorUserControl ActorA;
        IActorUserControl ActorB;

        public ConnectionCreator(SimulationModel model, Object target)
        {
            this.Target = target;
            this.Model = model;
        }

        public void AddActor(IActorUserControl actor)
        {
            if(ActorA == null)
            {
                ActorA = actor;
            }
            else if(ActorA != actor)
            {
                ActorB = actor;
            }
        }

        public void CreateConnectionIfPossible()
        {
            if(ActorA != null && ActorB != null)
            {
                //tworze LINIE - wywolac konstruktor graficzny, on niech tworzy w sobie linie jako model
                //DODAJE do modelu
                //RYSUJE - czyli 
                //linia ma miec simulationmodel!!!!
            }
        }

        public void CancelConnection()
        {
            ActorA = null;
            ActorB = null;
        }
    }
}
