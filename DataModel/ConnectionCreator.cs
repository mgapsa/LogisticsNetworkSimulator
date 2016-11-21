using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    class ConnectionCreator
    {
        //ma byc w simulation modelu
        //jak klikam na actorow ma sie to tu odolywac i sprawdzac i ustawiac
        //exception handling zeby tam byl try catch i tam mi lapalo i tam bede wiedzial i wysiwetla jakich polaczen nie mozna robic
        //right clikc nulluje mi actorow
        //po dodanu actora wywoluje sprawdzenie czy mozna linie stworzyc - jak tak dodaje do listy connectionow connection 
        //tworze connectionUI, rysuje, dodaje wartosc do HashMapy
        IActorUserControl ActorA;
        IActorUserControl ActorB;

        public Connection AddActor(IActorUserControl actor)
        {
            //logic
            return null;
        }
    }
}
