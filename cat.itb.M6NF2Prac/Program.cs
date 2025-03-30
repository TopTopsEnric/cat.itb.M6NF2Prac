using cat.itb.M6NF2Prac.cruds;
using cat.itb.M6NF2Prac.model;
using NHibernate.Criterion;
using System.Net;

namespace cat.itb.M6NF2Prac
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Menú:");
                Console.WriteLine("1. Ejecutar Drop And Script");
                Console.WriteLine("2. Ejercicio 1");
                Console.WriteLine("3. Ejercicio 2");
                Console.WriteLine("4. Ejercicio 3");
                Console.WriteLine("5. Ejercicio 4");
                Console.WriteLine("6. Ejercicio 5");
                Console.WriteLine("7. Ejercicio 6");
                Console.WriteLine("8. Ejercicio 7");
                Console.WriteLine("9. Ejercicio 8");
                Console.WriteLine("10. Ejercicio 9");
                Console.WriteLine("11. Ejercicio 10");
                Console.WriteLine("12. Ejercicio 11");
                Console.WriteLine("13. Ejercicio 12");
                Console.WriteLine("14. Ejercicio 13");
                Console.WriteLine("15. Ejercicio 14");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ExecuteDropAndScript();
                        break;
                    case "2":
                        Ejercicio1();
                        break;
                    case "3":
                        Ejercicio2();
                        break;
                    case "4":
                        Ejercicio3();
                        break;
                    case "5":
                        Ejercicio4();
                        break;
                    case "6":
                        Ejercicio5();
                        break;
                    case "7":
                        Ejercicio6();
                        break;
                    case "8":
                        Ejercicio7();
                        break;
                    case "9":
                        Ejercicio8();
                        break;
                    case "10":
                        Ejercicio9();
                        break;
                    case "11":
                        Ejercicio10();
                        break;
                    case "12":
                        Ejercicio11();
                        break;
                    case "13":
                        Ejercicio12();
                        break;
                    case "14":
                        Ejercicio13();
                        break;
                    case "15":
                        Ejercicio14();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }

                // Pausa para ver los logs antes de regresar al menú
                if (!exit)
                {
                    Console.WriteLine("\nPresione una tecla para regresar al menú...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Gracias por usar el programa. ¡Adiós!");
        }



        static void ExecuteDropAndScript()
        {
            var crud= new GeneralCRUD();
            // Lista de tablas a eliminar
            List<string> tables = new List<string>
            {
              "client", "orderprod", "product", "provider", "salesperson"
             };

            // Llamar a DropTables para eliminar las tablas
            crud.DropTables(tables);

            // Llamar a RunScriptHR para ejecutar el script
            crud.RunScriptHR();
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
            var preus = new List<decimal>{
                59.05m ,25.56m,33.12m,17.34m
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

        static void Ejercicio10()
        {
            ProductCRUD productCRUD = new ProductCRUD();
            ProviderCRUD providerCRUD = new ProviderCRUD();
            // Obtener un vendedor existente para asignarlo a los nuevos productos
            SalespersonCRUD salespersonCRUD = new SalespersonCRUD();
            Salesperson vendedor = salespersonCRUD.SelectBySurname("YOUNG"); // Podríamos usar otro vendedor si es necesario

            // Crear primer producto (sin referencia al proveedor inicialmente)
            Product producto1 = new Product
            {
                code = 244234,
                description = "Martillo profesional reforzado",
                currentstock = 50,
                minstock = 10,
                price = 45.99m,
                salesp = vendedor,
                ordenes = new HashSet<Orders>(),
                prov = null // Inicialmente sin proveedor
            };

            // Crear el proveedor para el primer producto (sin asignar el producto aún)
            Provider proveedor1 = new Provider
            {
                name = "Herramientas ProTools",
                address = "123 Calle Industrial",
                city = "Barcelona",
                stcode = "BC",
                zipcode = "08001",
                area = 05025,
                phone = "934567890",
                amount = 200,
                credit = 15000,
                remark = "Proveedor de herramientas profesionales",
                product = producto1 // Inicialmente sin producto
            };

            // Crear segundo producto (sin referencia al proveedor inicialmente)
            Product producto2 = new Product
            {
                code = 645002,
                description = "Kit_destornilladores",
                currentstock = 30,
                minstock = 5,
                price = 89.95m,
                salesp = vendedor,
                ordenes = new HashSet<Orders>(),
                prov = null // Inicialmente sin proveedor
            };

            // Crear el proveedor para el segundo producto (sin asignar el producto aún)
            Provider proveedor2 = new Provider
            {
                name = "ElectroTools S.L.",
                address = "456 Avenida Tecnológica",
                city = "Madrid",
                stcode = "MA",
                zipcode = "28001",
                area = 08023,
                phone = "917654321",
                amount = 150,
                credit = 20000,
                remark = "Especialistas en herramientas eléctricas",
                product = producto2 // Inicialmente sin producto
            };

            // Insertar los productos y proveedores
            try
            {
                // Insertamos primero los productos
                productCRUD.Insert(producto1);
                productCRUD.Insert(producto2);

                // Ahora asignamos los productos a los proveedores
                proveedor1.product = producto1;
                proveedor2.product = producto2;

                // Insertamos los proveedores con los productos ya guardados
                providerCRUD.Insert(proveedor1);
                providerCRUD.Insert(proveedor2);


                Console.WriteLine("Se han insertado correctamente los dos nuevos productos y sus proveedores");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante la inserción: {ex.Message}");
                // Opcional: mostrar más detalles del error si es una excepción anidada
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Detalle: {ex.InnerException.Message}");
                }
            }
        }

        public static void Ejercicio11()
        {
            ClientCRUD clientCRUD = new ClientCRUD();

            // Obtenemos todos los clientes
            IList<Client> clientes = clientCRUD.SelectAll();

            if (clientes.Count > 0)
            {
                Console.WriteLine($"Se han encontrado {clientes.Count} clientes:");

                foreach (Client cliente in clientes)
                {
                    Console.WriteLine($"ID: {cliente.id}");
                    Console.WriteLine($"Código: {cliente.code}");
                    Console.WriteLine($"Nombre: {cliente.name}");
                    Console.WriteLine($"Crédito: {cliente.credit}");
                    Console.WriteLine("------------------------");
                }
            }
            else
            {
                Console.WriteLine("No se ha encontrado ningún cliente");
            }
        }

        public static void Ejercicio12()
        {
            ProviderCRUD providerCRUD = new ProviderCRUD();

            // Obtenemos los proveedores de BELMONT
            IList<Provider> proveedoresBelmont = providerCRUD.SelectByCity("BELMONT");

            if (proveedoresBelmont.Count > 0)
            {
                Console.WriteLine($"Se han encontrado {proveedoresBelmont.Count} proveedores en BELMONT");

                int actualizados = 0;

                foreach (Provider proveedor in proveedoresBelmont)
                {
                    Console.WriteLine($"Actualizando crédito del proveedor {proveedor.name} de {proveedor.credit} a 25000");

                    // Actualizamos el crédito a 25000
                    proveedor.credit = 25000;

                    // Guardamos los cambios
                    bool resultado = providerCRUD.UpdateHQL(proveedor);

                    if (resultado)
                    {
                        actualizados++;
                        Console.WriteLine("Actualización exitosa");
                    }
                    else
                    {
                        Console.WriteLine("Error al actualizar");
                    }
                }

                Console.WriteLine($"Se han actualizado {actualizados} de {proveedoresBelmont.Count} proveedores de BELMONT");
            }
            else
            {
                Console.WriteLine("No se ha encontrado ningún proveedor en BELMONT");
            }
        }

        public static void Ejercicio13()
        {
            ProductCRUD productCRUD = new ProductCRUD();

            // Obtenemos los productos con precio superior a 100
            IList<object[]> productos = productCRUD.SelectByPriceHigherThan(100);

            if (productos.Count > 0)
            {
                Console.WriteLine($"Se han encontrado {productos.Count} productos con precio superior a 100:");

                foreach (object[] producto in productos)
                {
                    string descripcion = (string)producto[0];
                    decimal precio = (decimal)producto[1];

                    Console.WriteLine($"Descripción: {descripcion}");
                    Console.WriteLine($"Precio: {precio}");
                    Console.WriteLine("------------------------");
                }
            }
            else
            {
                Console.WriteLine("No se ha encontrado ningún producto con precio superior a 100");
            }
        }

        public static void Ejercicio14()
        {
            ClientCRUD clientCRUD = new ClientCRUD();

            // Obtenemos los clientes con crédito superior a 50000
            IList<Client> clientes = clientCRUD.SelectByCreditHigherThan(50000);

            if (clientes.Count > 0)
            {
                Console.WriteLine($"Se han encontrado {clientes.Count} clientes con crédito superior a 50000:");

                foreach (Client cliente in clientes)
                {
                    Console.WriteLine($"Nombre: {cliente.name}");
                    Console.WriteLine($"Crédito: {cliente.credit}");
                    Console.WriteLine("------------------------");
                }
            }
            else
            {
                Console.WriteLine("No se ha encontrado ningún cliente con crédito superior a 50000");
            }
        }

    }
}
