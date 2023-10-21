Menu();

void Menu()
{
    Console.Clear();
    Console.WriteLine("Válassz egy műveletet:");
    Console.Write("1 - Felhasználók listázása\t");
    Console.Write("2 - Felhasználók szűrése\t");
    Console.Write("3 - Felhasználók rendezése\t");
    Console.WriteLine("4 - Kilépés");

    int menu = Convert.ToInt32(Console.ReadLine());

    while (menu != 4)
    {
        switch (menu)
        {
            case 1:
                Console.WriteLine("Első opció");
                Console.ReadLine();
                Menu();
                return;
            case 2:
                Console.WriteLine("Második opció");
                Console.ReadLine();
                Menu();
                return;
            case 3:
                Console.WriteLine("Harmadik opció");
                Console.ReadLine();
                Menu();
                return;
            default:
                Console.WriteLine("Nincs ilyen művelet!");
                Console.ReadLine();
                Menu();
                return;
        }
    }
}
