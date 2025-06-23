// -------------------------------------------
// Agenda Telefónica - Versión sencilla
// Autor: Ronald Piedra
// Fecha: 2025-06-22
// -------------------------------------------

using System;

namespace AgendaTelefonica
{
    // Clase que representa un contacto
    class Contacto
    {
        public int Id;
        public string Nombres;
        public string Apellidos;
        public bool Activo;
    }

    class Program
    {
        // Arreglo de 100 contactos máximo
        static Contacto[] agenda = new Contacto[100];

        static void Main()
        {
            bool continuar = true;

            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": Agregar(); break;
                    case "2": Listar(); break;
                    case "3": Buscar(); break;
                    case "4": Actualizar(); break;
                    case "5": Eliminar(); break;
                    case "6": continuar = false; break;
                    default: Console.WriteLine("Opción no válida."); break;
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n=== AGENDA TELEFÓNICA ===");
            Console.WriteLine("1. Agregar contacto");
            Console.WriteLine("2. Listar contactos");
            Console.WriteLine("3. Buscar contacto");
            Console.WriteLine("4. Actualizar contacto");
            Console.WriteLine("5. Eliminar contacto");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
        }

        static void Agregar()
        {
            int pos = EncontrarLibre();
            if (pos == -1)
            {
                Console.WriteLine("Agenda llena.");
                return;
            }

            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            if (Existe(id))
            {
                Console.WriteLine("El ID ya existe.");
                return;
            }

            Console.Write("Nombres: ");
            string nombres = Console.ReadLine();

            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine();

            agenda[pos] = new Contacto
            {
                Id = id,
                Nombres = nombres,
                Apellidos = apellidos,
                Activo = true
            };

            Console.WriteLine("Contacto agregado con éxito.");
        }

        static void Listar()
        {
            Console.WriteLine("\n--- LISTADO DE CONTACTOS ---");
            bool vacio = true;

            foreach (var c in agenda)
            {
                if (c != null && c.Activo)
                {
                    Console.WriteLine($"ID: {c.Id} | {c.Nombres} {c.Apellidos}");
                    vacio = false;
                }
            }

            if (vacio)
                Console.WriteLine("No hay contactos.");
        }

        static void Buscar()
        {
            Console.Write("Ingrese el ID del contacto: ");
            int id = int.Parse(Console.ReadLine());

            int pos = EncontrarPorId(id);
            if (pos != -1)
            {
                var c = agenda[pos];
                Console.WriteLine($"Encontrado: {c.Nombres} {c.Apellidos}");
            }
            else
            {
                Console.WriteLine("No se encontró ese contacto.");
            }
        }

        static void Actualizar()
        {
            Console.Write("Ingrese el ID a actualizar: ");
            int id = int.Parse(Console.ReadLine());

            int pos = EncontrarPorId(id);
            if (pos != -1)
            {
                Console.Write("Nuevos nombres: ");
                agenda[pos].Nombres = Console.ReadLine();

                Console.Write("Nuevos apellidos: ");
                agenda[pos].Apellidos = Console.ReadLine();

                Console.WriteLine("Contacto actualizado.");
            }
            else
            {
                Console.WriteLine("Contacto no encontrado.");
            }
        }

        static void Eliminar()
        {
            Console.Write("Ingrese el ID a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            int pos = EncontrarPorId(id);
            if (pos != -1)
            {
                agenda[pos].Activo = false;
                Console.WriteLine("Contacto eliminado.");
            }
            else
            {
                Console.WriteLine("No se encontró ese ID.");
            }
        }

        // ------------------ APOYO ------------------

        static int EncontrarLibre()
        {
            for (int i = 0; i < agenda.Length; i++)
            {
                if (agenda[i] == null || !agenda[i].Activo)
                    return i;
            }
            return -1;
        }

        static int EncontrarPorId(int id)
        {
            for (int i = 0; i < agenda.Length; i++)
            {
                if (agenda[i] != null && agenda[i].Activo && agenda[i].Id == id)
                    return i;
            }
            return -1;
        }

        static bool Existe(int id)
        {
            return EncontrarPorId(id) != -1;
        }
    }
}
