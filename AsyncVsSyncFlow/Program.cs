using AsyncFlowExample;
using System.Diagnostics;

Stopwatch stopwatch = new Stopwatch();  // Initializes a time counter
stopwatch.Start();

Console.WriteLine("\n **************************************************");
Console.WriteLine("\n Bienvenido a la calculadora de Hipotecas síncrona.");

var yearsVidaLaboral = CalculadoraHipotecaSync.ObtenerYearsVidaLaboral();
Console.WriteLine($"\n Años de vida laboral obtenidos: {yearsVidaLaboral}");

var esTipoContratoIndefinido = CalculadoraHipotecaSync.EsTipoContratoIndefinido();
Console.WriteLine($"\n Es tipo contrato indefinido: {esTipoContratoIndefinido}");

var sueldoNeto = CalculadoraHipotecaSync.ObtenerSueldoNeto();
Console.WriteLine($"\n Sueldo neto: {sueldoNeto}");

var gastosMensuales = CalculadoraHipotecaSync.ObtenerGastosMensuales();
Console.WriteLine($"\n Gastos mensuales: {gastosMensuales}");

var hipotecaConcedida = CalculadoraHipotecaSync.AnalizarInformacionParaConcederHipoteca(yearsVidaLaboral, esTipoContratoIndefinido, sueldoNeto, gastosMensuales, cantidadSolicitada: 50000, yearsPagar: 30);

var resultado = hipotecaConcedida ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\nAnálisis finalizado. Su hipoteca ha sido {resultado}");


stopwatch.Stop();

Console.WriteLine($"\n La operación ha durado: {stopwatch.Elapsed}");



stopwatch.Restart();
Console.WriteLine("\n **************************************************");
Console.WriteLine("\n Bienvenido a la calculadora de Hipotecas Asíncrona.");

Task<int> yearsVidaLaboralTask = CalculadoraHipotecaAsync.ObtenerYearsVidaLaboral();
Task<bool> esTipoContratoIndefinidoTask = CalculadoraHipotecaAsync.EsTipoContratoIndefinido();
Task<int> sueldoNetoTask = CalculadoraHipotecaAsync.ObtenerSueldoNeto();
Task<int> gastosMensualesTask = CalculadoraHipotecaAsync.ObtenerGastosMensuales();

var analisisHipotecaTasks = new List<Task>
{
    yearsVidaLaboralTask,
    esTipoContratoIndefinidoTask,
    sueldoNetoTask,
    gastosMensualesTask,
};

while (analisisHipotecaTasks.Any())
{
    Task tareaFinalizada = await Task.WhenAny(analisisHipotecaTasks);
    if(tareaFinalizada == yearsVidaLaboralTask)
    {
        Console.WriteLine($"\n Años de vida laboral obtenidos: {yearsVidaLaboralTask.Result}");
    }
    else if (tareaFinalizada == esTipoContratoIndefinidoTask)
    {
        Console.WriteLine($"\n Es tipo contrato indefinido: {esTipoContratoIndefinidoTask.Result}");
    }
    else if (tareaFinalizada == sueldoNetoTask)
    {
        Console.WriteLine($"\n Sueldo neto: {sueldoNetoTask.Result}");
            }
    else if (tareaFinalizada == gastosMensualesTask)
    {
        Console.WriteLine($"\n Gastos mensuales: {gastosMensualesTask.Result}");
    }

    analisisHipotecaTasks.Remove(tareaFinalizada);  // se elimina la tarea finalizada de la lista analisisHipotecaTasks.

}

var hipotecaConcedidaAsync = CalculadoraHipotecaAsync.AnalizarInformacionParaConcederHipoteca(yearsVidaLaboralTask.Result, esTipoContratoIndefinidoTask.Result, sueldoNetoTask.Result, gastosMensualesTask.Result, cantidadSolicitada: 50000, yearsPagar: 30);

var resultadoAsync = hipotecaConcedida ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\nAnálisis finalizado. Su hipoteca ha sido {resultadoAsync}");

stopwatch.Stop();

Console.WriteLine($"\n La operación Asíncrona ha durado: {stopwatch.Elapsed}");

Console.Read();