using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;


namespace TareaPrograSocket.Comunicacion
{
    internal class ClienteCom
    {
        private Socket cliente;
        private StreamReader reader;
        private StreamWriter writer;

        public ClienteCom(Socket socket)
        {
            this.cliente = socket;
            Stream stream = new NetworkStream(this.cliente);
            this.reader = new StreamReader(stream);
            this.writer = new StreamWriter(stream);
        }

        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public String Leer()
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
        public bool LeerMensajeCliente(string mensaje)
        {
            if (mensaje.ToLower() == "adios") {
                return true;
            }
            else {
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
        public void ComprobarMensajes(ClienteCom cliente)
        {
            string mensaje;
            string respuesta;
    
            int cont = 0;
            while (cont == 0)
            {
                respuesta = cliente.Leer();
                if (cliente.LeerMensajeCliente(respuesta))
                {

                    cliente.Desconectar();
                    Console.WriteLine("Cliente desconectado");
                    cont = 1;

                }
                else
                {
                    Console.WriteLine("El cliente dice: {0}", respuesta);
                    Console.WriteLine("Responder:");
                    mensaje = Console.ReadLine().Trim();
                    cliente.Escribir(mensaje);
                    if (cliente.LeerRespuestaServidor(mensaje))
                    {
                        cliente.Desconectar();
                        Console.WriteLine("Cliente desconectado");
                        cont = 1;
                        
                    }
                }
            }

        }

    }
}

