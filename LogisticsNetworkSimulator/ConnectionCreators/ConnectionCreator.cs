using DataModel;
using LogisticsNetworkSimulator.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LogisticsNetworkSimulator.ConnectionCreators
{
    public class ConnectionCreator
    {
        //exception handling zeby tam byl try catch i tam mi lapalo i tam bede wiedzial i wysiwetla jakich polaczen nie mozna robic
        //tworze connectionUI, rysuje, dodaje wartosc do HashMapy
        public Canvas Target { get; set; }
        public SimulationModel Model { get; set; }
        IActorUserControl ActorA;
        IActorUserControl ActorB;
        public Dictionary<Connection, ConnectionUI> ConnectionDictionary = new Dictionary<Connection,ConnectionUI>();

        public ConnectionCreator(SimulationModel model, Canvas target)
        {
            this.Target = target;
            this.Model = model;
        }

        public void AddActor(IActorUserControl actor)
        {
            if (ActorA == null)
            {
                if(actor.GetUserControlType() == EnumTypes.UserControlTypes.BuyerUserControl)
                {
                    throw new ConnectionException(ConnectionException.ILLEGALCONNECTIONFROMBUYER);
                }
                ActorA = actor;
            }
            else if (ActorA != actor)
            {
                if (actor.GetUserControlType() == EnumTypes.UserControlTypes.SupplierUserControl)
                {
                    throw new ConnectionException(ConnectionException.ILLEGALCONNECTIONTOSUPPLIER);
                }
                if(ActorA.GetUserControlType() == EnumTypes.UserControlTypes.SupplierUserControl && actor.GetUserControlType() == EnumTypes.UserControlTypes.BuyerUserControl)
                {
                    throw new ConnectionException(ConnectionException.ILLEGALCONNECTIONSUPPLIERBUYER);
                }

                ActorB = actor;

                CheckForDuplicate();
            }
        }

        public void CheckForDuplicate()
        {
            if(Model.Connections.Where(c => c.ActorA == this.ActorA.GetActor() && c.ActorB == this.ActorB.GetActor()).Count() != 0)
            {
                ActorB = null;
                throw new ConnectionException(ConnectionException.CONNECTIONALREADYEXISTS);
            }
        }

        public void CreateConnectionIfPossible()
        {
            ConnectionUI connectionUI;
            Connection conn;
            if(ActorA != null && ActorB != null)
            {
                switch(ActorA.GetUserControlType())
                {
                    case EnumTypes.UserControlTypes.ShopUserControl:
                        ShopUserControl ShopA = (ShopUserControl)ActorA;
                        switch(ActorB.GetUserControlType())
                        {
                            case EnumTypes.UserControlTypes.ShopUserControl:
                                ShopUserControl ShopB = (ShopUserControl)ActorB;
                                conn = new Connection(ShopA.ShopModel, ShopB.ShopModel, EnumTypes.ConnectionTypes.ShopToShop);
                                connectionUI = new ConnectionUI(this.Model, Target, conn, this);
                                ConnectionDictionary.Add(conn,connectionUI);
                                this.Model.Connections.Add(conn);
                                connectionUI.PrintOnTarget();
                                ShopA.Reprint(Target);
                                ShopB.Reprint(Target);
                                break;
                            case EnumTypes.UserControlTypes.BuyerUserControl:
                                BuyerUserControl BuyerB = (BuyerUserControl)ActorB;
                                conn = new Connection(ShopA.ShopModel, BuyerB.BuyerModel, EnumTypes.ConnectionTypes.ShopToBuyer);
                                connectionUI = new ConnectionUI(this.Model, Target, conn, this);
                                ConnectionDictionary.Add(conn,connectionUI);
                                this.Model.Connections.Add(conn);
                                connectionUI.PrintOnTarget();
                                ShopA.Reprint(Target);
                                BuyerB.Reprint(Target);
                                break;
                        }
                        break;
                    case EnumTypes.UserControlTypes.SupplierUserControl:
                        SupplierUserControl SuppA = (SupplierUserControl)ActorA;
                        ShopUserControl ShopB2 = (ShopUserControl)ActorB;
                        conn = new Connection(SuppA.SupplierModel, ShopB2.ShopModel, EnumTypes.ConnectionTypes.SupplierToShop);
                        connectionUI = new ConnectionUI(this.Model, Target, conn, this);
                        ConnectionDictionary.Add(conn,connectionUI);
                        this.Model.Connections.Add(conn);
                        connectionUI.PrintOnTarget();
                        SuppA.Reprint(Target);
                        ShopB2.Reprint(Target);
                        break;
                }
                //RYSUJE - czyli 
                this.ActorA = null;
                this.ActorB = null;
            }
        }

        public void CancelConnection()
        {
            ActorA = null;
            ActorB = null;
        }
    }
}
