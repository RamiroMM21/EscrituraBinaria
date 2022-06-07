using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos_Binarios
{
    
    class Program
    {
        class ArchivoBinarioEmpleados
        {
            // Declaración de flujos 
            BinaryWriter bw = null; // Flujo salida - escritura de datos 
            BinaryReader br = null; // Flujo entrada - lectura de datos

            // Campos de la clase
            string Nombre, Direccion;
            long Telefono;
            int NumEmp, DiasTrabajados;
            float SalarioDiario;

            public void CrearAchivo(string Archivo)
            {
                // Variable local metodo
                char resp;

                try
                {
                    // Creación del flujo para escribir datos al archivo 
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));
                    //CAPTURA DE DATOS 

                    do
                    {
                        Console.Clear();
                        Console.Write("Numero del Empleado: ");
                        NumEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del Empleado: ");
                        Nombre = Console.ReadLine();
                        Console.Write("Direccion del Empleado: ");
                        Direccion = Console.ReadLine();
                        Console.Write("Telefono del Empleado: ");
                        Telefono = long.Parse(Console.ReadLine());
                        Console.Write("Dias Trabajados del Empleado: ");
                        DiasTrabajados = Int32.Parse(Console.ReadLine());
                        Console.Write("Salario Diario del Empleado: ");
                        SalarioDiario = Int32.Parse(Console.ReadLine());

                        // Escribe los datos al archivo
                        bw.Write(NumEmp);
                        bw.Write(Nombre);
                        bw.Write(Direccion);
                        bw.Write(Telefono);
                        bw.Write(DiasTrabajados);
                        bw.Write(SalarioDiario);

                        Console.Write("\n\nDeseas almacenar otro registro (s/n)?");

                        resp = Char.Parse(Console.ReadLine());
                    } while ((resp == 'S') || (resp == 'S'));
                }
                catch (IOException e)
                {
                    Console.WriteLine("\nError : " + e.Message);
                    Console.WriteLine("\nRuta : " + e.StackTrace);
                }
                finally
                {
                    if (bw != null) bw.Close(); // Cierra el flujo - Escritura 
                    Console.Write("\nPresione <ENTER> para terminar la escritura de datos y regresar al Menu.");
                    Console.ReadKey();
                }
            }
            public void MostrarArchivo(string Archivo)
            {
                try
                {
                    // Verifica si existe el archivo
                    if (File.Exists(Archivo))
                    {
                        // Creación flujo para leer datos del archivo
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        // Despliegue de datos en pantalla 
                        Console.Clear();
                        do
                        {
                            // Lectura de registros mientras no llegue a EndOfFile
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Direccion = br.ReadString();
                            Telefono = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();

                            // Muestra los datos 
                            Console.WriteLine("Número del empleado: " + NumEmp);
                            Console.WriteLine("Nombre del empleado: " + Nombre);
                            Console.WriteLine("Dirección del empleado: " + Direccion);
                            Console.WriteLine("Telefono del empleado: " + Telefono);
                            Console.WriteLine("Dias trabajados del empleado: " + DiasTrabajados);
                            Console.WriteLine("Salario diario del empleado: {0:c}" + SalarioDiario);
                            Console.WriteLine("Sueldo total del empleado: {0:c}", (DiasTrabajados + SalarioDiario));
                            Console.WriteLine("\n");
                        } while (true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl archivo " + Archivo + " No existe en el disco!!");
                        Console.Write("\nPresione ENTER para continuar");
                        Console.ReadKey();
                    }
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("\n\nFin del listado de empleados");
                    Console.Write("\nPresione ENTER para continuar");
                }
                finally
                {
                    if (br != null) br.Close(); // Cierra flujo
                    Console.Write("\nPresione ENTER para terminar la lectura de datos y regresar al menú");
                    Console.ReadKey();
                }
            }
        }
        static void Main(string[] args)
        {
            // Declaración de variables auxiliares
            string Arch = null;
            int opcion;

            // Creación del objeto
            ArchivoBinarioEmpleados Al = new ArchivoBinarioEmpleados();

            // Menu de opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n***ARHIVO BINARIO EMPLEADOS");
                Console.WriteLine("1.- Creación de un archivo.");
                Console.WriteLine("2.- Lectura del archivo.");
                Console.WriteLine("3.- Salida del programa.");
                Console.Write("\nQue opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        // Bloque de escritura
                        try
                        {
                            // Captura nombre archivo 
                            Console.Write("\nAlimenta el nombre del archivo a crear: "); Arch = Console.ReadLine();
                            // Verifica si existe el archivo
                            char resp = 'S';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl archivo existe!!, deseas sobreescribirlo (S/N)?");
                                resp = char.Parse(Console.ReadLine());
                            }
                            if ((resp == 'S') || (resp == 'S'))
                                Al.CrearAchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;

                    case 2:
                        // Bloque de lectura 
                        try
                        {
                            // Captura nombre archivo 
                            Console.Write("\nAlimenta el nombre del archivo que deseas leer: "); Arch = Console.ReadLine();
                            Al.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione ENTER para salir del programa.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("\nEsa opción no existe!!, presione ENTER para continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }
    }
}
