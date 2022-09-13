using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncFlowExample
{
    internal class CalculadoraHipotecaSync
    {
        public static int ObtenerYearsVidaLaboral()
        {
            Console.WriteLine("\n Obteniendo años de vida laboral...");
            Task.Delay(5000).Wait();    // Wait for 5 seconds.
            return new Random().Next(1, 35);    // Returns a value between 1 and 35.
        }

        public static bool EsTipoContratoIndefinido()
        {
            Console.WriteLine("\n Verificando si el tipo de contrato es indefinido...");
            Task.Delay(5000).Wait();
            return (new Random().Next(1, 10)) % 2 == 0; // Returns true or false if random value is odd/couple
        }

        public static int ObtenerSueldoNeto()
        {
            Console.WriteLine("\n Obteniendo sueldo neto...");
            Task.Delay(5000).Wait();    // Wait for 5 seconds.
            return new Random().Next(800, 6000);   // Returns a value between 800 and 6000.
        }

        public static int ObtenerGastosMensuales()
        {
            Console.WriteLine("\n Obteniendo gastos mensuales del usuario...");
            Task.Delay(5000).Wait();
            return new Random().Next(200, 1000);    // Returns a value between 200 and 1000.
        }

        public static bool AnalizarInformacionParaConcederHipoteca(
            int yearsVidaLaboral,
            bool tipoContratoIndefinido,
            int sueldoNeto,
            int gastosMensuales,
            int cantidadSolicitada,
            int yearsPagar)
        {
            Console.WriteLine("\n Analizando información para conceder la hipoteca...");

            if (yearsVidaLaboral < 2)
            {
                return false;
            }

            // Obtener la cuota
            var cuota = (cantidadSolicitada / yearsPagar) / 12 ;

            if (cuota > sueldoNeto || cuota > (sueldoNeto / 2))
            {
                return false;
            }

            var porcentajeGastosSobreSueldo = ((gastosMensuales * 100) / sueldoNeto);

            if(porcentajeGastosSobreSueldo > 30)
            {
                return false;
            }

            if((cuota + gastosMensuales) >= sueldoNeto)
            {
                return false;
            }

            if (!tipoContratoIndefinido)
            {
                if ((cuota + gastosMensuales) > (sueldoNeto / 3))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            // Si no cumple ninguna de las condiciones sí se la concedemos.
            return true;
        }

        internal static object AnalizarInformacionParaConcederHipoteca(int yearsVidaLaboral, bool esTipoContratoIndefinido, int sueldoNeto, int gastosMensuales)
        {
            throw new NotImplementedException();
        }
    }
};

// Mediante este sistema tarda 20 segundos: 4 operaciones por 5 segundos de delay cada una.