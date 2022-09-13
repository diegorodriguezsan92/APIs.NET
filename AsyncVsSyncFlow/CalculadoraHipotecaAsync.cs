using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncFlowExample
{
    public static class CalculadoraHipotecaAsync
    {
        public static async Task<int> ObtenerYearsVidaLaboral()
        {
            Console.WriteLine("\n Obteniendo años de vida laboral...");
            await Task.Delay(5000);
            return new Random().Next(1, 35);
        }
        public static async Task<bool> EsTipoContratoIndefinido()
        {
            Console.WriteLine("\n Verificando si el tipo de contrato es indefinido...");
            await Task.Delay(5000);
            return (new Random().Next(1, 10)) % 2 == 0; // Returns true or false if random value is odd/couple
        }

        public static async Task<int> ObtenerSueldoNeto()
        {
            Console.WriteLine("\n Obteniendo sueldo neto...");
            await Task.Delay(5000);
            return new Random().Next(800, 6000);
        }

        public static async Task<int> ObtenerGastosMensuales()
        {
            Console.WriteLine("\n Obteniendo gastos mensuales del usuario...");
            await Task.Delay(5000);
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
            var cuota = (cantidadSolicitada / yearsPagar) / 12;

            if (cuota > sueldoNeto || cuota > (sueldoNeto / 2))
            {
                return false;
            }

            var porcentajeGastosSobreSueldo = ((gastosMensuales * 100) / sueldoNeto);

            if (porcentajeGastosSobreSueldo > 30)
            {
                return false;
            }

            if ((cuota + gastosMensuales) >= sueldoNeto)
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
    }
}

// Mediante este sistema tarda 5 segundos en completar todas las tareas. Es un ejemplo simple pero muy ilustrativo.