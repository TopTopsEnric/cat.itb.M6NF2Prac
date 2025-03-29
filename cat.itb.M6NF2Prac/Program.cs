using cat.itb.M6NF2Prac.cruds;
using cat.itb.M6NF2Prac.model;
using System.Net;

namespace cat.itb.M6NF2Prac
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ActualitzarProducte();
        }


        static void InsertClients()
        {
            var clientes = new List<Client>
            {
                new Client { code = 2998, name = "Sun Systems", credit=45000},
                new Client { code = 2677, name = "Roxy Stars", credit=45000},
                new Client { code = 2865, name = "Clen Ferrant", credit=45000},
                new Client { code = 2873, name = "Roast Coast", credit=45000}
             };

            var crud = new ClientCRUD();
            crud.InsertAdo(clientes);
        }

        static void EliminacioClient()
        {
            var crud = new ClientCRUD();
            var client = crud.SelectByNameADO("Roast Coast");
            crud.DeleteADO(client);
        }

        static void ActualitzarProducte()
        {
            var crud = new ProductCRUD();
            var preus = new List<double>{
                59.05,25.56,33.12,17.34
            };

            var productos = new List<Product>
            {
                crud.SelectByCodeADO(100890),crud.SelectByCodeADO(200376),crud.SelectByCodeADO(200380),crud.SelectByCodeADO(100861)
            };

            for (int i = 0; i < productos.Count; i++)
            {
                Product product = productos[i];
                product.price = preus[i];
                crud.UpdateADO(product);
                Console.WriteLine($"Producto {product.code}: Precio asignado {product.price}");

            }

        }










    }
}
