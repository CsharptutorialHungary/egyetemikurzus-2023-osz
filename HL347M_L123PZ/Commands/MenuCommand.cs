using StudentTerminal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTerminal.Commands
{
    public static class MenuCommand
    {
        public static async Task Menu()
        {
            List<Student> students = await StudentCommand.LoadStudentsFromJson();

            Console.Clear();
            Console.WriteLine("Válassz egy műveletet:");
            Console.Write("1 - Tanulók listázása\t");
            Console.Write("2 - Tanuló szűrése\t");
            Console.Write("3 - Tanulók rendezése átlag alapján\t");
            Console.WriteLine("4 - Kilépés");

            int menu = Convert.ToInt32(Console.ReadLine());

            while (menu != 4)
            {
                switch (menu)
                {
                    case 1:
                        StudentCommand.Write(students);
                        Console.WriteLine("A folytatáshoz nyomj meg egy gombot!");
                        Console.ReadKey();
                        await Menu();
                        return;
                    case 2:
                        Console.WriteLine("Második opció");
                        Console.WriteLine("A folytatáshoz nyomj meg egy gombot!");
                        Console.ReadKey();
                        await Menu();
                        return;
                    case 3:
                        Console.WriteLine("Harmadik opció");
                        Console.WriteLine("A folytatáshoz nyomj meg egy gombot!");
                        Console.ReadKey();
                        await Menu();
                        return;
                    default:
                        Console.WriteLine("Nincs ilyen művelet!\nA folytatáshoz nyomj meg egy gombot!");
                        Console.ReadKey();
                        await Menu();
                        return;
                }
            }

            Console.WriteLine("Köszönjük, hogy a StudentTerminál alkalmazást használta!");

            Console.ReadKey();
        }
    }
}
