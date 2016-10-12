using Dapper.FluentMap;
using Database.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class DapperConfiguration
    {
        private static bool mapped;
        public static void Map()
        {
            if (mapped)
            {
                return;
            }
            FluentMapper.Initialize(config =>
            {
                mapped = true;
                config.AddMap(new ProjectMapper());
                config.AddMap(new BuyerMapper());
                config.AddMap(new ShopMapper());
                config.AddMap(new SupplierMapper());
                config.AddMap(new ConnectionMapper());
            });
        }
    }
}
