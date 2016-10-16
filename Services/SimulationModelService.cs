using Database;
using Database.Brokers;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SimulationModelService
    {
        public SimulationModel Get(Project project)
        {
            SimulationModel model = new SimulationModel();
            SupplierBroker supplierBroker = new SupplierBroker();
            ShopBroker shopBroker = new ShopBroker();
            BuyerBroker buyerBroker = new BuyerBroker();
            ConnectionBroker connectionBroker = new ConnectionBroker();

            model.Project = project;

            using (IDbConnection connection = new ConnectionProvider().GetConnection(true))
            {
                model.Buyers = buyerBroker.GetAllFromProject(connection, model.Project.Id).ToList();
                model.Shops = shopBroker.GetAllFromProject(connection, model.Project.Id).ToList();
                model.Suppliers = supplierBroker.GetAllFromProject(connection, model.Project.Id).ToList();
                model.Connections = connectionBroker.GetAllFromProject(connection, model.Project.Id).ToList();
            }

            foreach (Connection conn in model.Connections)
            {
                switch(conn.ConnectionType)
                {
                    case Connection.ConnectionTypes.ShopToBuyer:
                        conn.ActorA = model.Shops.Find(x => x.Id == conn.ActorAId);
                        conn.ActorB = model.Buyers.Find(x => x.Id == conn.ActorBId);
                        break;
                    case Connection.ConnectionTypes.ShopToShop:
                        conn.ActorA = model.Shops.Find(x => x.Id == conn.ActorAId);
                        conn.ActorB = model.Shops.Find(x => x.Id == conn.ActorBId);
                        break;
                    case Connection.ConnectionTypes.SupplierToShop:
                        conn.ActorA = model.Suppliers.Find(x => x.Id == conn.ActorAId);
                        conn.ActorB = model.Shops.Find(x => x.Id == conn.ActorBId);
                        break;
                    default:
                        break;
                }
            }

            return model;
        }

        public int Save(SimulationModel model)
        {
            ProjectBroker projectBroker = new ProjectBroker();
            SupplierBroker supplierBroker = new SupplierBroker();
            ShopBroker shopBroker = new ShopBroker();
            BuyerBroker buyerBroker = new BuyerBroker();
            ConnectionBroker connectionBroker = new ConnectionBroker();

            //najpierw zapis podczas ktorego zbieram 
            using (IDbConnection connection = new ConnectionProvider().GetConnection(true))
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (model.Project.Id != 0)
                    {
                        buyerBroker.RemoveNotExistingFromProject(connection, model.Project.Id, model.GetListOfBuyersIds());
                        shopBroker.RemoveNotExistingFromProject(connection, model.Project.Id, model.GetListOfShopsIds());
                        supplierBroker.RemoveNotExistingFromProject(connection, model.Project.Id, model.GetListOfSuppliersIds());
                        connectionBroker.RemoveNotExistingFromProject(connection, model.Project.Id, model.GetListOfConnectionsIds());

                    }

                    projectBroker.Save(connection, model.Project, transaction);

                    foreach (Buyer buyer in model.Buyers)
                    {
                        buyer.ProjectId = model.Project.Id;
                        buyerBroker.Save(connection, buyer, transaction);
                    }

                    foreach (Shop shop in model.Shops)
                    {
                        shop.ProjectId = model.Project.Id;
                        shopBroker.Save(connection, shop, transaction);
                    }

                    foreach (Supplier supplier in model.Suppliers)
                    {
                        supplier.ProjectId = model.Project.Id;
                        supplierBroker.Save(connection, supplier, transaction);
                    }

                    foreach (Connection conn in model.Connections)
                    {
                        conn.ProjectId = model.Project.Id;
                        conn.ActorAId = conn.ActorA.Id;
                        conn.ActorBId = conn.ActorB.Id;
                        connectionBroker.Save(connection, conn, transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
            return 1;
        }

        public int SaveAs(SimulationModel model)
        {
            ProjectBroker projectBroker = new ProjectBroker();
            SupplierBroker supplierBroker = new SupplierBroker();
            ShopBroker shopBroker = new ShopBroker();
            BuyerBroker buyerBroker = new BuyerBroker();
            ConnectionBroker connectionBroker = new ConnectionBroker();

            //I will set all ids to 0, so it wil add new rows and inserts function
            //set id to proper one. 
            using (IDbConnection connection = new ConnectionProvider().GetConnection(true))
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    model.Project.Id = 0;
                    projectBroker.Save(connection, model.Project, transaction);

                    foreach (Buyer buyer in model.Buyers)
                    {
                        buyer.Id = 0;
                        buyerBroker.Save(connection, buyer, transaction);
                    }

                    foreach (Shop shop in model.Shops)
                    {
                        shop.Id = 0;
                        shopBroker.Save(connection, shop, transaction);
                    }

                    foreach (Supplier supplier in model.Suppliers)
                    {
                        supplier.Id = 0;
                        supplierBroker.Save(connection, supplier, transaction);
                    }

                    foreach (Connection conn in model.Connections)
                    {
                        conn.Id = 0;
                        conn.ActorAId = conn.ActorA.Id;
                        conn.ActorBId = conn.ActorB.Id;
                        connectionBroker.Save(connection, conn, transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
            return 1;
        }
    }
}
