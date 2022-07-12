using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using TareaPrograSocketCliente.Comunicacion;

namespace TareaPrograSocketCliente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            string servidor = ConfigurationManager.AppSettings["servidor"];
            Console.ForegroundColor = ConsoleColor.Green;            
            Console.WriteLine("Conectando a servidor {0} en puerto {1}", servidor, puerto);
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);
            if (clienteSocket.Conectar())
            {
         
                Console.WriteLine("Conectado al server");
                string mensaje;
                string respuesta;
                int cont = 0;
                while (cont == 0) {
                    Console.WriteLine("Escriba mensaje:");
                    mensaje = Console.ReadLine().Trim();
                    clienteSocket.Escribir(mensaje);
                    if (mensaje == "Adios")
                    {
                        clienteSocket.Desconectar();
                        cont = 1;
                    }
                    Console.WriteLine("Esperando respuesta del server");
                    respuesta = clienteSocket.Leer();
                    Console.WriteLine("R: {0}", respuesta);
                    if (respuesta == "Adios")
                    {
                 
                        clienteSocket.Desconectar();
                        Console.WriteLine("...??????");
                        cont = 1;
                    }
                }
                Console.WriteLine("Conexion finalizada");

            }
            else
            {
                Console.WriteLine("Error");
            }
            Console.ReadKey();
        }
    }
}

 