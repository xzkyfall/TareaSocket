using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TareaPrograSocketCliente.Comunicacion
{
    internal class ClienteSocket
    {
        private int puerto;
        private string server;
        private Socket cliente;
        private StreamReader reader;
        private StreamWriter writer;

        public ClienteSocket(string server, int puerto)
        {
            this.server = server;
            this.puerto = puerto;
        }

        public string Leer()
        {
            try
            {
                return this.reader.ReadLine().Trim();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
            }
            catch (Exception)
            {

            }
        }

        public void Desconectar()
        {
            try
            {
                this.cliente.Close();
            }
            catch (Exception)
            {

            }
        }
        public bool Conectar()
        {
            try
            {
                this.cliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(this.server), this.puerto);
                this.cliente.Connect(endPoint);
                Stream stream = new NetworkStream(this.cliente);
                this.reader = new StreamReader(stream);
                this.writer = new StreamWriter(stream);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }
        public bool LeerMensajeEnviado(string mensaje)
        {
            if (mensaje.ToLower() == "adios")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool LeerRespuestaServidor(string mensaje)
        {
            if (mensaje.ToLower() == "adios")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ComprobarMensajes(ClienteSocket cliente)
        {
            string mensaje;
            
            
            int cont = 0;
            while (cont == 0)
            {
                Console.WriteLine("Enviar Mensaje:");
                mensaje = Console.ReadLine().Trim();
                if (cliente.LeerMensajeEnviado(mensaje))
                {
                    cliente.Desconectar();                 
                    Console.WriteLine("Desconectado");
                    cont = 1;
                }
                else
                {
                    mensaje = cliente.Leer();
                    cliente.Escribir(mensaje);
                    if (cliente.LeerRespuestaServidor(mensaje))
                    {
                        cliente.Desconectar();
                        Console.WriteLine("Desconectado");
                        cont = 1;
                    }
                    else {
                        Console.WriteLine("El cliente dice: {0}",mensaje);

                    }
                    

                }
            }
        }
    }
}

