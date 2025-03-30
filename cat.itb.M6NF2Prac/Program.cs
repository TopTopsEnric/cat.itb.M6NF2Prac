using cat.itb.M6NF2Prac.cruds;
using cat.itb.M6NF2Prac.model;
using System.Net;

namespace cat.itb.M6NF2Prac
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ejercicio9();
        }


        static void Ejercicio1()
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

        static void Ejercicio2()
        {
            var crud = new ClientCRUD();
            var client = crud.SelectByNameADO("Roast Coast");
            crud.DeleteADO(client);
        }

        static void Ejercicio3()
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
        static void Ejercicio4()
        {
            var crud = new ProviderCRUD();

            var proveedores = crud.SelectCreditLowerThanADO(6000);

            for (int i = 0; i < proveedores.Count; i++)
            {
                Provider provider = proveedores[i];
                Console.WriteLine($"Provider -> ID: {provider.id}, Name: {provider.name}, Address: {provider.address}, City: {provider.city}, " +
                       $"State Code: {provider.stcode}, Zipcode: {provider.zipcode}, Area: {provider.area}, Phone: {provider.phone}, " +
                       $"Product: {(provider.product != null ? provider.product.ToString() : "N/A")}, Amount: {provider.amount}, " +
                       $"Credit: {provider.credit}, Remark: {provider.remark}");

            }

        }

        static void Ejercicio5()
        {
            var Vendedores = new List<Salesperson>
            {
             new Salesperson { surname = "WASHINGTON", job = "MANAGER", startdate = new DateTime(1974, 12, 1), salary = 139000, commission = 62000, dep = "REPAIR" },
             new Salesperson { surname = "FORD", job = "ASSISTANT", startdate = new DateTime(1985, 4, 15), salary = 105000, commission = 25000, dep = "REPAIR" },
             new Salesperson { surname = "FREEMAN", job = "ASSISTANT", startdate = new DateTime(1965, 9, 12), salary = 90000, commission = null, dep = "REPAIR" },
             new Salesperson { surname = "DAMON", job = "ASSISTANT", startdate = new DateTime(1995, 11, 15), salary = 90000, commission = null, dep = "WOOD" }

             };

            var crud = new SalespersonCRUD();
            crud.InsertAdo(Vendedores);
        }

        static void Ejercicio6()
        {
            ClientCRUD clientCRUD = new ClientCRUD();

            // Obtenemos el cliente "Carter & Sons" utilizando el método SelectByName
            Client client = clientCRUD.SelectByName("Carter & Sons");

            if (client != null)
            {
                // Contamos el número de comandas
                int numComandes = client.comandes.Count;

                // Calculamos el coste total sumando el coste de todas las comandas
                decimal costTotal = 0;
                foreach (Orders comanda in client.comandes)
                {
                    costTotal += comanda.cost;
                }

                // Mostramos el resultado
                Console.WriteLine($"El client amb id {client.id} ha realitzat {numComandes} i s'ha gastat en total {costTotal}");
            }
            else
            {
                Console.WriteLine("No s'ha trobat el client 'Carter & Sons'");
            }
        }

         static void Ejercicio7()
        {
            SalespersonCRUD salespersonCRUD = new SalespersonCRUD();

            // Obtenemos el vendedor con apellido "YOUNG"
            // Young no me salia con  ningun producto asi que tepongo  "STRONGMAN "que este que si que tiene
            Salesperson vendedor = salespersonCRUD.SelectBySurname("STRONGMAN"); 

            if (vendedor != null)
            {
                Console.WriteLine($"Vendedor: {vendedor.surname}");
                Console.WriteLine("Proveedores de los productos que gestiona:");

                // Recorremos todos los productos que gestiona el vendedor
                foreach (Product producto in vendedor.product)
                {
                    // Accedemos al proveedor de cada producto
                    Provider proveedor = producto.prov;

                    if (proveedor != null)
                    {
                        Console.WriteLine($"Nombre: {proveedor.name}");
                        Console.WriteLine($"Ciudad: {proveedor.city}");
                        Console.WriteLine($"Código Postal: {proveedor.zipcode}");
                        Console.WriteLine($"Teléfono: {proveedor.phone}");
                        Console.WriteLine("------------------------");
                    }
                }
            }
            else
            {
                Console.WriteLine("No se ha encontrado el vendedor con apellido YOUNG");
            }
        }


         static void Ejercicio8()
        {
            OrderCRUD orderCRUD = new OrderCRUD();

            // Obtenemos las comandas con coste > 12000 y cantidad = 100
            IList<Orders> comandas = orderCRUD.SelectByCostHigherThan(12000, 100);

            if (comandas.Count > 0)
            {
                Console.WriteLine($"Se han encontrado {comandas.Count} comandas con coste > 12000 y cantidad = 100:");

                foreach (Orders comanda in comandas)
                {
                    Console.WriteLine($"ID: {comanda.id}");
                    Console.WriteLine($"Fecha del pedido: {comanda.orderdate}");
                    Console.WriteLine($"Cantidad: {comanda.amount}");
                    Console.WriteLine($"Fecha de entrega: {comanda.deliverydate}");
                    Console.WriteLine($"Coste: {comanda.cost}");

                    // Mostramos la descripción y el precio del producto de la comanda
                    Product producto = comanda.product;
                    if (producto != null)
                    {
                        Console.WriteLine($"Descripción del producto: {producto.description}");
                        Console.WriteLine($"Precio del producto: {producto.price}");
                    }

                    Console.WriteLine("------------------------");
                }
            }
            else
            {
                Console.WriteLine("No se han encontrado comandas que cumplan los criterios");
            }
        }

         static void Ejercicio9()
        {
            ProviderCRUD providerCRUD = new ProviderCRUD();

            // Obtenemos el proveedor con la cantidad mínima
            Provider proveedor = providerCRUD.SelectLowestAmount();

            if (proveedor != null)
            {
                Console.WriteLine("Proveedor con la cantidad mínima:");
                Console.WriteLine($"Nombre: {proveedor.name}");
                Console.WriteLine($"Cantidad: {proveedor.amount}");

                // Mostramos la descripción y el stock actual del producto que provee
                Product producto = proveedor.product;
                if (producto != null)
                {
                    Console.WriteLine($"Descripción del producto: {producto.description}");
                    Console.WriteLine($"Stock actual: {producto.currentstock}");
                }
                else
                {
                    Console.WriteLine("Este proveedor no tiene producto asociado");
                }
            }
            else
            {
                Console.WriteLine("No se ha encontrado ningún proveedor");
            }
        }










    }
}
