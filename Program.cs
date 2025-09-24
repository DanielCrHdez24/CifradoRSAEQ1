using System;
using System.Text;
using System.Security.Cryptography;

namespace SeguridadInformaticaAlgoritmoCifradoAsimetrico
{
    class Program
    {
        static void Main(string[] args)
        {
            // Texto para cifrar
            string MensajeOriginal = "UNIDEH CRUZ LAMAS LUNA PATLÁN";

            // Crear claves RSA
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    // Exportar clave pública y privada
                    string ClavePublica = rsa.ToXmlString(false); // solo pública
                    string ClavePrivada = rsa.ToXmlString(true);  // pública + privada

                    Console.WriteLine("Clave Pública:\n{0}\n", ClavePublica);
                    Console.WriteLine("Clave Privada:\n{0}\n", ClavePrivada);

                    // Convertir mensaje original a bytes
                    byte[] datosOriginales = Encoding.UTF8.GetBytes(MensajeOriginal);

                    // Encriptar con la clave pública
                    rsa.FromXmlString(ClavePublica);
                    byte[] datosEncriptados = rsa.Encrypt(datosOriginales, false);

                    // Desencriptar con la clave privada
                    rsa.FromXmlString(ClavePrivada);
                    byte[] datosDesencriptados = rsa.Decrypt(datosEncriptados, false);

                    // Convertir de nuevo a string
                    string MensajeDesencriptado = Encoding.UTF8.GetString(datosDesencriptados);

                    // Mostrar resultados
                    Console.WriteLine("Mensaje Original: {0}\n", MensajeOriginal);
                    Console.WriteLine("Mensaje Encriptado (bytes): {0}\n", BitConverter.ToString(datosEncriptados));
                    Console.WriteLine("Mensaje Desencriptado: {0}\n", MensajeDesencriptado);

                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
