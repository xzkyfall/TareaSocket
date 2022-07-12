using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TareaPrograSocket.Comunicacion;

namespace TareaPrograSocket
{
    internal class Program
    {
        

        public static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine(puerto);
            Console.WriteLine("Iniciando Servidor en puerto {0}", puerto);

            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {
            
                Console.WriteLine("Sevidor iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando Cliente");
                    Socket socketCliente = servidor.ObtenerCliente();
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    cliente.ComprobarMensajes(cliente);
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} esta en uso", puerto);
            }
        }         
    }
    
}




